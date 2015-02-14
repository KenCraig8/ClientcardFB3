namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IncomeGroup
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(20)]
        public string ShortName { get; set; }

        public string Notes { get; set; }

        public DateTime? AsOfDate { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public int? ProcessId { get; set; }
    }
}
