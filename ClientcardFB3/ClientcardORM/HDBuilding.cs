namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HDBuilding
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string BldgName { get; set; }

        [StringLength(50)]
        public string BldgAddress { get; set; }

        [StringLength(50)]
        public string BldgCity { get; set; }

        [StringLength(2)]
        public string BldgState { get; set; }

        [StringLength(10)]
        public string BldgZip { get; set; }

        [StringLength(20)]
        public string BldgOperator { get; set; }

        [StringLength(10)]
        public string ContactName { get; set; }

        [StringLength(15)]
        public string ContactPhone { get; set; }

        [StringLength(10)]
        public string ContactAptNbr { get; set; }

        [StringLength(50)]
        public string ContactEmail { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
