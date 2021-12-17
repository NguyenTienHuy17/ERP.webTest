using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ERP.PhuongXas.Dtos
{
    public class CreateOrEditPhuongXaDto : EntityDto<int?>
    {

        [Required]
        [StringLength(PhuongXaConsts.MaxMaPhuongLength, MinimumLength = PhuongXaConsts.MinMaPhuongLength)]
        public string MaPhuong { get; set; }

        [Required]
        [StringLength(PhuongXaConsts.MaxTenPhuongLength, MinimumLength = PhuongXaConsts.MinTenPhuongLength)]
        public string TenPhuong { get; set; }

        public int SoDan { get; set; }

        [Required]
        [StringLength(PhuongXaConsts.MaxChuTichPhuongLength, MinimumLength = PhuongXaConsts.MinChuTichPhuongLength)]
        public string ChuTichPhuong { get; set; }

        public int ThanhPhoId { get; set; }

    }
}