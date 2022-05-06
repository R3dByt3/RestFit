using Rest.WPF.FitnessTrackingAndPlanning;
using RestFit.Client;
using RestFit.Client.Abstract.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessTrackingAndPlanning.ViewModels
{
    public sealed class LoginViewModel : ViewModelBase
    {

        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get => _loginCommand;
            set
            {
                _loginCommand = value;
                OnPropertyChanged(nameof(LoginCommand));
            }
        }

        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _password = string.Empty;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool _loginWasSuccessful;

        public bool LoginWasSuccessful
        {
            get => _loginWasSuccessful;
            set
            {
                _loginWasSuccessful = value;
                OnPropertyChanged(nameof(LoginWasSuccessful));
            }
        }

        public LoginViewModel()
        {
            _loginCommand = new RelayCommand(_ => Login());
        }

        private void Login()
        {
            Kernel.ClientHub = new ClientHub(UserName, Password);
        }
    }
}