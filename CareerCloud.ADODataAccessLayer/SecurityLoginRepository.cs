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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        private readonly string _connectionString;

        public SecurityLoginRepository()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DataConnection")
                                ?? throw new InvalidOperationException("Connection string 'DataConnection' not found.");
        }

        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Security_Logins]
                                                              ([Id], [Login], [Password], [Created_Date], [Password_Update_Date], [Agreement_Accepted_Date], [Is_Locked], [Is_Inactive], [Email_Address], [Phone_Number], [Full_Name], [Force_Change_Password], [Prefferred_Language])
                                                              VALUES
                                                              (@Id, @Login, @Password, @Created_Date, @Password_Update_Date, @Agreement_Accepted_Date, @Is_Locked, @Is_Inactive, @Email_Address, @Phone_Number, @Full_Name, @Force_Change_Password, @Prefferred_Language)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Login", item.Login);
                        cmd.Parameters.AddWithValue("@Password", item.Password);
                        cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                        cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                        cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                        cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                        cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                        cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                        cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                        cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                        cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Security_Logins]
                                                              SET [Login] = @Login,
                                                                  [Password] = @Password,
                                                                  [Created_Date] = @Created_Date,
                                                                  [Password_Update_Date] = @Password_Update_Date,
                                                                  [Agreement_Accepted_Date] = @Agreement_Accepted_Date,
                                                                  [Is_Locked] = @Is_Locked,
                                                                  [Is_Inactive] = @Is_Inactive,
                                                                  [Email_Address] = @Email_Address,
                                                                  [Phone_Number] = @Phone_Number,
                                                                  [Full_Name] = @Full_Name,
                                                                  [Force_Change_Password] = @Force_Change_Password,
                                                                  [Prefferred_Language] = @Prefferred_Language
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Login", item.Login);
                        cmd.Parameters.AddWithValue("@Password", item.Password);
                        cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                        cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                        cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                        cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                        cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                        cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                        cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                        cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                        cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[Security_Logins]
                                                              WHERE [Id] = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Security_Logins]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco
                    {
                        Id = reader.GetGuid(0),
                        Login = reader.GetString(1),
                        Password = reader.GetString(2),
                        Created = reader.GetDateTime(3),
                        PasswordUpdate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                        AgreementAccepted = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                        IsLocked = reader.GetBoolean(6),
                        IsInactive = reader.GetBoolean(7),
                        EmailAddress = reader.GetString(8),
                        PhoneNumber = reader.IsDBNull(9) ? null : reader.GetString(9),
                        FullName = reader.IsDBNull(10) ? null : reader.GetString(10),
                        ForceChangePassword = reader.GetBoolean(11),
                        PrefferredLanguage = reader.IsDBNull(12) ? null : reader.GetString(12),
                        TimeStamp = reader.IsDBNull(13) ? null : (byte[])reader[13]
                    };

                    pocos.Add(poco);
                }

                conn.Close();
            }

            return pocos;
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
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