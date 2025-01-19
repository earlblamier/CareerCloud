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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        private readonly string _connectionString;

        public CompanyProfileRepository()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DataConnection")
                                ?? throw new InvalidOperationException("Connection string 'DataConnection' not found.");
        }

        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Company_Profiles]
                                                              ([Id], [Registration_Date], [Company_Website], [Contact_Phone], [Contact_Name], [Company_Logo])
                                                              VALUES
                                                              (@Id, @Registration_Date, @Company_Website, @Contact_Phone, @Contact_Name, @Company_Logo)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                        cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                        cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                        cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                        cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Company_Profiles]
                                                              SET [Registration_Date] = @Registration_Date,
                                                                  [Company_Website] = @Company_Website,
                                                                  [Contact_Phone] = @Contact_Phone,
                                                                  [Contact_Name] = @Contact_Name,
                                                                  [Company_Logo] = @Company_Logo
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                        cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                        cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                        cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                        cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[Company_Profiles]
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            List<CompanyProfilePoco> pocos = new List<CompanyProfilePoco>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Company_Profiles]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco
                    {
                        Id = reader.GetGuid(0),
                        RegistrationDate = reader.GetDateTime(1),
                        CompanyWebsite = reader.IsDBNull(2) ? null : reader.GetString(2),
                        ContactPhone = reader.GetString(3),
                        ContactName = reader.IsDBNull(4) ? null : reader.GetString(4),
                        CompanyLogo = reader.IsDBNull(5) ? null : (byte[])reader[5],
                        TimeStamp = reader.IsDBNull(6) ? null : (byte[])reader[6]
                    };

                    pocos.Add(poco);
                }

                conn.Close();
            }

            return pocos;
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
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