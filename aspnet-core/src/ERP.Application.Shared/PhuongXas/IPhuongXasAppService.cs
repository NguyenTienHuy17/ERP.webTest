using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.PhuongXas.Dtos;
using ERP.Dto;
using System.Collections.Generic;

namespace ERP.PhuongXas
{
    public interface IPhuongXasAppService : IApplicationService
    {
        Task<PagedResultDto<GetPhuongXaForViewDto>> GetAll(GetAllPhuongXasInput input);

        Task<GetPhuongXaForViewDto> GetPhuongXaForView(int id);

        Task<GetPhuongXaForEditOutput> GetPhuongXaForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditPhuongXaDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetPhuongXasToExcel(GetAllPhuongXasForExcelInput input);

        Task<List<PhuongXaThanhPhoLookupTableDto>> GetAllThanhPhoForTableDropdown();

    }
}