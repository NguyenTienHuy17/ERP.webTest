using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ERP.NhanSus.Exporting;
using ERP.NhanSus.Dtos;
using ERP.Dto;
using Abp.Application.Services.Dto;
using ERP.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ERP.Storage;

namespace ERP.NhanSus
{
    [AbpAuthorize(AppPermissions.Pages_NhanSus)]
    public class NhanSusAppService : ERPAppServiceBase, INhanSusAppService
    {
        private readonly IRepository<NhanSu> _nhanSuRepository;
        private readonly INhanSusExcelExporter _nhanSusExcelExporter;

        public NhanSusAppService(IRepository<NhanSu> nhanSuRepository, INhanSusExcelExporter nhanSusExcelExporter)
        {
            _nhanSuRepository = nhanSuRepository;
            _nhanSusExcelExporter = nhanSusExcelExporter;

        }

        public async Task<PagedResultDto<GetNhanSuForViewDto>> GetAll(GetAllNhanSusInput input)
        {

            var filteredNhanSus = _nhanSuRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaNhanSu.Contains(input.Filter) || e.TenNhanSu.Contains(input.Filter) || e.PhongBan.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaNhanSuFilter), e => e.MaNhanSu == input.MaNhanSuFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenNhanSuFilter), e => e.TenNhanSu == input.TenNhanSuFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhongBanFilter), e => e.PhongBan == input.PhongBanFilter)
                        .WhereIf(input.MinThamNienFilter != null, e => e.ThamNien >= input.MinThamNienFilter)
                        .WhereIf(input.MaxThamNienFilter != null, e => e.ThamNien <= input.MaxThamNienFilter)
                        .WhereIf(input.MinTuoiFilter != null, e => e.Tuoi >= input.MinTuoiFilter)
                        .WhereIf(input.MaxTuoiFilter != null, e => e.Tuoi <= input.MaxTuoiFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.QueQuanFilter), e => e.QueQuan == input.QueQuanFilter);

            var pagedAndFilteredNhanSus = filteredNhanSus
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var nhanSus = from o in pagedAndFilteredNhanSus
                          select new
                          {

                              o.MaNhanSu,
                              o.TenNhanSu,
                              o.PhongBan,
                              o.ThamNien,
                              o.Tuoi,
                              o.QueQuan,
                              Id = o.Id
                          };

            var totalCount = await filteredNhanSus.CountAsync();

            var dbList = await nhanSus.ToListAsync();
            var results = new List<GetNhanSuForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetNhanSuForViewDto()
                {
                    NhanSu = new NhanSuDto
                    {

                        MaNhanSu = o.MaNhanSu,
                        TenNhanSu = o.TenNhanSu,
                        PhongBan = o.PhongBan,
                        ThamNien = o.ThamNien,
                        Tuoi = o.Tuoi,
                        Id = o.Id,
                        QueQuan = o.QueQuan,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetNhanSuForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetNhanSuForViewDto> GetNhanSuForView(int id)
        {
            var nhanSu = await _nhanSuRepository.GetAsync(id);

            var output = new GetNhanSuForViewDto { NhanSu = ObjectMapper.Map<NhanSuDto>(nhanSu) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_NhanSus_Edit)]
        public async Task<GetNhanSuForEditOutput> GetNhanSuForEdit(EntityDto input)
        {
            var nhanSu = await _nhanSuRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetNhanSuForEditOutput { NhanSu = ObjectMapper.Map<CreateOrEditNhanSuDto>(nhanSu) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditNhanSuDto input)
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

        [AbpAuthorize(AppPermissions.Pages_NhanSus_Create)]
        protected virtual async Task Create(CreateOrEditNhanSuDto input)
        {
            var nhanSu = ObjectMapper.Map<NhanSu>(input);

            if (AbpSession.TenantId != null)
            {
                nhanSu.TenantId = (int?)AbpSession.TenantId;
            }

            await _nhanSuRepository.InsertAsync(nhanSu);

        }

        [AbpAuthorize(AppPermissions.Pages_NhanSus_Edit)]
        protected virtual async Task Update(CreateOrEditNhanSuDto input)
        {
            var nhanSu = await _nhanSuRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, nhanSu);

        }

        [AbpAuthorize(AppPermissions.Pages_NhanSus_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _nhanSuRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetNhanSusToExcel(GetAllNhanSusForExcelInput input)
        {

            var filteredNhanSus = _nhanSuRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaNhanSu.Contains(input.Filter) || e.TenNhanSu.Contains(input.Filter) || e.PhongBan.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaNhanSuFilter), e => e.MaNhanSu == input.MaNhanSuFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenNhanSuFilter), e => e.TenNhanSu == input.TenNhanSuFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhongBanFilter), e => e.PhongBan == input.PhongBanFilter)
                        .WhereIf(input.MinThamNienFilter != null, e => e.ThamNien >= input.MinThamNienFilter)
                        .WhereIf(input.MaxThamNienFilter != null, e => e.ThamNien <= input.MaxThamNienFilter)
                        .WhereIf(input.MinTuoiFilter != null, e => e.Tuoi >= input.MinTuoiFilter)
                        .WhereIf(input.MaxTuoiFilter != null, e => e.Tuoi <= input.MaxTuoiFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.QueQuanFilter), e => e.QueQuan == input.QueQuanFilter);

            var query = (from o in filteredNhanSus
                         select new GetNhanSuForViewDto()
                         {
                             NhanSu = new NhanSuDto
                             {
                                 MaNhanSu = o.MaNhanSu,
                                 TenNhanSu = o.TenNhanSu,
                                 PhongBan = o.PhongBan,
                                 ThamNien = o.ThamNien,
                                 Tuoi = o.Tuoi,
                                 QueQuan = o.QueQuan,
                                 Id = o.Id
                             }
                         });

            var nhanSuListDtos = await query.ToListAsync();

            return _nhanSusExcelExporter.ExportToFile(nhanSuListDtos);
        }

    }
}