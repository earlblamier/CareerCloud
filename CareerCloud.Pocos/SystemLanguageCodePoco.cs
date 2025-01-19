using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;


namespace CareerCloud.Pocos
{
    [Table("System_Language_Codes")]
    //public class SystemLanguageCodePoco : IPoco
    public class SystemLanguageCodePoco
    {
        [Key]
        //public Guid Id { get; set; } // add for assignment 3

        [Column("LanguageID")]
        //public required string LanguageID { get; set; }
        public string LanguageID { get; set; }

        [Column("Name")]
        //public required string Name { get; set; }
        public string Name { get; set; }

        [Column("Native_Name")]
        //public required string NativeName { get; set; }
        public string NativeName { get; set; }

        // Navigation Properties
        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
    }
}