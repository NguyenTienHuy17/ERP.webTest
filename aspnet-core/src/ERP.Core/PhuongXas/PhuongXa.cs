using ERP.ThanhPhos;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ERP.PhuongXas
{
    [Table("PhuongXas")]
    public class PhuongXa : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(PhuongXaConsts.MaxMaPhuongLength, MinimumLength = PhuongXaConsts.MinMaPhuongLength)]
        public virtual string MaPhuong { get; set; }

        [Required]
        [StringLength(PhuongXaConsts.MaxTenPhuongLength, MinimumLength = PhuongXaConsts.MinTenPhuongLength)]
        public virtual string TenPhuong { get; set; }

        public virtual int SoDan { get; set; }

        [Required]
        [StringLength(PhuongXaConsts.MaxChuTichPhuongLength, MinimumLength = PhuongXaConsts.MinChuTichPhuongLength)]
        public virtual string ChuTichPhuong { get; set; }

        public virtual int ThanhPhoId { get; set; }

        [ForeignKey("ThanhPhoId")]
        public ThanhPho ThanhPhoFk { get; set; }

    }
}