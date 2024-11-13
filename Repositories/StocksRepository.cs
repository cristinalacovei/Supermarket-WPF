using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class StocksRepository : RepositoryBase
    {
        public void AddStock(StockModel stock)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = @"INSERT INTO Stocuri (ProdusID, Cantitate, UnitateMasura, DataAprovizionare, DataExpirare, PretAchizitie, PretVanzare,isActive) 
                         VALUES (@ProdusID, @Cantitate, @UnitateMasura, @DataAprovizionare, @DataExpirare, @PretAchizitie, @PretVanzare, @isActive)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProdusID", stock.ProductID);
                    command.Parameters.AddWithValue("@Cantitate", stock.Quantity);
                    command.Parameters.AddWithValue("@UnitateMasura", stock.UnitOfMeasure);
                    command.Parameters.AddWithValue("@DataAprovizionare", stock.SupplyDate);
                    command.Parameters.AddWithValue("@DataExpirare", (object)stock.ExpiryDate ?? DBNull.Value); // Handle nullable ExpiryDate
                    command.Parameters.AddWithValue("@PretAchizitie", stock.PurchasePrice);
                    command.Parameters.AddWithValue("@PretVanzare", stock.SellingPrice);
                    command.Parameters.AddWithValue("@isActive", true);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public List<Stock> GetAllStocks()
        {
            List<Stock> stocks = new List<Stock>();

            string query = @"
                SELECT 
                    s.StocID AS ID,
                    p.Nume AS ProductName,
                    s.Cantitate AS Quantity,
                    s.UnitateMasura AS UnitOfMeasure,
                    s.DataAprovizionare AS SupplyDate,
                    s.DataExpirare AS ExpiryDate,
                    s.PretAchizitie AS PurchasePrice,
                    s.PretVanzare AS SellingPrice
                FROM 
                    Stocuri s
               
                JOIN 
                    Produse p ON s.ProdusID = p.ProdusID
                WHERE
                    s.isActive = 1";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Stock stock = new Stock
                    {
                        ID = reader["ID"].ToString(),
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToDecimal(reader["Quantity"]),
                        UnitOfMeasure = reader["UnitOfMeasure"].ToString(),
                        SupplyDate = Convert.ToDateTime(reader["SupplyDate"]),
                        ExpiryDate = reader.IsDBNull(reader.GetOrdinal("ExpiryDate")) ? (DateTime?)null : Convert.ToDateTime(reader["ExpiryDate"]),
                        PurchasePrice = Convert.ToDecimal(reader["PurchasePrice"]),
                        SellingPrice = Convert.ToDecimal(reader["SellingPrice"])
                    };
                    stocks.Add(stock);
                }
            }

            return stocks;
        }

        public void UpdateStock(Stock stock)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = @"
            UPDATE Stocuri 
            SET 
                Cantitate = @Cantitate,
                UnitateMasura = @UnitateMasura,
                DataAprovizionare = @DataAprovizionare,
                DataExpirare = @DataExpirare,
                PretAchizitie = @PretAchizitie,
                PretVanzare = @PretVanzare,
                isActive=@isActive
            WHERE StocID = @StocID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cantitate", stock.Quantity);
                    command.Parameters.AddWithValue("@UnitateMasura", stock.UnitOfMeasure);
                    command.Parameters.AddWithValue("@DataAprovizionare", stock.SupplyDate);
                    command.Parameters.AddWithValue("@DataExpirare", (object)stock.ExpiryDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PretAchizitie", stock.PurchasePrice);
                    command.Parameters.AddWithValue("@PretVanzare", stock.PurchasePrice * 1.2M);
                    command.Parameters.AddWithValue("@StocID", stock.ID);
                    command.Parameters.AddWithValue("@isActive", true);

                    command.ExecuteNonQuery();
                }
            }
        }


    }

    public class Stock
    {
        public string ID { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime SupplyDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool IsActive { get; set; }

    }
}
