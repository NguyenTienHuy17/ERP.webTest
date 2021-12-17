using Abp.Application.Services.Dto;
using System;

namespace ERP.PhuongXas.Dtos
{
    public class GetAllPhuongXasInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MaPhuongFilter { get; set; }

        public string TenPhuongFilter { get; set; }

        public int? MaxSoDanFilter { get; set; }
        public int? MinSoDanFilter { get; set; }

        public string ChuTichPhuongFilter { get; set; }

        public string ThanhPhoMaTPFilter { get; set; }

    }
}