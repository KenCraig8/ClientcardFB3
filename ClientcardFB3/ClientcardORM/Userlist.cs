namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Userlist")]
    public partial class Userlist
    {
        public int ID { get; set; }

        [Required]
        [StringLength(16)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? LastLogon { get; set; }

        [StringLength(50)]
        public string UserRole { get; set; }
    }
}
