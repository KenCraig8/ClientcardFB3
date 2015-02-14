namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HDRoute
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string RouteTitle { get; set; }

        public int? DeliveryDOW { get; set; }

        public int? DeliveryCycle { get; set; }

        public bool? InActive { get; set; }

        public int? DefaultDriver { get; set; }

        public string Notes { get; set; }

        public decimal? EstHours { get; set; }

        public decimal? EstMiles { get; set; }

        public string DriverNotes { get; set; }

        [StringLength(50)]
        public string FBContact { get; set; }

        [StringLength(20)]
        public string FBContactPhone { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
