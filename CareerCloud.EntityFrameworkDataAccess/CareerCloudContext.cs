/*
 * Assignment 4 - Developing Data Access with Entity Framework Core in Applications
 * 
 * 
 * Developer: Earl Lamier
 * Student Account: N01710966
 * Email: earlblamier@gmail.com
 * 
 * Course: Full Stack .NET Cloud Developer
 * School: Humber Polytechnic
 * Instructor: 
 * Date: 15-18 January 2025
 */
using Microsoft.EntityFrameworkCore;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        // Default constructor for backward compatibility
        public CareerCloudContext()
        {
        }

        // Constructor accepting DbContextOptions
        public CareerCloudContext(DbContextOptions<CareerCloudContext> options) : base(options) { }

        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        // Configures DbContext for migrations by specifying design-time settings.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure only if the options have not been previously set (differentiating between design-time and runtime).
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(config.GetConnectionString("DataConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // List of entities with TimeStamp property 
            var entitiesWithTimestamp = new[]
            {
                typeof(ApplicantJobApplicationPoco),
                typeof(ApplicantEducationPoco),
                typeof(ApplicantWorkHistoryPoco),
                typeof(ApplicantProfilePoco),
                typeof(ApplicantSkillPoco),
                typeof(CompanyDescriptionPoco),
                typeof(CompanyJobDescriptionPoco),
                typeof(CompanyJobEducationPoco),
                typeof(CompanyJobPoco),
                typeof(CompanyJobSkillPoco),
                typeof(CompanyLocationPoco),
                typeof(CompanyProfilePoco),
                typeof(SecurityLoginPoco),
                typeof(SecurityLoginsRolePoco)
            };

            // Loop to ignore TimeStamp and apply relationships; can be improved later
            foreach (var entityType in entitiesWithTimestamp)
            {
                modelBuilder.Entity(entityType)
                    .Ignore("TimeStamp"); // Ignore TimeStamp property
            }

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(ap => ap.SecurityLogin)
                .WithMany(sl => sl.ApplicantProfiles)
                .HasForeignKey(ap => ap.Login);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(ap => ap.SystemCountryCode) // Added this line
                .WithMany(scc => scc.ApplicantProfiles)
                .HasForeignKey(ap => ap.Country); // Ensure this matches the foreign key property

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(awh => awh.ApplicantProfile)
                .WithMany(ap => ap.ApplicantWorkHistorys)
                .HasForeignKey(awh => awh.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(awh => awh.SystemCountryCode)
                .WithMany(scc => scc.ApplicantWorkHistorys)
                .HasForeignKey(awh => awh.CountryCode);


            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(cl => cl.CompanyProfile)
                .WithMany(cp => cp.CompanyLocations)
                .HasForeignKey(cl => cl.Company);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(cl => cl.SystemCountryCode)
                .WithMany(scc => scc.CompanyLocations)
                .HasForeignKey(cl => cl.CountryCode);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(cd => cd.CompanyProfile)
                .WithMany(cp => cp.CompanyDescriptions)
                .HasForeignKey(cd => cd.Company);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(cd => cd.SystemLanguageCode)
                .WithMany(sl => sl.CompanyDescriptions)
                .HasForeignKey(cd => cd.LanguageId);


            /* no systemcountrycodes */


            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(ae => ae.ApplicantProfile)
                .WithMany(ap => ap.ApplicantEducations)
                .HasForeignKey(ae => ae.Applicant); // changed from ApplicantID to Applicant to match the database schema assignment 4

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(aja => aja.ApplicantProfile)
                .WithMany(ap => ap.ApplicantJobApplications)
                .HasForeignKey(aja => aja.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(aja => aja.CompanyJob)
                .WithMany(cj => cj.ApplicantJobApplications)
                .HasForeignKey(aja => aja.Job);

            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(ar => ar.ApplicantProfile)
                .WithMany(ap => ap.ApplicantResumes)
                .HasForeignKey(ar => ar.Applicant);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .HasOne(a => a.ApplicantProfile)
                .WithMany(ap => ap.ApplicantSkills)
                .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(cj => cj.CompanyProfile)
                .WithMany(cp => cp.CompanyJobs)
                .HasForeignKey(cj => cj.Company);

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .HasOne(cjd => cjd.CompanyJob)
                .WithMany(cj => cj.CompanyJobDescriptions)
                .HasForeignKey(cjd => cjd.Job);

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .HasOne(cje => cje.CompanyJob)
                .WithMany(cj => cj.CompanyJobEducations)
                .HasForeignKey(cje => cje.Job);

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .HasOne(cjs => cjs.CompanyJob)
                .WithMany(cj => cj.CompanyJobSkills)
                .HasForeignKey(cjs => cjs.Job);



            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(cp => cp.CompanyLocations)
                .WithOne(cl => cl.CompanyProfile)
                .HasForeignKey(cl => cl.Company);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(sll => sll.SecurityLogin)
                .WithMany(sl => sl.SecurityLoginsLogs)
                .HasForeignKey(sll => sll.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(slr => slr.SecurityLogin)
                .WithMany(sl => sl.SecurityLoginsRoles)
                .HasForeignKey(slr => slr.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(slr => slr.SecurityRole)
                .WithMany(sr => sr.SecurityLoginsRoles)
                .HasForeignKey(slr => slr.Role);
        }
    }
}