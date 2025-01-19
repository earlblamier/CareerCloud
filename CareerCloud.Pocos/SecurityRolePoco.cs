// using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Security_Roles")]
    public class SecurityRolePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Role")]
        //public required string Role { get; set; }
        public string Role { get; set; }

        [Column("Is_Inactive")]
        public bool IsInactive { get; set; }

        // Navigation Property
        public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
    }
}