namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HouseholdMember
    {
        public int ID { get; set; }

        public bool? Inactive { get; set; }

        public int? HouseholdID { get; set; }

        [StringLength(70)]
        public string LastName { get; set; }

        [StringLength(70)]
        public string FirstName { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        public DateTime? Birthdate { get; set; }

        public int? AgeGroup { get; set; }

        public bool? SpecialDiet { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public bool? WorksInArea { get; set; }

        [StringLength(50)]
        public string Employer { get; set; }

        [StringLength(50)]
        public string EmpAddress { get; set; }

        [StringLength(40)]
        public string EmpCity { get; set; }

        [StringLength(10)]
        public string EmpZipcode { get; set; }

        [StringLength(30)]
        public string EmpPhone { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool? UserFlag0 { get; set; }

        public bool? UserFlag1 { get; set; }

        public bool? VolunteersAtFoodBank { get; set; }

        public int? Age { get; set; }

        public bool? UseAge { get; set; }

        public bool? NotIncludedInClientList { get; set; }

        public bool? CSFP { get; set; }

        public bool? HeadHH { get; set; }

        public int? Language { get; set; }

        public bool? IsDisabled { get; set; }

        public DateTime? CSFPExpiration { get; set; }

        public string CSFPComments { get; set; }

        public int? CSFPRoute { get; set; }

        [StringLength(50)]
        public string MemIDNbr { get; set; }

        public short? MemIDType { get; set; }

        public short? Race { get; set; }

        public bool? Hispanic { get; set; }

        public bool? BackPack { get; set; }

        public DateTime? BPExpiration { get; set; }

        public short? BPSize { get; set; }

        public int? BPSchool { get; set; }

        public string BPNotes { get; set; }

        public bool? NotCounted { get; set; }

        public int? Relationship { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }

        public int? Grade { get; set; }

        public bool? SchSupply { get; set; }

        public DateTime? SchSupplyDelivered { get; set; }

        public int? SchSupplySchool { get; set; }

        public int? CSFPStatus { get; set; }
    }
}
