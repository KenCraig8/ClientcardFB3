namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VolunteerHour
    {
        public int ID { get; set; }

        public DateTime? TrxDate { get; set; }

        public int? VolId { get; set; }

        public int? NumVolunteers { get; set; }

        public float? NumVolHours { get; set; }

        [StringLength(5)]
        public string VolTimeIn { get; set; }

        [StringLength(5)]
        public string VolTimeOut { get; set; }
    }
}
