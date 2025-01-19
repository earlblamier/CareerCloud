using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobEducationRepository : IDataRepository<CompanyJobEducationPoco>
    {
        private readonly string _connectionString;

        public CompanyJobEducationRepository()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DataConnection")
                                ?? throw new InvalidOperationException("Connection string 'DataConnection' not found.");
        }

        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Company_Job_Educations]
                                                              ([Id], [Job], [Major], [Importance])
                                                              VALUES
                                                              (@Id, @Job, @Major, @Importance)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Job", item.Job);
                        cmd.Parameters.AddWithValue("@Major", item.Major);
                        cmd.Parameters.AddWithValue("@Importance", item.Importance);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Company_Job_Educations]
                                                              SET [Job] = @Job,
                                                                  [Major] = @Major,
                                                                  [Importance] = @Importance
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Job", item.Job);
                        cmd.Parameters.AddWithValue("@Major", item.Major);
                        cmd.Parameters.AddWithValue("@Importance", item.Importance);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[Company_Job_Educations]
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Company_Job_Educations]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CompanyJobEducationPoco poco = new CompanyJobEducationPoco
                    {
                        Id = reader.GetGuid(0),
                        Job = reader.GetGuid(1),
                        Major = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Importance = reader.GetInt16(3),
                        TimeStamp = reader.IsDBNull(4) ? null : (byte[])reader[4]
                    };

                    pocos.Add(poco);
                }

                conn.Close();
            }

            return pocos;
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(name, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Item1, param.Item2);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}