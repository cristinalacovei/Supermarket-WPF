using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class ProducersViewModel : ViewModelBase
    {
        private readonly ProducerRepository producerRepository;

        public ProducersViewModel()
        {
            producerRepository = new ProducerRepository();
            LoadProducers();
            AddCommand = new RelayCommand<Producer>(AddProducer);
            EditCommand = new RelayCommand<Producer>(EditProducer);
            DeleteCommand = new RelayCommand<Producer>(DeleteProducer);
            OpenAddProducerDialogCommand = new RelayCommand<object>(OpenAddProducerDialog);
        }

        private ObservableCollection<Producer> producers;
        public ObservableCollection<Producer> Producers
        {
            get { return producers; }
            set { producers = value; OnPropertyChanged(nameof(Producers)); }
        }

        private Producer selectedProducer;
        public Producer SelectedProducer
        {
            get { return selectedProducer; }
            set { selectedProducer = value; OnPropertyChanged(nameof(SelectedProducer)); }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand OpenAddProducerDialogCommand { get; private set; }

        private void LoadProducers()
        {
            Producers = new ObservableCollection<Producer>(producerRepository.GetAllProducers());
        }

        private void AddProducer(object parameter)
        {
            // Implementați logica pentru adăugarea unui nou producător
            producerRepository.AddProducer(new Producer());
            LoadProducers();
        }

        private void EditProducer(object parameter)
        {
            // Implementați logica pentru editarea producătorului selectat
            producerRepository.UpdateProducer(SelectedProducer);
            LoadProducers();
        }

        private void DeleteProducer(object parameter)
        {
            // Implementați logica pentru ștergerea producătorului selectat
            producerRepository.DeleteProducer(SelectedProducer.ID.ToString());
            LoadProducers();
        }

        private void OpenAddProducerDialog(object parameter)
        {
          
            string name = Microsoft.VisualBasic.Interaction.InputBox("Enter producer name:", "Add New Producer", "");
            string countryOrigin = Microsoft.VisualBasic.Interaction.InputBox("Enter country of origin:", "Add New Producer", "");

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(countryOrigin))
            {
                // Creează un nou producător cu valorile introduse
                Producer newProducer = new Producer
                {
                    Name = name,
                    CountryOrigin = countryOrigin
                };

                // Adaugă noul producător utilizând metoda AddProducer din repository
                producerRepository.AddProducer(newProducer);

                // Reîncarcă lista de producători pentru a afișa noul producător în interfață
                LoadProducers();
            }
        }

    }
}
