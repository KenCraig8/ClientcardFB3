namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BackPackLog")]
    public partial class BackPackLog
    {
        [Key]
        public int UID { get; set; }

        public int BPSchool { get; set; }

        public DateTime BackPackSvcDate { get; set; }

        public int MemID { get; set; }

        public int SvcStatus { get; set; }

        public int? Lbs { get; set; }

        public string BPNotes { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
