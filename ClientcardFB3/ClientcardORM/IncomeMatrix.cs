namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IncomeMatrix")]
    public partial class IncomeMatrix
    {
        public int ID { get; set; }

        public int? IncomeGroup { get; set; }

        [StringLength(50)]
        public string Label1 { get; set; }

        [StringLength(50)]
        public string Label2 { get; set; }

        [StringLength(50)]
        public string Label3 { get; set; }

        public int? IncomeLow1 { get; set; }

        public int? IncomeLow2 { get; set; }

        public int? IncomeLow3 { get; set; }

        public int? IncomeLow4 { get; set; }

        public int? IncomeLow5 { get; set; }

        public int? IncomeLow6 { get; set; }

        public int? IncomeLow7 { get; set; }

        public int? IncomeLow8 { get; set; }

        public int? IncomeLow9 { get; set; }

        public int? IncomeLow10 { get; set; }

        public int? IncomeHi1 { get; set; }

        public int? IncomeHi2 { get; set; }

        public int? IncomeHi3 { get; set; }

        public int? IncomeHi4 { get; set; }

        public int? IncomeHi5 { get; set; }

        public int? IncomeHi6 { get; set; }

        public int? IncomeHi7 { get; set; }

        public int? IncomeHi8 { get; set; }

        public int? IncomeHi9 { get; set; }

        public int? IncomeHi10 { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
