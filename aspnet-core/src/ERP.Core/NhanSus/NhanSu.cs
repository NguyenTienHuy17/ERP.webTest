using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using ERP.ThanhPhos;

namespace ERP.NhanSus
{
    [Table("NhanSus")]
    public class NhanSu : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(NhanSuConsts.MaxMaNhanSuLength, MinimumLength = NhanSuConsts.MinMaNhanSuLength)]
        public virtual string MaNhanSu { get; set; }

        [Required]
        [StringLength(NhanSuConsts.MaxTenNhanSuLength, MinimumLength = NhanSuConsts.MinTenNhanSuLength)]
        public virtual string TenNhanSu { get; set; }

        [Required]
        [StringLength(NhanSuConsts.MaxPhongBanLength, MinimumLength = NhanSuConsts.MinPhongBanLength)]
        public virtual string PhongBan { get; set; }

        [Range(NhanSuConsts.MinThamNienValue, NhanSuConsts.MaxThamNienValue)]
        public virtual int ThamNien { get; set; }

        [Range(NhanSuConsts.MinTuoiValue, NhanSuConsts.MaxTuoiValue)]
        public virtual int Tuoi { get; set; }


        //Foreign key for ThanhPho
        public int ThanhPhoId { get; set; }
        [ForeignKey("ThanhPhoId")]
        public ThanhPho ThanhPhoFk { get; set; }
    }
}