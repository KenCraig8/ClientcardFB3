namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FamilyCardSig")]
    public partial class FamilyCardSig
    {
        [Key]
        public int UID { get; set; }

        public int? HhID { get; set; }

        public DateTime? SigDate { get; set; }

        [StringLength(500)]
        public string DocPath { get; set; }

        public byte[] SigImage { get; set; }

        public string SigString { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
    }
}
