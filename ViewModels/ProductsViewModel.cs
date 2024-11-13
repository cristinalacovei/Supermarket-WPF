using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Models;
using System.Windows;
using System;
using System.ComponentModel;

namespace WPF_LoginForm.ViewModels
{
    public class ProductsViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ProductRepository productRepository;
        private readonly ProducerRepository producerRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly StocksRepository stocksRepository;

        public ProductsViewModel()
        {
            productRepository = new ProductRepository();
            producerRepository = new ProducerRepository();
            categoryRepository = new CategoryRepository();
            stocksRepository = new StocksRepository();
            SearchCommand = new RelayCommand<object>(Search);
            AddProductCommand = new RelayCommand<object>(OpenAddProductDialog);
            ProductsForSelectedProducer = new ObservableCollection<Product>();

            LoadProducers();
            LoadCategories();
        }

        private ObservableCollection<Product> productsForSelectedProducer;
        public ObservableCollection<Product> ProductsForSelectedProducer
        {
            get { return productsForSelectedProducer; }
            set { productsForSelectedProducer = value; OnPropertyChanged(nameof(ProductsForSelectedProducer)); }
        }

        private string searchKeyword;
        public string SearchKeyword
        {
            get { return searchKeyword; }
            set { searchKeyword = value; OnPropertyChanged(nameof(SearchKeyword)); }
        }

        public ICommand SearchCommand { get; private set; }
        public ICommand AddProductCommand { get; private set; }

        private void Search(object parameter)
        {
            // Convertiți numele producătorului în ID-ul corespunzător
            string producerId = producerRepository.GetProducerIdByName(SearchKeyword);

            if (!string.IsNullOrEmpty(producerId))
            {
                // Căutați produsele asociate producătorului și actualizați lista de produse
                ProductsForSelectedProducer = new ObservableCollection<Product>(productRepository.GetProductsByProducer(producerId));
            }
            else
            {
                // Dacă nu s-a găsit niciun ID pentru producătorul introdus, nu există produse de afișat
                ProductsForSelectedProducer.Clear();
            }
        }

        public ObservableCollection<Producer> Producers { get; set; }
        public ObservableCollection<CategoryModel> Categories { get; set; }

        // Add the rest of your properties and commands here

        private void LoadProducers()
        {
            Producers = new ObservableCollection<Producer>(producerRepository.GetAllProducers());
        }

        private void LoadCategories()
        {
            Categories = new ObservableCollection<CategoryModel>(categoryRepository.GetAllCategories());
        }

        private Producer selectedProducer;
        public Producer SelectedProducer
        {
            get { return selectedProducer; }
            set { selectedProducer = value; OnPropertyChanged(nameof(SelectedProducer)); }
        }

        private CategoryModel selectedCategory;
        public CategoryModel SelectedCategory
        {
            get { return selectedCategory; }
            set { selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); }
        }


        private void OpenAddProductDialog(object parameter)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Enter product name:", "Add New Product", "");
            string bareCode = Microsoft.VisualBasic.Interaction.InputBox("Enter the barcode of the product", "Add New Product", "");
            decimal quantity = Convert.ToDecimal(Microsoft.VisualBasic.Interaction.InputBox("Enter the quantity:", "Add New Product", ""));
            string unitOfMeasure = Microsoft.VisualBasic.Interaction.InputBox("Enter the unit of measure:", "Add New Product", "");
            DateTime supplyDate = DateTime.Now; // Puteți modifica această valoare pentru a reflecta data reală de aprovizionare
            DateTime? expiryDate = null; // Data expirării poate fi nulă inițial
            string expiryInput = Microsoft.VisualBasic.Interaction.InputBox("Enter the expiry date (leave blank if none):", "Add New Product", "");
            if (!string.IsNullOrEmpty(expiryInput))
            {
                if (DateTime.TryParse(expiryInput, out DateTime parsedExpiryDate))
                {
                    expiryDate = parsedExpiryDate;
                }
                else
                {
                    MessageBox.Show("Invalid expiry date format! Please enter the date in the correct format (e.g., yyyy-mm-dd).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            decimal purchasePrice = Convert.ToDecimal(Microsoft.VisualBasic.Interaction.InputBox("Enter the purchase price:", "Add New Product", ""));

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(bareCode) && SelectedProducer != null && SelectedCategory != null)
            {
                ProductsModel newProduct = new ProductsModel
                {
                    Name = name,
                    CodBare = bareCode,
                    CategoryID = SelectedCategory.ID,
                    ProducersID = SelectedProducer.ID
                };

                // Adăugați produsul în baza de date
                productRepository.AddProduct(newProduct);

                // Obțineți ID-ul produsului nou adăugat
                int productId = productRepository.GetProductIdByName(name);

                // Creează obiectul StocuriModel pentru a fi adăugat în baza de date
                StockModel newStock = new StockModel
                {
                    ProductID = productId.ToString(),
                    Quantity = quantity,
                    UnitOfMeasure = unitOfMeasure,
                    SupplyDate = supplyDate,
                    ExpiryDate = expiryDate,
                    PurchasePrice = purchasePrice,
                    SellingPrice = purchasePrice * 1.20M,
                };

                // Adaugă stocul în baza de date
                stocksRepository.AddStock(newStock);

                // Afisează un mesaj de succes
                MessageBox.Show("Product and stock added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(SearchKeyword):
                        if (string.IsNullOrEmpty(SearchKeyword))
                            return "Search keyword cannot be empty.";
                        break;
                        // Adăugați alte cazuri pentru SelectedProducer și SelectedCategory
                }
                return null;
            }
        }

        // Actualizarea AddProductCommand pentru a verifica dacă sunt selectate atât o categorie cât și un producător
        private bool CanAddProduct(object parameter)
        {
            return SelectedProducer != null && SelectedCategory != null;
        }
    
}

}

