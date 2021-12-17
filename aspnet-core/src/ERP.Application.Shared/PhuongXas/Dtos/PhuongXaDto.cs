using System;
using Abp.Application.Services.Dto;

namespace ERP.PhuongXas.Dtos
{
    public class PhuongXaDto : EntityDto
    {
        public string MaPhuong { get; set; }

        public string TenPhuong { get; set; }

        public int SoDan { get; set; }

        public string ChuTichPhuong { get; set; }

        public int ThanhPhoId { get; set; }

    }
}