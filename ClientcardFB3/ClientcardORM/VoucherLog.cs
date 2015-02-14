namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoucherLog")]
    public partial class VoucherLog
    {
        [Key]
        public int TrxId { get; set; }

        public DateTime? TrxDate { get; set; }

        public int? HouseholdID { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? VoucherItemID { get; set; }

        public int? Infants { get; set; }

        public int? Youth { get; set; }

        public int? Teens { get; set; }

        public int? Eighteen { get; set; }

        public int? Adults { get; set; }

        public int? Seniors { get; set; }

        public int? TotalFamily { get; set; }

        public bool? FirstTimeEver { get; set; }

        public bool? FiscalFirstTime { get; set; }

        public bool? CalFirstTime { get; set; }

        public bool? MonthFirstTime { get; set; }

        public bool? Homeless { get; set; }

        public bool? Transient { get; set; }

        public int? Disabled { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
