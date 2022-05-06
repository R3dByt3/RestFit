using System.Windows.Input;

namespace FitnessTrackingAndPlanning.ViewModels
{
    public sealed class LoginViewModel : ViewModelBase
    {
        private bool _userName;

        public bool UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private bool _password;

        public bool Password
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
    }
}