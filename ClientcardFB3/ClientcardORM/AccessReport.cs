namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AccessReport
    {
        public int ID { get; set; }

        [StringLength(24)]
        public string RptGroup { get; set; }

        [StringLength(50)]
        public string Grouping { get; set; }

        [Column("Display Name")]
        [StringLength(255)]
        public string Display_Name { get; set; }

        [StringLength(255)]
        public string ReportTitle { get; set; }

        public int? DateRangeType { get; set; }

        [StringLength(64)]
        public string Date0 { get; set; }

        [StringLength(64)]
        public string Date1 { get; set; }

        public bool? UseWhere { get; set; }

        [StringLength(255)]
        public string WhereClause { get; set; }

        public string SQLQuery { get; set; }

        [StringLength(50)]
        public string LabelLowDate { get; set; }

        [StringLength(50)]
        public string LabelHiDate { get; set; }

        public bool? Preview { get; set; }

        public bool? UseActive { get; set; }

        public bool? AllowBlank { get; set; }

        public short? CboResultType { get; set; }

        public bool? UseFilter { get; set; }

        [StringLength(255)]
        public string FilterName { get; set; }

        public int? Orientation { get; set; }

        public double? MarginTop { get; set; }

        public double? MarginBottom { get; set; }

        public double? MarginLeft { get; set; }

        public double? MarginRight { get; set; }

        public int? NbrCopies { get; set; }
    }
}
