using System;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        public ICommand NavigateCategoriesCommand { get; }
        public ICommand NavigateUsersCommand { get; }
        public ICommand NavigateProducersCommand { get; }
        public ICommand NavigateProductsCommand { get; }

        public AdminViewModel()
        {
         
            NavigateCategoriesCommand = new RelayCommandNoParam(NavigateCategories);
            NavigateUsersCommand = new RelayCommandNoParam(NavigateUsers);
            NavigateProducersCommand = new RelayCommandNoParam(NavigateProducers);
            NavigateProductsCommand = new RelayCommandNoParam(NavigateProducts);
        }

        private void NavigateCategories()
        {
            // Open CategoriesView as a Window
            var categoriesWindow = new CategoryView();
            categoriesWindow.Show();
         
        }

        private void NavigateUsers()
        {
            // Open UsersView as a Window
            var usersWindow = new UsersView();
            usersWindow.Show();

        }

        private void NavigateProducers()
        {
            // Open ProducersView as a Window
            var producersWindow = new ProducersView();
            producersWindow.Show();
        }

        private void NavigateProducts()
        {
            // Open ProductsView as a Window
            var productsWindow = new ProductsView();
            productsWindow.Show();
        }
    }
}
