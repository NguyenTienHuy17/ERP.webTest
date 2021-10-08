using System;
using Abp.Application.Services.Dto;

namespace ERP.ThanhPhos.Dtos
{
    public class ThanhPhoDto : EntityDto
    {
        public string MaTP { get; set; }

        public string TenTP { get; set; }

        public string MoTa { get; set; }

    }
}