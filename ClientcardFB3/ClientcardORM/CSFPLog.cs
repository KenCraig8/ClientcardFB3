namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CSFPLog")]
    public partial class CSFPLog
    {
        public int ID { get; set; }

        [StringLength(6)]
        public string Period { get; set; }

        public DateTime TrxDate { get; set; }

        public int MemID { get; set; }

        public int? Lbs { get; set; }

        public int? DistributionMethod { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
