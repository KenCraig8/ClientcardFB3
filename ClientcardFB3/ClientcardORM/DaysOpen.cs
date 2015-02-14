namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DaysOpen")]
    public partial class DaysOpen
    {
        [Key]
        public DateTime date { get; set; }

        public bool IsCommodity { get; set; }

        public string SpecialItems { get; set; }
    }
}
