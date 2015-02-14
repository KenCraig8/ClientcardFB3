namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Default
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FldName { get; set; }

        [StringLength(255)]
        public string FldVal { get; set; }

        [StringLength(50)]
        public string EditForm { get; set; }

        [StringLength(50)]
        public string EditLabel { get; set; }

        [StringLength(255)]
        public string EditTip { get; set; }

        [StringLength(128)]
        public string FldType { get; set; }

        [StringLength(8)]
        public string ControlType { get; set; }

        public int? ControlWidth { get; set; }
    }
}
