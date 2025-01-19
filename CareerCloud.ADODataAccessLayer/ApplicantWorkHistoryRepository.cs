using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string _connectionString;

        public ApplicantWorkHistoryRepository()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DataConnection")
                                ?? throw new InvalidOperationException("Connection string 'DataConnection' not found.");
        }

        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Applicant_Work_History]
                                                              ([Id], [Applicant], [Company_Name], [Country_Code], [Location], [Job_Title], [Job_Description], [Start_Month], [Start_Year], [End_Month], [End_Year])
                                                              VALUES
                                                              (@Id, @Applicant, @Company_Name, @Country_Code, @Location, @Job_Title, @Job_Description, @Start_Month, @Start_Year, @End_Month, @End_Year)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                        cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                        cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                        cmd.Parameters.AddWithValue("@Location", item.Location);
                        cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                        cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                        cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                        cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                        cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                        cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Applicant_Work_History]
                                                              SET [Applicant] = @Applicant,
                                                                  [Company_Name] = @Company_Name,
                                                                  [Country_Code] = @Country_Code,
                                                                  [Location] = @Location,
                                                                  [Job_Title] = @Job_Title,
                                                                  [Job_Description] = @Job_Description,
                                                                  [Start_Month] = @Start_Month,
                                                                  [Start_Year] = @Start_Year,
                                                                  [End_Month] = @End_Month,
                                                                  [End_Year] = @End_Year
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                        cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                        cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                        cmd.Parameters.AddWithValue("@Location", item.Location);
                        cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                        cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                        cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                        cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                        cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                        cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[Applicant_Work_History]
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            List<ApplicantWorkHistoryPoco> pocos = new List<ApplicantWorkHistoryPoco>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Applicant_Work_History]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco
                    {
                        Id = reader.GetGuid(0),
                        Applicant = reader.GetGuid(1),
                        CompanyName = reader.GetString(2),
                        CountryCode = reader.GetString(3),
                        Location = reader.GetString(4),
                        JobTitle = reader.GetString(5),
                        JobDescription = reader.GetString(6),
                        StartMonth = reader.GetInt16(7),
                        StartYear = reader.GetInt32(8),
                        EndMonth = reader.GetInt16(9),
                        EndYear = reader.GetInt32(10)
                    };

                    pocos.Add(poco);
                }

                conn.Close();
            }

            return pocos;
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            // Implementation for CallStoredProc
            throw new NotImplementedException();
        }
    }
}