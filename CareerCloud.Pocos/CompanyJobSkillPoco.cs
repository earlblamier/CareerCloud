using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Skills")]
    public class CompanyJobSkillPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Job")]
        public Guid Job { get; set; }

        [Column("Skill")]
        public string Skill { get; set; }

        [Column("Skill_Level")]
        public string SkillLevel { get; set; }

        [Column("Importance")]
        public int Importance { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; } = new byte[8];

        // Navigation Property
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}