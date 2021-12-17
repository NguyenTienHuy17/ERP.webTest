using ERP.ThanhPhos;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ERP.PhuongXas.Exporting;
using ERP.PhuongXas.Dtos;
using ERP.Dto;
using Abp.Application.Services.Dto;
using ERP.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ERP.Storage;

namespace ERP.PhuongXas
{
    [AbpAuthorize(AppPermissions.Pages_PhuongXas)]
    public class PhuongXasAppService : ERPAppServiceBase, IPhuongXasAppService
    {
        private readonly IRepository<PhuongXa> _phuongXaRepository;
        private readonly IPhuongXasExcelExporter _phuongXasExcelExporter;
        private readonly IRepository<ThanhPho, int> _lookup_thanhPhoRepository;

        public PhuongXasAppService(IRepository<PhuongXa> phuongXaRepository, IPhuongXasExcelExporter phuongXasExcelExporter, IRepository<ThanhPho, int> lookup_thanhPhoRepository)
        {
            _phuongXaRepository = phuongXaRepository;
            _phuongXasExcelExporter = phuongXasExcelExporter;
            _lookup_thanhPhoRepository = lookup_thanhPhoRepository;

        }

        public async Task<PagedResultDto<GetPhuongXaForViewDto>> GetAll(GetAllPhuongXasInput input)
        {

            var filteredPhuongXas = _phuongXaRepository.GetAll()
                        .Include(e => e.ThanhPhoFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaPhuong.Contains(input.Filter) || e.TenPhuong.Contains(input.Filter) || e.ChuTichPhuong.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaPhuongFilter), e => e.MaPhuong == input.MaPhuongFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenPhuongFilter), e => e.TenPhuong == input.TenPhuongFilter)
                        .WhereIf(input.MinSoDanFilter != null, e => e.SoDan >= input.MinSoDanFilter)
                        .WhereIf(input.MaxSoDanFilter != null, e => e.SoDan <= input.MaxSoDanFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ChuTichPhuongFilter), e => e.ChuTichPhuong == input.ChuTichPhuongFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ThanhPhoMaTPFilter), e => e.ThanhPhoFk != null && e.ThanhPhoFk.MaTP == input.ThanhPhoMaTPFilter);

            var pagedAndFilteredPhuongXas = filteredPhuongXas
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var phuongXas = from o in pagedAndFilteredPhuongXas
                            join o1 in _lookup_thanhPhoRepository.GetAll() on o.ThanhPhoId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            select new
                            {

                                o.MaPhuong,
                                o.TenPhuong,
                                o.SoDan,
                                o.ChuTichPhuong,
                                Id = o.Id,
                                ThanhPhoMaTP = s1 == null || s1.MaTP == null ? "" : s1.MaTP.ToString()
                            };

            var totalCount = await filteredPhuongXas.CountAsync();

            var dbList = await phuongXas.ToListAsync();
            var results = new List<GetPhuongXaForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPhuongXaForViewDto()
                {
                    PhuongXa = new PhuongXaDto
                    {

                        MaPhuong = o.MaPhuong,
                        TenPhuong = o.TenPhuong,
                        SoDan = o.SoDan,
                        ChuTichPhuong = o.ChuTichPhuong,
                        Id = o.Id,
                    },
                    ThanhPhoMaTP = o.ThanhPhoMaTP
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPhuongXaForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetPhuongXaForViewDto> GetPhuongXaForView(int id)
        {
            var phuongXa = await _phuongXaRepository.GetAsync(id);

            var output = new GetPhuongXaForViewDto { PhuongXa = ObjectMapper.Map<PhuongXaDto>(phuongXa) };

            if (output.PhuongXa.ThanhPhoId != null)
            {
                var _lookupThanhPho = await _lookup_thanhPhoRepository.FirstOrDefaultAsync((int)output.PhuongXa.ThanhPhoId);
                output.ThanhPhoMaTP = _lookupThanhPho?.MaTP?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_PhuongXas_Edit)]
        public async Task<GetPhuongXaForEditOutput> GetPhuongXaForEdit(EntityDto input)
        {
            var phuongXa = await _phuongXaRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPhuongXaForEditOutput { PhuongXa = ObjectMapper.Map<CreateOrEditPhuongXaDto>(phuongXa) };

            if (output.PhuongXa.ThanhPhoId != null)
            {
                var _lookupThanhPho = await _lookup_thanhPhoRepository.FirstOrDefaultAsync((int)output.PhuongXa.ThanhPhoId);
                output.ThanhPhoMaTP = _lookupThanhPho?.MaTP?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPhuongXaDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PhuongXas_Create)]
        protected virtual async Task Create(CreateOrEditPhuongXaDto input)
        {
            var phuongXa = ObjectMapper.Map<PhuongXa>(input);

            if (AbpSession.TenantId != null)
            {
                phuongXa.TenantId = (int?)AbpSession.TenantId;
            }

            await _phuongXaRepository.InsertAsync(phuongXa);

        }

        [AbpAuthorize(AppPermissions.Pages_PhuongXas_Edit)]
        protected virtual async Task Update(CreateOrEditPhuongXaDto input)
        {
            var phuongXa = await _phuongXaRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, phuongXa);

        }

        [AbpAuthorize(AppPermissions.Pages_PhuongXas_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _phuongXaRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetPhuongXasToExcel(GetAllPhuongXasForExcelInput input)
        {

            var filteredPhuongXas = _phuongXaRepository.GetAll()
                        .Include(e => e.ThanhPhoFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaPhuong.Contains(input.Filter) || e.TenPhuong.Contains(input.Filter) || e.ChuTichPhuong.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaPhuongFilter), e => e.MaPhuong == input.MaPhuongFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenPhuongFilter), e => e.TenPhuong == input.TenPhuongFilter)
                        .WhereIf(input.MinSoDanFilter != null, e => e.SoDan >= input.MinSoDanFilter)
                        .WhereIf(input.MaxSoDanFilter != null, e => e.SoDan <= input.MaxSoDanFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ChuTichPhuongFilter), e => e.ChuTichPhuong == input.ChuTichPhuongFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ThanhPhoMaTPFilter), e => e.ThanhPhoFk != null && e.ThanhPhoFk.MaTP == input.ThanhPhoMaTPFilter);

            var query = (from o in filteredPhuongXas
                         join o1 in _lookup_thanhPhoRepository.GetAll() on o.ThanhPhoId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetPhuongXaForViewDto()
                         {
                             PhuongXa = new PhuongXaDto
                             {
                                 MaPhuong = o.MaPhuong,
                                 TenPhuong = o.TenPhuong,
                                 SoDan = o.SoDan,
                                 ChuTichPhuong = o.ChuTichPhuong,
                                 Id = o.Id
                             },
                             ThanhPhoMaTP = s1 == null || s1.MaTP == null ? "" : s1.MaTP.ToString()
                         });

            var phuongXaListDtos = await query.ToListAsync();

            return _phuongXasExcelExporter.ExportToFile(phuongXaListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_PhuongXas)]
        public async Task<List<PhuongXaThanhPhoLookupTableDto>> GetAllThanhPhoForTableDropdown()
        {
            return await _lookup_thanhPhoRepository.GetAll()
                .Select(thanhPho => new PhuongXaThanhPhoLookupTableDto
                {
                    Id = thanhPho.Id,
                    DisplayName = thanhPho == null || thanhPho.MaTP == null ? "" : thanhPho.MaTP.ToString()
                }).ToListAsync();
        }

    }
}