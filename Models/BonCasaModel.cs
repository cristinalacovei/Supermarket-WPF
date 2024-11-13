using System;
using System.Collections.Generic;

namespace WPF_LoginForm.Models
{
    public class BonCasaModel
    {
        public int Id { get; set; }
        public DateTime DataEliberare { get; set; }
        public string NumeCasier { get; set; }
        public List<ProdusBonModel> Produse { get; set; }
        public decimal SumaIncasata { get; set; }

        public BonCasaModel()
        {
            Produse = new List<ProdusBonModel>();
        }
    }

    public class ProdusBonModel
    {
        public string NumeProdus { get; set; }
        public decimal Cantitate { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class DailyTotalModel
    {
        public int Day { get; set; }
        public decimal Total { get; set; }
    }

}
