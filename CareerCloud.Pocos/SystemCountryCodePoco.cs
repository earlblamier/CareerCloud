using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    //public class SystemCountryCodePoco : IPoco
    public class SystemCountryCodePoco
    {
        [Key]
        //public Guid Id { get; set; } // add for assignment 3

        [Column("Code")]
        //public required string Code { get; set; }
        public string Code { get; set; }

        [Column("Name")]
        //public required string Name { get; set; }
        public string Name { get; set; }

        

        // Navigation Properties
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; }
        public virtual ICollection<CompanyLocationPoco> CompanyLocations { get; set; }
        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; } // Added navigation property
    }
}