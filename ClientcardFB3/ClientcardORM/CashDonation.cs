namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CashDonation
    {
        [Key]
        public int TrxID { get; set; }

        public int? DonorID { get; set; }

        public DateTime? TrxDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? DollarValue { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
