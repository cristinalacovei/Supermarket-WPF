using System;

namespace WPF_LoginForm.Models
{
    public class StockModel
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime SupplyDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
