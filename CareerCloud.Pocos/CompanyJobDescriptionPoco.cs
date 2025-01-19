using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs_Descriptions")]
    public class CompanyJobDescriptionPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Job")]
        public Guid Job { get; set; }

        [Column("Job_Name")]
        public string JobName { get; set; }

        [Column("Job_Descriptions")]
        public string JobDescriptions { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; } = new byte[8];

        // Navigation Property
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}