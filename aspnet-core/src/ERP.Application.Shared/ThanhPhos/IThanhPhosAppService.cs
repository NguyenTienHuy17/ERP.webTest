using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.ThanhPhos.Dtos;
using ERP.Dto;

namespace ERP.ThanhPhos
{
    public interface IThanhPhosAppService : IApplicationService
    {
        Task<PagedResultDto<GetThanhPhoForViewDto>> GetAll(GetAllThanhPhosInput input);

        Task<GetThanhPhoForViewDto> GetThanhPhoForView(int id);

        Task<GetThanhPhoForEditOutput> GetThanhPhoForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditThanhPhoDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetThanhPhosToExcel(GetAllThanhPhosForExcelInput input);

    }
}