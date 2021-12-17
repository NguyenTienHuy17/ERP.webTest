using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.Collections.Generic;
using ERP.NhanSus;

namespace ERP.ThanhPhos
{
    [Table("ThanhPhos")]
    public class ThanhPho : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(ThanhPhoConsts.MaxMaTPLength, MinimumLength = ThanhPhoConsts.MinMaTPLength)]
        public virtual string MaTP { get; set; }

        [Required]
        [StringLength(ThanhPhoConsts.MaxTenTPLength, MinimumLength = ThanhPhoConsts.MinTenTPLength)]
        public virtual string TenTP { get; set; }

        [StringLength(ThanhPhoConsts.MaxMoTaLength, MinimumLength = ThanhPhoConsts.MinMoTaLength)]
        public virtual string MoTa { get; set; }
        public virtual string ZipCode { get; set; }

        public ICollection<NhanSu> NhanSus { get; set; }
    }
}