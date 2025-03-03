/*
 * Assignment 5 - Assignment 5 – NET .31 - Creating a REST Service Interface for CareerCloud in .NET 8
 * 
 * Developer: Earl Lamier
 * Student Account: N01710966
 * Email: earlblamier@gmail.com
 * 
 * Course: Full Stack .NET Cloud Developer Program
 * School: Humber Polytechnic (Cohort 7)
 * Instructor: Rupinder Sandhu (REST API); Supun Kandaudahewa
 * Date: 12 February 2025
 */
using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Register EFGenericRepository and Business Logic Layer
            builder.Services.AddScoped<IDataRepository<ApplicantEducationPoco>, EFGenericRepository<ApplicantEducationPoco>>();
            builder.Services.AddScoped<ApplicantEducationLogic>();

            builder.Services.AddScoped<IDataRepository<ApplicantJobApplicationPoco>, EFGenericRepository<ApplicantJobApplicationPoco>>();
            builder.Services.AddScoped<ApplicantJobApplicationLogic>();

            builder.Services.AddScoped<IDataRepository<ApplicantProfilePoco>, EFGenericRepository<ApplicantProfilePoco>>();
            builder.Services.AddScoped<ApplicantProfileLogic>();

            builder.Services.AddScoped<IDataRepository<ApplicantResumePoco>, EFGenericRepository<ApplicantResumePoco>>();
            builder.Services.AddScoped<ApplicantResumeLogic>();

            builder.Services.AddScoped<IDataRepository<ApplicantSkillPoco>, EFGenericRepository<ApplicantSkillPoco>>();
            builder.Services.AddScoped<ApplicantSkillLogic>();

            builder.Services.AddScoped<IDataRepository<ApplicantWorkHistoryPoco>, EFGenericRepository<ApplicantWorkHistoryPoco>>();
            builder.Services.AddScoped<ApplicantWorkHistoryLogic>();

            builder.Services.AddScoped<IDataRepository<CompanyDescriptionPoco>, EFGenericRepository<CompanyDescriptionPoco>>();
            builder.Services.AddScoped<CompanyDescriptionLogic>();

            builder.Services.AddScoped<IDataRepository<CompanyJobEducationPoco>, EFGenericRepository<CompanyJobEducationPoco>>();
            builder.Services.AddScoped<CompanyJobEducationLogic>();

            builder.Services.AddScoped<IDataRepository<CompanyJobPoco>, EFGenericRepository<CompanyJobPoco>>();
            builder.Services.AddScoped<CompanyJobLogic>();

            builder.Services.AddScoped<IDataRepository<CompanyJobDescriptionPoco>, EFGenericRepository<CompanyJobDescriptionPoco>>();
            builder.Services.AddScoped<CompanyJobDescriptionLogic>();

            builder.Services.AddScoped<IDataRepository<CompanyJobSkillPoco>, EFGenericRepository<CompanyJobSkillPoco>>();
            builder.Services.AddScoped<CompanyJobSkillLogic>();

            builder.Services.AddScoped<IDataRepository<CompanyLocationPoco>, EFGenericRepository<CompanyLocationPoco>>();
            builder.Services.AddScoped<CompanyLocationLogic>();

            builder.Services.AddScoped<IDataRepository<CompanyProfilePoco>, EFGenericRepository<CompanyProfilePoco>>();
            builder.Services.AddScoped<CompanyProfileLogic>();

            builder.Services.AddScoped<IDataRepository<SecurityLoginPoco>, EFGenericRepository<SecurityLoginPoco>>();
            builder.Services.AddScoped<SecurityLoginLogic>();

            builder.Services.AddScoped<IDataRepository<SecurityLoginsLogPoco>, EFGenericRepository<SecurityLoginsLogPoco>>();
            builder.Services.AddScoped<SecurityLoginsLogLogic>();

            builder.Services.AddScoped<IDataRepository<SecurityLoginsRolePoco>, EFGenericRepository<SecurityLoginsRolePoco>>();
            builder.Services.AddScoped<SecurityLoginsRoleLogic>();

            builder.Services.AddScoped<IDataRepository<SecurityRolePoco>, EFGenericRepository<SecurityRolePoco>>();
            builder.Services.AddScoped<SecurityRoleLogic>();

            builder.Services.AddScoped<IDataRepository<SystemCountryCodePoco>, EFGenericRepository<SystemCountryCodePoco>>();
            builder.Services.AddScoped<SystemCountryCodeLogic>();

            builder.Services.AddScoped<IDataRepository<SystemLanguageCodePoco>, EFGenericRepository<SystemLanguageCodePoco>>();
            builder.Services.AddScoped<SystemLanguageCodeLogic>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // added azure data migration Feb 18, 2025
            //Adding the db context through dependency injection
            var connectionString = builder.Configuration.GetConnectionString("CareerCloudConString");

            builder.Services.AddDbContext<CareerCloudContext>(options =>
            {
                options.UseSqlServer(connectionString!);
            });

            //Adding CORS
            builder.Services.AddCors(p => p.AddPolicy("CorsPolicy1", build =>
            {
                build.AllowAnyOrigin().WithMethods("GET", "POST").AllowAnyHeader();
            }));
            // end here of addition update code

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy1");

            app.UseHttpsRedirection();


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
