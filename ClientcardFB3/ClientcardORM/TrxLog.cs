namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrxLog")]
    public partial class TrxLog
    {
        [Key]
        public int TrxId { get; set; }

        public DateTime? TrxDate { get; set; }

        public int? HouseholdID { get; set; }

        public short? TrxStatus { get; set; }

        public short? Meals { get; set; }

        public short? Bags { get; set; }

        public int? LbsStd { get; set; }

        public int? LbsOther { get; set; }

        public int? LbsSupplemental { get; set; }

        public int? LbsCommodity { get; set; }

        public int? LbsBabySvc { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public string FoodSvcList { get; set; }

        public string NonFoodSvcList { get; set; }

        public string BabySvcList { get; set; }

        public string ConcatFoodSvcItemsList { get; set; }

        public string ConcatNonFoodSvcItemsList { get; set; }

        public string ConcatBabySvcItemsList { get; set; }

        public short? Infants { get; set; }

        public short? Youth { get; set; }

        public short? Adults { get; set; }

        public short? Seniors { get; set; }

        public int? TotalFamily { get; set; }

        public int? SpecialDiet { get; set; }

        public int? NumCat1 { get; set; }

        public int? NumCat2 { get; set; }

        public bool? RcvdCommodity { get; set; }

        public bool? RcvdSupplemental { get; set; }

        public bool? FirstTimeEver { get; set; }

        public bool? FiscalFirstTime { get; set; }

        public bool? CalFirstTime { get; set; }

        public bool? Transient { get; set; }

        public int? EthnicSpeaking { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool? MonthFirstTime { get; set; }

        public int? DelivMethod { get; set; }

        public int? LbsNonFood { get; set; }

        public bool? RcvdVoucher { get; set; }

        public int Teens { get; set; }

        public bool? Homeless { get; set; }

        public bool? InCityLimits { get; set; }

        public short? ClientType { get; set; }

        [StringLength(50)]
        public string ZipCode { get; set; }

        public int? Disabled { get; set; }

        [StringLength(50)]
        public string HHMemID { get; set; }

        public int? Eighteen { get; set; }

        public bool? SingleHeadHh { get; set; }

        public int? FBProgram { get; set; }

        public int? White { get; set; }

        public int? Black { get; set; }

        public int? Asian { get; set; }

        public int? Native { get; set; }

        public int? Pacific { get; set; }

        public int? WhiteNative { get; set; }

        public int? WhiteAsian { get; set; }

        public int? WhiteBlack { get; set; }

        public int? BlackNative { get; set; }

        public int? Other { get; set; }

        public int? Undisclosed { get; set; }

        public int? WhiteHispanic { get; set; }

        public int? BlackHispanic { get; set; }

        public int? AsianHispanic { get; set; }

        public int? NativeHispanic { get; set; }

        public int? PacificHispanic { get; set; }

        public int? WhiteNativeHispanic { get; set; }

        public int? WhiteAsianHispanic { get; set; }

        public int? WhiteBlackHispanic { get; set; }

        public int? BlackNativeHispanic { get; set; }

        public int? OtherHispanic { get; set; }

        public int? UndisclosedHispanic { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public int? ServiceGroup { get; set; }

        public bool? FastTrack { get; set; }

        public bool? PrintReceipt { get; set; }

        public bool? FullService { get; set; }
    }
}
