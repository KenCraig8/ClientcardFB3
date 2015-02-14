namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserField
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string TableName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string FldName { get; set; }

        [StringLength(8)]
        public string ControlType { get; set; }

        [StringLength(50)]
        public string EditLabel { get; set; }

        [StringLength(255)]
        public string EditTip { get; set; }

        public bool? AutoAlert { get; set; }

        [StringLength(50)]
        public string AutoAlertText { get; set; }
    }
}
