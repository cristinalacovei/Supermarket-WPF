using System.Collections.Generic;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class StocksViewModel : ViewModelBase
    {
        private readonly StocksRepository stocksRepository;

        public List<Stock> Stocks { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public StocksViewModel()
        {
            stocksRepository = new StocksRepository();
            EditCommand = new RelayCommand<Stock>(EditStock);
            DeleteCommand = new RelayCommand<Stock>(DeleteStock);

            LoadStocks();
        }

        private Stock selectedStock;
        public Stock SelectedStock
        {
            get { return selectedStock; }
            set { selectedStock = value; OnPropertyChanged(nameof(SelectedStock)); }
        }



        private void LoadStocks()
        {
            Stocks = stocksRepository.GetAllStocks();
        }

        private void EditStock(Stock selectedStock)
        {
            if (selectedStock != null)
            {
                stocksRepository.UpdateStock(selectedStock);
                LoadStocks();
            }
        }

        public void DeleteStock(Stock stock)
        {
            if (stock != null)
            {
                stock.IsActive = false; // Setăm isActive la false pentru a marca stocul ca fiind inactiv
                stocksRepository.UpdateStock(stock); // Apelăm metoda UpdateStock pentru a salva modificările
                LoadStocks(); // Reîncărcăm lista de stocuri pentru a reflecta modificările
            }
        }

    }
}
