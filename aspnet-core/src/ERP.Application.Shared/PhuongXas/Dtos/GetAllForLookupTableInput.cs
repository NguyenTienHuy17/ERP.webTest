using Abp.Application.Services.Dto;

namespace ERP.PhuongXas.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}