using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class BonuriCasaRepository : RepositoryBase
    {

        public List<BonCasaModel> GetBonuriByMonthAndCasier(int casierId, int month, int year)
        {
            var bonuri = new List<BonCasaModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT b.BonID, b.DataEliberare, u.NumeUtilizator, b.SumaIncasata
                    FROM BonuriCasa b
                    JOIN Utilizatori u ON b.CasierID = u.UtilizatorID
                    WHERE b.CasierID = @casierId AND MONTH(b.DataEliberare) = @month AND YEAR(b.DataEliberare) = @year";
                command.Parameters.Add("@casierId", SqlDbType.Int).Value = casierId;
                command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                command.Parameters.Add("@year", SqlDbType.Int).Value = year;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bon = new BonCasaModel
                        {
                            Id = reader.GetInt32(0),
                            DataEliberare = reader.GetDateTime(1),
                            NumeCasier = reader.GetString(2),
                            SumaIncasata = reader.GetDecimal(3),
                            Produse = GetProduseByBonId(reader.GetInt32(0))
                        };
                        bonuri.Add(bon);
                    }
                }
            }

            return bonuri;
        }

        private List<ProdusBonModel> GetProduseByBonId(int bonId)
        {
            var produse = new List<ProdusBonModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT p.NumeProdus, pb.Cantitate, pb.Subtotal
                    FROM ProduseBon pb
                    JOIN Produse p ON pb.ProdusID = p.ProdusID
                    WHERE pb.BonID = @bonId";
                command.Parameters.Add("@bonId", SqlDbType.Int).Value = bonId;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var produs = new ProdusBonModel
                        {
                            NumeProdus = reader.GetString(0),
                            Cantitate = reader.GetDecimal(1),
                            Subtotal = reader.GetDecimal(2)
                        };
                        produse.Add(produs);
                    }
                }
            }

            return produse;
        }


        public IEnumerable<DailyTotalModel> GetDailyTotalsByUserAndMonth(int casierId, int month, int year)
        {
            var dailyTotals = new List<DailyTotalModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                // Modificați aici comanda SQL
                command.CommandText = @"
            SELECT DAY(DataEliberare) AS [Day], SUM(SumaIncasata) AS [Total]
            FROM BonuriCasa
            WHERE CasierID = @casierId AND MONTH(DataEliberare) = @month AND YEAR(DataEliberare) = @year
            GROUP BY DAY(DataEliberare)
            ORDER BY [Day]";
                command.Parameters.AddWithValue("@casierId", casierId);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@year", year);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dailyTotal = new DailyTotalModel
                        {
                            Day = reader.GetInt32(0), // Aici setăm proprietatea Day
                            Total = reader.GetDecimal(1)
                        };
                        dailyTotals.Add(dailyTotal);
                    }
                }
            }

            return dailyTotals;
        }

    }
}
