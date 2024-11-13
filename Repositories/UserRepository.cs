using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }


        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [Utilizatori] where NumeUtilizator=@username and [Parola]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [Utilizatori] SET TipUtilizator = @typeUser WHERE UtilizatorID = @id";
                command.Parameters.Add("@typeUser", SqlDbType.NVarChar).Value = userModel.TypeUser;
                command.Parameters.Add("@id", SqlDbType.Int).Value = userModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<UserModel> GetByAll()
        {
            var users = new List<UserModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Utilizatori] WHERE TipUtilizator != 'Inactive'";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new UserModel()
                        {
                            Id = reader["UtilizatorID"].ToString(),
                            Username = reader["NumeUtilizator"].ToString(),
                            Password = reader["Parola"].ToString(),
                            TypeUser = reader["TipUtilizator"].ToString()
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public IEnumerable<UserModel> GetAllCashiers()
        {
            var users = new List<UserModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Utilizatori] WHERE TipUtilizator = 'casier' AND TipUtilizator != 'Inactive'";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new UserModel()
                        {
                            Id = reader["UtilizatorID"].ToString(),
                            Username = reader["NumeUtilizator"].ToString(),
                            Password = reader["Parola"].ToString(),
                            TypeUser = reader["TipUtilizator"].ToString()
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }


        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [Utilizatori] where NumeUtilizator=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            TypeUser = reader[2].ToString(),
                        };
                    }
                }
            }
            return user;
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public string GetUserRole(string username)
        {
            string role = string.Empty;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT TipUtilizator FROM [Utilizatori] WHERE NumeUtilizator=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        role = reader["TipUtilizator"].ToString();
                    }
                }
            }
            return role;
        }

        public string GetUsernameById(int id)
        {
            string username = string.Empty;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT NumeUtilizator FROM [Utilizatori] WHERE UtilizatorID = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        username = reader["NumeUtilizator"].ToString();
                    }
                }
            }
            return username;
        }

    }
}
