using System;
using Abp.Application.Services.Dto;

namespace ERP.NhanSus.Dtos
{
    public class NhanSuDto : EntityDto
    {
        public string MaNhanSu { get; set; }

        public string TenNhanSu { get; set; }

        public string PhongBan { get; set; }

        public int ThamNien { get; set; }

        public int Tuoi { get; set; }

        public int ThanhPhoId { get; set; }
    }
}