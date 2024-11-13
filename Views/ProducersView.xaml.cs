using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_LoginForm.ViewModels;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Interaction logic for ProducersView.xaml
    /// </summary>
    public partial class ProducersView : Window
    {
        public ProducersView()
        {
            InitializeComponent();
       
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            new AdminView().Show();
            this.Hide();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnGoToProducts_Click(object sender, RoutedEventArgs e)
        {
            new SearchProducts().Show();
            this.Close();
        }
    }
}
