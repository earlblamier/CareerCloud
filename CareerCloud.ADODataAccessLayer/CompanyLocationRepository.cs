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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        private readonly string _connectionString;

        public CompanyLocationRepository()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DataConnection")
                                ?? throw new InvalidOperationException("Connection string 'DataConnection' not found.");
        }

        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Company_Locations]
                                                              ([Id], [Company], [Country_Code], [State_Province_Code], [Street_Address], [City_Town], [Zip_Postal_Code])
                                                              VALUES
                                                              (@Id, @Company, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Company", item.Company);
                        cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                        cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                        cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                        cmd.Parameters.AddWithValue("@City_Town", item.City);
                        cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Company_Locations]
                                                              SET [Company] = @Company,
                                                                  [Country_Code] = @Country_Code,
                                                                  [State_Province_Code] = @State_Province_Code,
                                                                  [Street_Address] = @Street_Address,
                                                                  [City_Town] = @City_Town,
                                                                  [Zip_Postal_Code] = @Zip_Postal_Code
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Company", item.Company);
                        cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                        cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                        cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                        cmd.Parameters.AddWithValue("@City_Town", item.City);
                        cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[Company_Locations]
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            List<CompanyLocationPoco> pocos = new List<CompanyLocationPoco>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Company_Locations]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco
                    {
                        Id = reader.GetGuid(0),
                        Company = reader.GetGuid(1),
                        CountryCode = reader.GetString(2),
                        Province = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Street = reader.IsDBNull(4) ? null : reader.GetString(4),
                        City = reader.IsDBNull(5) ? null : reader.GetString(5),
                        PostalCode = reader.IsDBNull(6) ? null : reader.GetString(6),
                        TimeStamp = reader.IsDBNull(7) ? null : (byte[])reader[7]
                    };

                    pocos.Add(poco);
                }

                conn.Close();
            }

            return pocos;
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
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