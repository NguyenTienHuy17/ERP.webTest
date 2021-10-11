using Abp.Application.Services.Dto;

namespace ERP.NhanSus.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}