using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        private readonly IUserRepository _userRepository;

        public ObservableCollection<UserModel> Users { get; set; }
        public ICommand DeleteUserCommand { get; }

        public UsersViewModel()
        {
            _userRepository = new UserRepository(); 
            Users = new ObservableCollection<UserModel>(_userRepository.GetByAll());
            DeleteUserCommand = new RelayCommand<UserModel>(DeleteUser);
        }

        private void DeleteUser(UserModel user)
        {
            if (user != null)
            {
                user.TypeUser = "Inactive"; // Assuming there's a status field to mark as inactive
                _userRepository.Edit(user);
                Users = new ObservableCollection<UserModel>(_userRepository.GetByAll());


            }
        }
    }
}
