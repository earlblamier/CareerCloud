using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Job_Applications")]
    public class ApplicantJobApplicationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Applicant")]
        public Guid Applicant { get; set; }

        [Column("Job")]
        public Guid Job { get; set; }

        [Column("Application_Date")]
        public DateTime ApplicationDate { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        // public required byte[] TimeStamp { get; set; }
        public byte[] TimeStamp { get; set; } = Array.Empty<byte>();

        // Navigation Properties
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}