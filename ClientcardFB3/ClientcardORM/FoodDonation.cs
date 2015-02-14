namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FoodDonation
    {
        [Key]
        public int TrxID { get; set; }

        public int? DonorID { get; set; }

        public DateTime? TrxDate { get; set; }

        [Required]
        [StringLength(30)]
        public string FoodCode { get; set; }

        public double? Pounds { get; set; }

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

        public bool? Flag0 { get; set; }

        public bool? Flag1 { get; set; }

        public bool? Flag2 { get; set; }

        public short? DonationType { get; set; }

        public short? FoodClass { get; set; }
    }
}
