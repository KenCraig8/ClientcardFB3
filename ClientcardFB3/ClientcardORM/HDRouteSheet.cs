namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HDRouteSheet")]
    public partial class HDRouteSheet
    {
        public int ID { get; set; }

        public DateTime? TrxDate { get; set; }

        public int? HDRoute { get; set; }

        public int? RouteStatus { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? VolId { get; set; }

        public decimal? NbrHours { get; set; }

        public string DriverNotes { get; set; }

        public decimal? ActMiles { get; set; }

        [StringLength(50)]
        public string FBContact { get; set; }

        [StringLength(20)]
        public string FBContactPhone { get; set; }

        [StringLength(1000)]
        public string BagDescr { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
