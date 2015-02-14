namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HDLbsPerService")]
    public partial class HDLbsPerService
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? LbsStd { get; set; }

        public int? LbsOther { get; set; }

        public int? LbsCommodity { get; set; }

        public int? LbsSupplemental { get; set; }

        public int? LbsNonFood { get; set; }

        public int? LbsBabySvc { get; set; }
    }
}
