namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Zipcode
    {
        [Key]
        [Column("ZipCode")]
        [StringLength(10)]
        public string ZipCode1 { get; set; }

        [StringLength(64)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(3)]
        public string AreaCode { get; set; }

        [StringLength(5)]
        public string FIPS { get; set; }

        [StringLength(64)]
        public string County { get; set; }

        public bool? AllowTEFAP { get; set; }

        public int? DefaultCategory { get; set; }
    }
}
