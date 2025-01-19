using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Resumes")]
    public class ApplicantResumePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Applicant")]
        public Guid Applicant { get; set; }

        [Column("Resume")]
        public required string Resume { get; set; }

        [Column("Last_Updated")]
        public DateTime? LastUpdated { get; set; }

        // Navigation Property
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}