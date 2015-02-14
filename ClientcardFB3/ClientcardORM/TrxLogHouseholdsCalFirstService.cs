namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrxLogHouseholdsCalFirstService")]
    public partial class TrxLogHouseholdsCalFirstService
    {
        [StringLength(5)]
        public string FiscalQuarter { get; set; }

        public DateTime? TrxDate { get; set; }

        public int? HouseholdId { get; set; }

        [StringLength(6)]
        public string FiscalPeriod { get; set; }

        [StringLength(6)]
        public string YearMonth { get; set; }

        public bool? Homeless { get; set; }

        public bool? InCityLimits { get; set; }

        public bool? Transient { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHSingleFemale { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHSingleMale { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHSingleOther { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHSingleMinor { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHOneParentFemale { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHOneParentMale { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHOneParentOther { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHTwoParent { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHOtherWithMinors { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHPartnered { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HHOtherRelated { get; set; }

        public bool? FiscalFirstTime { get; set; }

        public short? Infants { get; set; }

        public short? Youth { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Teens { get; set; }

        public int? Eighteen { get; set; }

        public short? Adults { get; set; }

        public short? Seniors { get; set; }

        public int? TotalFamily { get; set; }

        public int? SpecialDiet { get; set; }

        public int? Disabled { get; set; }

        public short? ClientType { get; set; }

        [StringLength(50)]
        public string ZipCode { get; set; }

        public bool? SingleHeadHh { get; set; }
    }
}
