using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class ProducerRepository : RepositoryBase
    {
        public List<Producer> GetAllProducers()
        {
            List<Producer> producers = new List<Producer>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Producatori WHERE IsActive = 1"; // Selectați doar producătorii activi
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producer producer = new Producer
                            {
                                ID = reader["ProducatorID"].ToString(),
                                Name = reader["Nume"].ToString(),
                                CountryOrigin = reader["TaraOrigine"].ToString()
                            };
                            producers.Add(producer);
                        }
                    }
                }
            }

            return producers;
        }

     
        public void AddProducer(Producer producer)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO Producatori (Nume, TaraOrigine, IsActive) VALUES (@Name, @CountryOrigin, @IsActive)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Verificați dacă Name este null și atribuiți DBNull.Value în acest caz
                    command.Parameters.AddWithValue("@Name", (object)producer.Name ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CountryOrigin", producer.CountryOrigin);
                    command.Parameters.AddWithValue("@IsActive", 1); // Setați inițial ca activ
                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateProducer(Producer producer)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "UPDATE Producatori SET Nume = @Name, TaraOrigine = @CountryOrigin WHERE ProducatorID = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", producer.Name);
                    command.Parameters.AddWithValue("@CountryOrigin", producer.CountryOrigin);
                    command.Parameters.AddWithValue("@ID", int.Parse(producer.ID));
                    command.ExecuteNonQuery();
                }
            }
        }

        public string GetProducerIdByName(string producerName)
        {
            string producerId = null;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT ProducatorID FROM Producatori WHERE Nume = @Name AND IsActive = 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", producerName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        producerId = result.ToString();
                    }
                }
            }

            return producerId;
        }

        public void DeleteProducer(string producerId)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "UPDATE Producatori SET IsActive = 0 WHERE ProducatorID = @ID"; 
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", int.Parse(producerId));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
