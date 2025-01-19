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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        private readonly string _connectionString;

        public SystemLanguageCodeRepository()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DataConnection")
                                ?? throw new InvalidOperationException("Connection string 'DataConnection' not found.");
        }

        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[System_Language_Codes]
                                                              ([LanguageID], [Name], [Native_Name])
                                                              VALUES
                                                              (@LanguageID, @Name, @Native_Name)", conn))
                    {
                        cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[System_Language_Codes]
                                                              SET [Name] = @Name,
                                                                  [Native_Name] = @Native_Name
                                                              WHERE [LanguageID] = @LanguageID", conn))
                    {
                        cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[System_Language_Codes]
                                                              WHERE [LanguageID] = @LanguageID", conn))
                    {
                        cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[System_Language_Codes]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco
                    {
                        LanguageID = reader.GetString(0),
                        Name = reader.GetString(1),
                        NativeName = reader.GetString(2)
                    };

                    pocos.Add(poco);
                }

                conn.Close();
            }

            return pocos;
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
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