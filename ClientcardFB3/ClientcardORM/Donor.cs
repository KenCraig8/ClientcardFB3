namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donor
    {
        public int ID { get; set; }

        public bool? Inactive { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        [StringLength(30)]
        public string CellPhone { get; set; }

        [StringLength(30)]
        public string WorkPhone { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        public short? RcdType { get; set; }

        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(50)]
        public string ContactPhone { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public bool? AutoAlert { get; set; }

        public bool? UserFlag0 { get; set; }

        public bool? UserFlag1 { get; set; }

        [StringLength(50)]
        public string Info1 { get; set; }

        [StringLength(50)]
        public string Info2 { get; set; }

        public DateTime? Date1 { get; set; }

        public DateTime? Date2 { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public short? DefaultDonationType { get; set; }
    }
}
