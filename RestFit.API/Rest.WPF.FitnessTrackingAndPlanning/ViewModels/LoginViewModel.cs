using Rest.WPF.FitnessTrackingAndPlanning;
using RestFit.Client;
using System;
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
            _loginCommand = new RelayCommand(async _ => await Login());
        }

        private async Task Login()
        {
            Kernel.ClientHub = new ClientHub(UserName, Password);
            
            var myUser = await Kernel.ClientHub.V1.UserClient.GetMyUser().ConfigureAwait(false);
            Console.WriteLine();

            //ToDo: GetMyUser knallt wenn Login nicht valide;
            //ToDo: Password falsch anzeigen
            //ToDo: Immer über Kernel.ClientHub.V1.<DataType>Client.Get / Create arbeiten (noch nicht alles da)
            //ToDo: Marvin: Datenmodelle gleichziehen, alle Routen / Clients implementieren
            //ToDo: Bei async immer hinter den Aufruf ConfigureAwait(false) bei Logik und true bei GUI (glaube ich)
        }
    }
}