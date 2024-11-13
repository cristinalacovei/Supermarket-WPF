using System.Collections.Generic;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using Microsoft.VisualBasic;
using System.Linq;
using System.Windows;

namespace WPF_LoginForm.ViewModels
{
    public class CashierViewModel : ViewModelBase
    {
        private readonly ProductRepository productRepository;
        private List<ProductBon> productsForReceipt = new List<ProductBon>(); 
        private decimal totalAmount;

        private bool isProductInputVisible;
        public bool IsProductInputVisible
        {
            get { return isProductInputVisible; }
            set { isProductInputVisible = value; OnPropertyChanged(nameof(IsProductInputVisible)); }
        }

        private string productSearchText;
        public string ProductSearchText
        {
            get { return productSearchText; }
            set { productSearchText = value; OnPropertyChanged(nameof(ProductSearchText)); }
        }

        private string receiptText;
        public string ReceiptText
        {
            get { return receiptText; }
            set { receiptText = value; OnPropertyChanged(nameof(ReceiptText)); }
        }

        public ICommand ScanProductsCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand FinalizeReceiptCommand { get; }

        public CashierViewModel()
        {
            productRepository = new ProductRepository();
            totalAmount = 0m;
            ScanProductsCommand = new RelayCommand(ShowProductInput);
            AddProductCommand = new RelayCommand(AddProduct);
            FinalizeReceiptCommand = new RelayCommand(FinalizeReceipt);
            IsProductInputVisible = false;
            ReceiptText = string.Empty;
        }

        private void ShowProductInput()
        {
            IsProductInputVisible = true;
        }

        private void AddProduct()
        {
            var product = productRepository.GetProductByBarcodeOrName(ProductSearchText);
            if (product != null)
            {
                string input = Interaction.InputBox("Enter the quantity:", "Add New Product", "1");
                if (int.TryParse(input, out int quantity) && quantity > 0)
                {
                    decimal price = product.Price * quantity;
                    ReceiptText += $"{quantity} x {product.Name} .......... {price}lei\n";
                    productsForReceipt.Add(new ProductBon
                    {
                        ID = product.ID,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = quantity
                    });
                    totalAmount += price;
                    ReceiptText += $"Total ..............................{totalAmount}lei\n";
                }
                else
                {
                    MessageBox.Show("Introduceți o cantitate validă.");
                }
            }
            else
            {
                MessageBox.Show("Produsul nu a fost găsit.");
            }
        }


        private void FinalizeReceipt()
        {
            try
            {
                int cashierId = 2; // Presupunem că este un cashier ID fix. Ar trebui să fie obținut din sesiunea de autentificare.
                productRepository.SaveReceipt(cashierId,productsForReceipt);
                MessageBox.Show("Bonul a fost salvat cu succes.");
                ResetReceipt();
            }
            catch
            {
                MessageBox.Show("A apărut o eroare la salvarea bonului.");
            }
        }

        private void ResetReceipt()
        {
            productsForReceipt.Clear();
            totalAmount = 0m;
            ReceiptText = string.Empty;
        }
    }
}
