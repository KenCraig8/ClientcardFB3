namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MonthlyReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string ReportName { get; set; }

        public string EmailAddresses { get; set; }

        [StringLength(255)]
        public string ReportPath { get; set; }

        public bool? ReportActive { get; set; }

        public int? GroupingBy { get; set; }

        [StringLength(8)]
        public string DocType { get; set; }
    }
}
