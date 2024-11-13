using System;
using System.Collections.Generic;
using WPF_LoginForm.Models;
using System.Data.SqlClient;

namespace WPF_LoginForm.Repositories
{
    public class CategoryRepository : RepositoryBase
    {
        // Metoda pentru a obține toate categoriile din baza de date
        public List<CategoryModel> GetAllCategories()
        {
            List<CategoryModel> categories = new List<CategoryModel>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                // Query pentru a obține toate categoriile și prețul total al produselor din fiecare categorie
                string query = @"
            SELECT c.CategorieID, c.Nume, SUM(s.Cantitate * s.PretVanzare) AS TotalPrice
            FROM Categorii c
            LEFT JOIN Produse p ON c.CategorieID = p.CategorieID
            LEFT JOIN Stocuri s ON p.ProdusID = s.ProdusID
            GROUP BY c.CategorieID, c.Nume";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CategoryModel category = new CategoryModel
                            {
                                ID = reader["CategorieID"].ToString(),
                                Name = reader["Nume"].ToString(),
                                Price = reader.IsDBNull(reader.GetOrdinal("TotalPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalPrice"))
                            };
                            categories.Add(category);
                        }
                    }
                }
            }

            return categories;
        }



        // Metoda pentru a adăuga o nouă categorie în baza de date
        public void AddCategory(CategoryModel category)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO Categorii (Nume) VALUES (@Name)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", category.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Metoda pentru a actualiza o categorie existentă în baza de date
        public void UpdateCategory(CategoryModel category)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "UPDATE Categorii SET Nume = @Name WHERE CategorieID = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", category.Name);
                    command.Parameters.AddWithValue("@ID", int.Parse(category.ID));
                    command.ExecuteNonQuery();
                }
            }
        }

        // Metoda pentru a calcula bugetul total
        public decimal GetTotalBudget()
        {
            decimal totalBudget = 0;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                // Query pentru a calcula suma tuturor prețurilor de vânzare ale produselor
                string query = @"
            SELECT SUM(s.Cantitate * s.PretVanzare) AS TotalBudget
            FROM Stocuri s";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    totalBudget = (decimal)command.ExecuteScalar();
                }
            }

            return totalBudget;
        }
    }


}
