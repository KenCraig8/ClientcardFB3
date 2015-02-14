namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("zData")]
    public partial class zData
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string DBVersion { get; set; }

        [StringLength(50)]
        public string ExeVersion { get; set; }

        [StringLength(100)]
        public string LicensedTo { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }
    }
}
