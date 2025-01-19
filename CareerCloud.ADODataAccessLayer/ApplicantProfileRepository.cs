using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private readonly string? _connectionString;

        public ApplicantProfileRepository()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DataConnection")
                                ?? throw new InvalidOperationException("Connection string 'DataConnection' not found.");
        }

        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                                        ([Id], [Login], [Current_Salary], [Current_Rate], [Currency], [Country_Code], [State_Province_Code], [Street_Address], [City_Town], [Zip_Postal_Code])
                                        VALUES
                                        (@Id, @Login, @Current_Salary, @Current_Rate, @Currency, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code)";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    // cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode); // assignment 4 changes
                    cmd.Parameters.AddWithValue("@Country_Code", item.Country); // assignment 4 changes
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                        SET [Login] = @Login,
                                            [Current_Salary] = @Current_Salary,
                                            [Current_Rate] = @Current_Rate,
                                            [Currency] = @Currency,
                                            [Country_Code] = @Country_Code,
                                            [State_Province_Code] = @State_Province_Code,
                                            [Street_Address] = @Street_Address,
                                            [City_Town] = @City_Town,
                                            [Zip_Postal_Code] = @Zip_Postal_Code
                                        WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    //cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode); // assignment 4 changes
                    cmd.Parameters.AddWithValue("@Country_Code", item.Country); // assignment 4 changes
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Profiles]
                                        WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Applicant_Profiles]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco
                    {
                        Id = reader.GetGuid(0),
                        Login = reader.GetGuid(1),
                        CurrentSalary = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2),
                        CurrentRate = reader.IsDBNull(3) ? (decimal?)null : reader.GetDecimal(3),
                        Currency = reader.IsDBNull(4) ? null : reader.GetString(4),
                        //CountryCode = reader.IsDBNull(5) ? null : reader.GetString(5), // assignment 4 changes
                        Country = reader.IsDBNull(5) ? null : reader.GetString(5), // assignment 4 changes
                        Province = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Street = reader.IsDBNull(7) ? null : reader.GetString(7),
                        City = reader.IsDBNull(8) ? null : reader.GetString(8),
                        PostalCode = reader.IsDBNull(9) ? null : reader.GetString(9)
                    };

                    pocos.Add(poco);
                }

                conn.Close();
            }

            return pocos;
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            // Implementation for GetList
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            // Implementation for CallStoredProc
            throw new NotImplementedException();
        }
    }
}