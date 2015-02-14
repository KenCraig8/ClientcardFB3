namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FBJobsActual
    {
        public int ID { get; set; }

        public DateTime? JobDate { get; set; }

        public int? JobPlanId { get; set; }

        public int? VolId { get; set; }

        [StringLength(10)]
        public string ShiftStart { get; set; }

        [StringLength(10)]
        public string ShiftEnd { get; set; }

        public float? VolHours { get; set; }

        public short? Status { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime? TimePosted { get; set; }

        [StringLength(50)]
        public string TimePostedBy { get; set; }
    }
}
