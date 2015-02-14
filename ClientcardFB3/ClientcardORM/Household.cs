namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Household")]
    public partial class Household
    {
        public int ID { get; set; }

        public bool? Inactive { get; set; }

        [StringLength(75)]
        public string Name { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zipcode { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        public short? PhoneType { get; set; }

        public short? Infants { get; set; }

        public short? Youth { get; set; }

        public short? Teens { get; set; }

        public short? Adults { get; set; }

        public short? Seniors { get; set; }

        public short? TotalFamily { get; set; }

        public short? SpecialDiet { get; set; }

        public bool? IncludeOnLog { get; set; }

        public bool? NeedCommoditySignature { get; set; }

        public bool? UseFamilyList { get; set; }

        public short? EthnicSpeaking { get; set; }

        [StringLength(512)]
        public string Comments { get; set; }

        public bool? AutoAlert { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime? FirstService { get; set; }

        public DateTime? LatestService { get; set; }

        public DateTime? FirstSvcThisYear { get; set; }

        public DateTime? LastCommodityService { get; set; }

        public bool? SecondServiceThisMonth { get; set; }

        public double? UserNum0 { get; set; }

        public double? UserNum1 { get; set; }

        public bool? UserFlag0 { get; set; }

        public bool? UserFlag1 { get; set; }

        public bool? UserFlag2 { get; set; }

        public bool? UserFlag3 { get; set; }

        public bool? UserFlag4 { get; set; }

        public bool? UserFlag5 { get; set; }

        public bool? UserFlag6 { get; set; }

        public bool? UserFlag7 { get; set; }

        public bool? UserFlag8 { get; set; }

        public bool? UserFlag9 { get; set; }

        public short? ClientType { get; set; }

        public bool? NoCommodities { get; set; }

        public bool? NeedToVerifyID { get; set; }

        public DateTime? DateIDVerified { get; set; }

        public bool? SupplOnly { get; set; }

        public short IdType { get; set; }

        public short? Disabled { get; set; }

        public bool? InCityLimits { get; set; }

        public bool? Homeless { get; set; }

        public int? AnnualIncome { get; set; }

        public short? NbrCSFP { get; set; }

        public bool? BabyServices { get; set; }

        public string BabySvcDescr { get; set; }

        public bool? SurveyComplete { get; set; }

        public bool? SingleHeadHh { get; set; }

        public int? Eighteen { get; set; }

        public int? BarCode { get; set; }

        public DateTime? TEFAPSignDate { get; set; }

        [StringLength(10)]
        public string AptNbr { get; set; }

        public bool? NeedIncomeVerification { get; set; }

        public DateTime? IncomeVerifiedDate { get; set; }

        public int? ServiceMethod { get; set; }

        public int? HDRoute { get; set; }

        public int? HDBuilding { get; set; }

        public int? HDProgram { get; set; }

        public int? HUDCategory { get; set; }

        public int? HDItem { get; set; }

        public string DriverNotes { get; set; }

        public string AlertText { get; set; }

        public DateTime? FirstCalService { get; set; }

        public DateTime? LastSupplService { get; set; }

        public int? Transportation { get; set; }

        [StringLength(50)]
        public string SchSupplyPickupPerson { get; set; }

        public DateTime? SchSupplyRegDate { get; set; }

        public bool? SchSupplyFlag { get; set; }

        public int? SchSupplyRegistration { get; set; }
    }
}
