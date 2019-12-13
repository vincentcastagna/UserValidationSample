using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserValidation.Models;

namespace UserValidation.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        private User _user;
        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public DelegateCommand RegisterCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            User = new User();

            RegisterCommand = new DelegateCommand(Register, () => !User.HasErrors)
                .ObservesProperty(() => User.HasErrors);
        }

        private void Register()
        {

        }
    }
}
