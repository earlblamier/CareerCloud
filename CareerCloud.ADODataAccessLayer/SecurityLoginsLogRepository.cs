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
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        private readonly string _connectionString;

        public SecurityLoginsLogRepository()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DataConnection")
                                ?? throw new InvalidOperationException("Connection string 'DataConnection' not found.");
        }

        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Security_Logins_Log]
                                                              ([Id], [Login], [Source_IP], [Logon_Date], [Is_Succesful])
                                                              VALUES
                                                              (@Id, @Login, @Source_IP, @Logon_Date, @Is_Succesful)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Login", item.Login);
                        cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                        cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                        cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Security_Logins_Log]
                                                              SET [Login] = @Login,
                                                                  [Source_IP] = @Source_IP,
                                                                  [Logon_Date] = @Logon_Date,
                                                                  [Is_Succesful] = @Is_Succesful
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Login", item.Login);
                        cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                        cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                        cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[Security_Logins_Log]
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Security_Logins_Log]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco
                    {
                        Id = reader.GetGuid(0),
                        Login = reader.GetGuid(1),
                        SourceIP = reader.GetString(2),
                        LogonDate = reader.GetDateTime(3),
                        IsSuccesful = reader.GetBoolean(4)
                    };

                    pocos.Add(poco);
                }

                conn.Close();
            }

            return pocos;
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
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