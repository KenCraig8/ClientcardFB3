namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrxLogSig")]
    public partial class TrxLogSig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrxID { get; set; }

        public int HouseholdID { get; set; }

        public byte[] SigImage { get; set; }

        public string SigString { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
    }
}
