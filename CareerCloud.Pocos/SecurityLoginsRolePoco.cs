using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Roles")]
    public class SecurityLoginsRolePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Login")]
        public Guid Login { get; set; }

        [Column("Role")]
        public Guid Role { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        //public required byte[] TimeStamp { get; set; }
        public byte[] TimeStamp { get; set; } = new byte[8];

        // Navigation Properties
        public virtual SecurityLoginPoco SecurityLogin { get; set; }
        public virtual SecurityRolePoco SecurityRole { get; set; }
    }
}