namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmailRecipient
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string RecipientName { get; set; }

        [StringLength(255)]
        public string EmailAddress { get; set; }

        public string Reports { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string CreatedPC { get; set; }

        [StringLength(50)]
        public string ModifiedPC { get; set; }
    }
}
