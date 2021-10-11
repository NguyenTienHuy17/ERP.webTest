using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ERP.ThanhPhos.Exporting;
using ERP.ThanhPhos.Dtos;
using ERP.Dto;
using Abp.Application.Services.Dto;
using ERP.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ERP.Storage;

namespace ERP.ThanhPhos
{
    [AbpAuthorize(AppPermissions.Pages_ThanhPhos)]
    public class ThanhPhosAppService : ERPAppServiceBase, IThanhPhosAppService
    {
        private readonly IRepository<ThanhPho> _thanhPhoRepository;
        private readonly IThanhPhosExcelExporter _thanhPhosExcelExporter;

        public ThanhPhosAppService(IRepository<ThanhPho> thanhPhoRepository, IThanhPhosExcelExporter thanhPhosExcelExporter)
        {
            _thanhPhoRepository = thanhPhoRepository;
            _thanhPhosExcelExporter = thanhPhosExcelExporter;

        }

        public async Task<PagedResultDto<GetThanhPhoForViewDto>> GetAll(GetAllThanhPhosInput input)
        {

            var filteredThanhPhos = _thanhPhoRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaTP.Contains(input.Filter) || e.TenTP.Contains(input.Filter) || e.MoTa.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaTPFilter), e => e.MaTP == input.MaTPFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenTPFilter), e => e.TenTP == input.TenTPFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MoTaFilter), e => e.MoTa == input.MoTaFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ZipCodeFilter), e => e.ZipCode == input.ZipCodeFilter);

            var pagedAndFilteredThanhPhos = filteredThanhPhos
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var thanhPhos = from o in pagedAndFilteredThanhPhos
                            select new
                            {
                                o.MaTP,
                                o.TenTP,
                                o.MoTa,
                                o.ZipCode,
                                Id = o.Id
                            };

            var totalCount = await filteredThanhPhos.CountAsync();

            var dbList = await thanhPhos.ToListAsync();
            var results = new List<GetThanhPhoForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetThanhPhoForViewDto()
                {
                    ThanhPho = new ThanhPhoDto
                    {

                        MaTP = o.MaTP,
                        TenTP = o.TenTP,
                        MoTa = o.MoTa,
                        Id = o.Id,
                        ZipCode = o.ZipCode,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetThanhPhoForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetThanhPhoForViewDto> GetThanhPhoForView(int id)
        {
            var thanhPho = await _thanhPhoRepository.GetAsync(id);

            var output = new GetThanhPhoForViewDto { ThanhPho = ObjectMapper.Map<ThanhPhoDto>(thanhPho) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ThanhPhos_Edit)]
        public async Task<GetThanhPhoForEditOutput> GetThanhPhoForEdit(EntityDto input)
        {
            var thanhPho = await _thanhPhoRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetThanhPhoForEditOutput { ThanhPho = ObjectMapper.Map<CreateOrEditThanhPhoDto>(thanhPho) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditThanhPhoDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_ThanhPhos_Create)]
        protected virtual async Task Create(CreateOrEditThanhPhoDto input)
        {
            var thanhPho = ObjectMapper.Map<ThanhPho>(input);

            if (AbpSession.TenantId != null)
            {
                thanhPho.TenantId = (int?)AbpSession.TenantId;
            }

            await _thanhPhoRepository.InsertAsync(thanhPho);

        }

        [AbpAuthorize(AppPermissions.Pages_ThanhPhos_Edit)]
        protected virtual async Task Update(CreateOrEditThanhPhoDto input)
        {
            var thanhPho = await _thanhPhoRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, thanhPho);

        }

        [AbpAuthorize(AppPermissions.Pages_ThanhPhos_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _thanhPhoRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetThanhPhosToExcel(GetAllThanhPhosForExcelInput input)
        {

            var filteredThanhPhos = _thanhPhoRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaTP.Contains(input.Filter) || e.TenTP.Contains(input.Filter) || e.MoTa.Contains(input.Filter) || e.ZipCode.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaTPFilter), e => e.MaTP == input.MaTPFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenTPFilter), e => e.TenTP == input.TenTPFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MoTaFilter), e => e.MoTa == input.MoTaFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ZipCodeFilter), e => e.ZipCode == input.ZipCodeFilter);

            var query = (from o in filteredThanhPhos
                         select new GetThanhPhoForViewDto()
                         {
                             ThanhPho = new ThanhPhoDto
                             {
                                 MaTP = o.MaTP,
                                 TenTP = o.TenTP,
                                 MoTa = o.MoTa,
                                 ZipCode = o.ZipCode,
                                 Id = o.Id
                             }
                         });

            var thanhPhoListDtos = await query.ToListAsync();

            return _thanhPhosExcelExporter.ExportToFile(thanhPhoListDtos);
        }

    }
}