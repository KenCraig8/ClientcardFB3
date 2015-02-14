namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SignaturePrompt
    {
        [Key]
        public int UID { get; set; }

        public int? PromptGroup { get; set; }

        [StringLength(255)]
        public string PromptText { get; set; }

        [StringLength(16)]
        public string RightButtonText { get; set; }
    }
}
