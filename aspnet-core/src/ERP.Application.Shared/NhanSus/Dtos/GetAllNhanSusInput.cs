using Abp.Application.Services.Dto;
using System;

namespace ERP.NhanSus.Dtos
{
    public class GetAllNhanSusInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MaNhanSuFilter { get; set; }

        public string TenNhanSuFilter { get; set; }

        public string PhongBanFilter { get; set; }

        public int? MaxThamNienFilter { get; set; }
        public int? MinThamNienFilter { get; set; }

        public int? MaxTuoiFilter { get; set; }
        public int? MinTuoiFilter { get; set; }
        public string QueQuanFilter { get; set; }
    }
}