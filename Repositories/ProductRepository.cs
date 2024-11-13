using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;
using static WPF_LoginForm.Repositories.ProductRepository;

namespace WPF_LoginForm.Repositories
{
    public class ProductRepository : RepositoryBase
    {
        public string GetNumeProdus(int produsId)
        {
            string numeProdus = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT NumeProdus FROM Produse WHERE ProdusID = @ProdusID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ProdusID", SqlDbType.Int).Value = produsId;
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        numeProdus = result.ToString();
                    }
                }
            }
            return numeProdus;
        }



        public List<Product> GetProductsByProducer(string producerId)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = @"SELECT 
                                    Produse.ProdusID, 
                                    Produse.Nume AS NumeProdus, 
                                    Categorii.Nume AS NumeCategorie,
                                    Producatori.Nume AS NumeProducator
                                FROM 
                                    Produse
                                JOIN 
                                    Categorii ON Produse.CategorieID = Categorii.CategorieID
                                JOIN 
                                    Producatori ON Produse.ProducatorID = Producatori.ProducatorID
                                WHERE 
                                    Producatori.ProducatorID = @ProducerID
                                ORDER BY 
                                    Producatori.Nume, Categorii.Nume";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProducerID", producerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ID = reader["ProdusID"].ToString(),
                                Name = reader["NumeProdus"].ToString(),
                                CategoryName = reader["NumeCategorie"].ToString(),

                            });
                        }
                    }
                }
            }

            return products;
        }
        public void AddProduct(ProductsModel product)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "INSERT INTO Produse (Nume, CodBare, CategorieID, ProducatorID) VALUES (@Nume, @CodBare, @CategorieID, @ProducatorID)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nume", product.Name);
                    command.Parameters.AddWithValue("@CodBare", product.CodBare);
                    command.Parameters.AddWithValue("@CategorieID", product.CategoryID);
                    command.Parameters.AddWithValue("@ProducatorID", product.ProducersID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }




        public int GetProductIdByName(string name)
        {
            int productId = -1; // Returnează -1 dacă nu se găsește niciun produs cu numele dat

            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT ProdusID FROM Produse WHERE Nume = @Nume";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nume", name);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        productId = Convert.ToInt32(result);
                    }
                }
            }

            return productId;
        }

        private decimal GetProductPrice(int productId)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                var query = "SELECT TOP 1 PretVanzare FROM Stocuri WHERE ProdusID = @productId ORDER BY DataAprovizionare DESC";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", productId);
                    return (decimal)command.ExecuteScalar();
                }
            }
        }

        public ProductBon GetProductByBarcodeOrName(string searchText)
        {
            using (SqlConnection connection = GetConnection())
            {

                connection.Open();
                var query = @"
            SELECT p.ProdusID, p.Nume, s.PretVanzare, s.Cantitate
            FROM Produse p
            INNER JOIN Stocuri s ON p.ProdusID = s.ProdusID
            WHERE p.CodBare = @searchText OR p.Nume = @searchText";
                using (var command = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        command.Parameters.AddWithValue("@searchText", searchText);
                    }
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProductBon
                            {
                                ID = reader.GetString(reader.GetOrdinal("ProdusID")),
                                Name = reader.GetString(reader.GetOrdinal("Nume")),
                                Price = reader.GetDecimal(reader.GetOrdinal("PretVanzare")),
                                Quantity = reader.GetInt32(reader.GetOrdinal("Cantitate"))
                                // Adăugați alte câmpuri necesare aici
                            };
                        }
                    }
                }
            }
            return null;
        }


        public void SaveReceipt(int cashierId, List<ProductBon> products)
        {
            decimal totalAmount = 0m;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var insertReceiptQuery = "INSERT INTO BonuriCasa (DataEliberare, CasierID, SumaIncasata) OUTPUT INSERTED.BonID VALUES (@dataEliberare, @casierID, @sumaIncasata)";
                        int receiptId;
                        using (var command = new SqlCommand(insertReceiptQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@dataEliberare", DateTime.Now);
                            command.Parameters.AddWithValue("@casierID", cashierId);
                            command.Parameters.AddWithValue("@sumaIncasata", totalAmount);
                            receiptId = (int)command.ExecuteScalar();
                        }

                        foreach (var product in products)
                        {
                            var insertProductReceiptQuery = "INSERT INTO ProduseBon (BonID, ProdusID, Cantitate, Subtotal) VALUES (@bonID, @produsID, @cantitate, @subtotal)";
                            using (var command = new SqlCommand(insertProductReceiptQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@bonID", receiptId);
                                command.Parameters.AddWithValue("@produsID", product.ID);
                                command.Parameters.AddWithValue("@cantitate", product.Quantity);

                                // Calculăm subtotalul pentru fiecare produs
                                decimal subtotal = product.Quantity * product.Price;
                                command.Parameters.AddWithValue("@subtotal", subtotal);

                                totalAmount += subtotal; // Adăugăm subtotalul la suma totală

                                command.ExecuteNonQuery();
                            }

                            var updateStockQuery = "UPDATE Stocuri SET Cantitate = Cantitate - @cantitate WHERE ProdusID = @produsID";
                            using (var command = new SqlCommand(updateStockQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@cantitate", product.Quantity);
                                command.Parameters.AddWithValue("@produsID", product.ID);
                                command.ExecuteNonQuery();
                            }
                        }

                        // Actualizăm suma totală în bon
                        var updateReceiptQuery = "UPDATE BonuriCasa SET SumaIncasata = @sumaIncasata WHERE BonID = @bonID";
                        using (var command = new SqlCommand(updateReceiptQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@sumaIncasata", totalAmount);
                            command.Parameters.AddWithValue("@bonID", receiptId);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    }



    public class Product
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }

    }

    public class ProductBon
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }



    }
}

