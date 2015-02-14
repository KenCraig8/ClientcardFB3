namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VoucherItem
    {
        [Key]
        public int UID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? VoucherType { get; set; }

        public bool? Inactive { get; set; }

        public decimal? DefaultAmount { get; set; }

        public decimal? MaxAmount { get; set; }
    }
}
