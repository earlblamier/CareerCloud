using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins")]
    public class SecurityLoginPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Login")]
        //public required string Login { get; set; }
        public string Login { get; set; }

        [Column("Password")]
        //public required string Password { get; set; }
        public string Password { get; set; }

        [Column("Created_Date")]
        //public required DateTime Created { get; set; }
        public DateTime Created { get; set; }

        [Column("Password_Update_Date")]
        //public required DateTime? PasswordUpdate { get; set; }
        public DateTime? PasswordUpdate { get; set; }

        [Column("Agreement_Accepted_Date")]
        //public required DateTime? AgreementAccepted { get; set; }
        public DateTime? AgreementAccepted { get; set; }

        [Column("Is_Locked")]
        //public required bool IsLocked { get; set; }
        public bool IsLocked { get; set; }

        [Column("Is_Inactive")]
        //public required bool IsInactive { get; set; }
        public bool IsInactive { get; set; }

        [Column("Email_Address")]
        //public required string EmailAddress { get; set; }
        public string EmailAddress { get; set; }

        [Column("Phone_Number")]
        //public required string PhoneNumber { get; set; }
        public string PhoneNumber { get; set; }

        [Column("Full_Name")]
        //public required string FullName { get; set; }
        public string FullName { get; set; }

        [Column("Force_Change_Password")]
        //public required bool ForceChangePassword { get; set; }
        public bool ForceChangePassword { get; set; }

        [Column("Prefferred_Language")]
        //public required string PrefferredLanguage { get; set; }
        public string PrefferredLanguage { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        //public required byte[] TimeStamp { get; set; }
        public byte[] TimeStamp { get; set; } = new byte[8];

        // Navigation Properties
        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public virtual ICollection<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
    }
}