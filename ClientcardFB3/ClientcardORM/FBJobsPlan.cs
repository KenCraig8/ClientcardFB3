namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FBJobsPlan")]
    public partial class FBJobsPlan
    {
        [Key]
        public int PlanID { get; set; }

        public int? WeekDay { get; set; }

        public int? JobID { get; set; }

        [StringLength(50)]
        public string JobTitle { get; set; }

        [StringLength(10)]
        public string ShiftStart { get; set; }

        [StringLength(10)]
        public string ShiftEnd { get; set; }

        public int? VolIdPrimary { get; set; }

        public int? VolIdBackup { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
