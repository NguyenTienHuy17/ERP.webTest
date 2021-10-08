using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ERP.ThanhPhos.Dtos
{
    public class CreateOrEditThanhPhoDto : EntityDto<int?>
    {

        [Required]
        [StringLength(ThanhPhoConsts.MaxMaTPLength, MinimumLength = ThanhPhoConsts.MinMaTPLength)]
        public string MaTP { get; set; }

        [Required]
        [StringLength(ThanhPhoConsts.MaxTenTPLength, MinimumLength = ThanhPhoConsts.MinTenTPLength)]
        public string TenTP { get; set; }

        [StringLength(ThanhPhoConsts.MaxMoTaLength, MinimumLength = ThanhPhoConsts.MinMoTaLength)]
        public string MoTa { get; set; }
        public string ZipCode { get; set; }

    }
}