namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class parm_SurveyFields
    {
        public short ID { get; set; }

        [StringLength(30)]
        public string FldName { get; set; }

        public int? SortOrder { get; set; }

        [StringLength(50)]
        public string Prompt { get; set; }

        public int? CtrlType { get; set; }

        [StringLength(50)]
        public string CtrlSource { get; set; }
    }
}
