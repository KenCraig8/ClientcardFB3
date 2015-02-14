namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class parm_EducationLevel
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public int? SortOrder { get; set; }

        [StringLength(4)]
        public string ShortName { get; set; }
    }
}
