namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HDRSClient
    {
        [Key]
        public int UID { get; set; }

        public int? RSUID { get; set; }

        public int? HHID { get; set; }

        public int? HDBuilding { get; set; }

        public int? HDProgram { get; set; }

        public int? HDItem { get; set; }

        public string ClientComments { get; set; }

        public string DriverNotes { get; set; }

        public int? Status { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBY { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
