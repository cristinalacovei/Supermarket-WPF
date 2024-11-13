using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private readonly CategoryRepository categoryRepository;

        public CategoryViewModel()
        {
            categoryRepository = new CategoryRepository();
            LoadCategories();
            AddCommand = new RelayCommand<string>(AddCategory);
            EditCommand = new RelayCommand<CategoryModel>(EditCategory);
            OpenAddCategoryDialogCommand = new RelayCommand<object>(OpenAddCategoryDialog);
        }
        private decimal totalBudget;
        public decimal TotalBudget
        {
            get { return totalBudget; }
            set { totalBudget = value; OnPropertyChanged(nameof(TotalBudget)); }
        }


        private ObservableCollection<CategoryModel> categories;
        public ObservableCollection<CategoryModel> Categories
        {
            get { return categories; }
            set { categories = value; OnPropertyChanged(nameof(Categories)); }
        }

        private CategoryModel selectedCategory;
        public CategoryModel SelectedCategory
        {
            get { return selectedCategory; }
            set { selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand OpenAddCategoryDialogCommand {  get; private set; }

        private void LoadCategories()
        {
            Categories = new ObservableCollection<CategoryModel>(categoryRepository.GetAllCategories());
            TotalBudget = categoryRepository.GetTotalBudget();
        }


        private void AddCategory(string categoryName)
        {
            if (!string.IsNullOrEmpty(categoryName))
            {
                CategoryModel newCategory = new CategoryModel { Name = categoryName };
                categoryRepository.AddCategory(newCategory);
                LoadCategories();
            }
        }


        private void EditCategory(CategoryModel category)
        {
            if (category != null)
            {
                categoryRepository.UpdateCategory(category);
                LoadCategories();
            }
        }


        private void OpenAddCategoryDialog(object parameter)
        {

            string name = Microsoft.VisualBasic.Interaction.InputBox("Enter category name:", "Add New Category", "");
          

            if (!string.IsNullOrEmpty(name))
            {
                // Creează un nou producător cu valorile introduse
                CategoryModel newCategory = new CategoryModel
                {
                    Name = name
                };

                // Adaugă noul producător utilizând metoda AddProducer din repository
                categoryRepository.AddCategory(newCategory);

                // Reîncarcă lista de producători pentru a afișa noul producător în interfață
                LoadCategories();
            }
        }


    }
}
