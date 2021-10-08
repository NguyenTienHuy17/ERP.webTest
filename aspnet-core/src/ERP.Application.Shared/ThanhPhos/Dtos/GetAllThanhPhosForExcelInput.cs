using Abp.Application.Services.Dto;
using System;

namespace ERP.ThanhPhos.Dtos
{
    public class GetAllThanhPhosForExcelInput
    {
        public string Filter { get; set; }

        public string MaTPFilter { get; set; }

        public string TenTPFilter { get; set; }

        public string MoTaFilter { get; set; }

    }
}