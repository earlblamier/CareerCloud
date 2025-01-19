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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        private readonly string _connectionString;

        public CompanyJobRepository()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DataConnection")
                                ?? throw new InvalidOperationException("Connection string 'DataConnection' not found.");
        }

        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Company_Jobs]
                                                              ([Id], [Company], [Profile_Created], [Is_Inactive], [Is_Company_Hidden])
                                                              VALUES
                                                              (@Id, @Company, @Profile_Created, @Is_Inactive, @Is_Company_Hidden)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Company", item.Company);
                        cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                        cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                        cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Company_Jobs]
                                                              SET [Company] = @Company,
                                                                  [Profile_Created] = @Profile_Created,
                                                                  [Is_Inactive] = @Is_Inactive,
                                                                  [Is_Company_Hidden] = @Is_Company_Hidden
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Company", item.Company);
                        cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                        cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                        cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[Company_Jobs]
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Company_Jobs]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco
                    {
                        Id = reader.GetGuid(0),
                        Company = reader.GetGuid(1),
                        ProfileCreated = reader.GetDateTime(2),
                        IsInactive = reader.GetBoolean(3),
                        IsCompanyHidden = reader.GetBoolean(4),
                        TimeStamp = reader.IsDBNull(5) ? null : (byte[])reader[5]
                    };

                    pocos.Add(poco);
                }

                conn.Close();
            }

            return pocos;
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
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