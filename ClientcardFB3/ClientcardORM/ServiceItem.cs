namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServiceItem
    {
        [Key]
        public int ItemKey { get; set; }

        [StringLength(32)]
        public string ItemDesc { get; set; }

        public short? ItemType { get; set; }

        public short? ItemRule { get; set; }

        public int? LbsPerItem { get; set; }

        public bool NotAvailable { get; set; }

        public int? FS01 { get; set; }

        public int? FS02 { get; set; }

        public int? FS03 { get; set; }

        public int? FS04 { get; set; }

        public int? FS05 { get; set; }

        public int? FS06 { get; set; }

        public int? FS07 { get; set; }

        public int? FS08 { get; set; }

        public int? FS09 { get; set; }

        public int? FS10 { get; set; }

        public int? FS11 { get; set; }

        public int? FS12 { get; set; }

        public int? FS13 { get; set; }

        public int? FS14 { get; set; }

        public int? FS15 { get; set; }

        public int? FS16 { get; set; }

        public int? FS17 { get; set; }

        public int? FS18 { get; set; }

        public int? FS19 { get; set; }

        public int? FS20 { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool? UseAgeGroup { get; set; }

        public bool? AllowInfants { get; set; }

        public bool? AllowYouth { get; set; }

        public bool? AllowTeens { get; set; }

        public bool? AllowAdults { get; set; }

        public bool? AllowSeniors { get; set; }

        [StringLength(5)]
        public string SvcMask { get; set; }

        public bool? Exclusive { get; set; }

        public int? DefaultSvcGrp { get; set; }

        public bool? FastTrack { get; set; }

        public bool? PrintReceipt { get; set; }

        public bool? FullService { get; set; }
    }
}
