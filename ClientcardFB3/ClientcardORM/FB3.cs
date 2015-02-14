namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FB3
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DBVersion { get; set; }

        [StringLength(20)]
        public string ExeVersion { get; set; }

        public string Comment { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }
    }
}
