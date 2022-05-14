using Rest.WPF.FitnessTrackingAndPlanning;
using RestFit.Client;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessTrackingAndPlanning.ViewModels
{
    public sealed class LoginViewModel : ViewModelBase
    {
        public delegate void Notify();

        #region Commands
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
        #endregion

        #region Properties
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

        private bool _loginFailed;

        public bool LoginFailed
        {
            get => _loginFailed;
            set
            {
                _loginFailed = value;
                OnPropertyChanged(nameof(LoginFailed));
            }
        }
        #endregion

        public event Notify? ChangeView;

        public LoginViewModel()
        {
            _loginCommand = new RelayCommand(async _ => await Login());
        }

        private async Task Login()
        {
            Kernel.ClientHub = new ClientHub(UserName, Password);
            try
            {
                var myUser = await Kernel.ClientHub.V1.UserClient.GetMyUser().ConfigureAwait(false);
                ChangeView?.Invoke();
            }
            catch
            {
                LoginFailed = true;
            }
            
            //ToDo: Immer über Kernel.ClientHub.V1.<DataType>Client.Get / Create arbeiten (noch nicht alles da)
            //ToDo: Bei async immer hinter den Aufruf ConfigureAwait(false) bei Logik und true bei GUI (glaube ich)
        }
    }
}