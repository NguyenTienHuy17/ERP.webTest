using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.NhanSus.Dtos;
using ERP.Dto;

namespace ERP.NhanSus
{
    public interface INhanSusAppService : IApplicationService
    {
        Task<PagedResultDto<GetNhanSuForViewDto>> GetAll(GetAllNhanSusInput input);

        Task<GetNhanSuForViewDto> GetNhanSuForView(int id);

        Task<GetNhanSuForEditOutput> GetNhanSuForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditNhanSuDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetNhanSusToExcel(GetAllNhanSusForExcelInput input);

    }
}