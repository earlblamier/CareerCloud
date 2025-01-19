using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs")]
    public class CompanyJobPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Company")]
        public Guid Company { get; set; }

        [Column("Profile_Created")]
        public DateTime ProfileCreated { get; set; }

        [Column("Is_Inactive")]
        public bool IsInactive { get; set; }

        [Column("Is_Company_Hidden")]
        public bool IsCompanyHidden { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; } = new byte[8];

        // Navigation Properties
        public virtual CompanyProfilePoco CompanyProfile { get; set; }
        public virtual ICollection<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public virtual ICollection<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
    }
}