using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ERP.NhanSus.Dtos
{
    public class CreateOrEditNhanSuDto : EntityDto<int?>
    {

        [Required]
        [StringLength(NhanSuConsts.MaxMaNhanSuLength, MinimumLength = NhanSuConsts.MinMaNhanSuLength)]
        public string MaNhanSu { get; set; }

        [Required]
        [StringLength(NhanSuConsts.MaxTenNhanSuLength, MinimumLength = NhanSuConsts.MinTenNhanSuLength)]
        public string TenNhanSu { get; set; }

        [Required]
        [StringLength(NhanSuConsts.MaxPhongBanLength, MinimumLength = NhanSuConsts.MinPhongBanLength)]
        public string PhongBan { get; set; }

        [Range(NhanSuConsts.MinThamNienValue, NhanSuConsts.MaxThamNienValue)]
        public int ThamNien { get; set; }

        [Range(NhanSuConsts.MinTuoiValue, NhanSuConsts.MaxTuoiValue)]
        public int Tuoi { get; set; }
        public string QueQuan { get; set; }
        public object ThanhPhoId { get; set; }
    }
}