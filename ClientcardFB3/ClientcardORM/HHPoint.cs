namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HHPoint
    {
        [Key]
        public int UID { get; set; }

        public int? HhID { get; set; }

        public DateTime? WeekOf { get; set; }

        public int? Allocated { get; set; }

        public int? Pts0 { get; set; }

        public int? Pts1 { get; set; }

        public int? Pts2 { get; set; }

        public int? Pts3 { get; set; }

        public int? Pts4 { get; set; }

        public int? Pts5 { get; set; }

        public int? Pts6 { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
