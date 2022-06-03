using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FitnessTrackingAndPlanning;
using RestFit.Client;

namespace Rest.WPF.FitnessTrackingAndPlanning.ViewModels;

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

    private Visibility _loginFailedMessageVisibility;

    public Visibility LoginFailedMessageVisibility
    {
        get => _loginFailedMessageVisibility;
        set
        {
            _loginFailedMessageVisibility = value;
            OnPropertyChanged(nameof(LoginFailedMessageVisibility));
        }
    }

    #endregion

    public event Notify? ChangeView;

    public LoginViewModel()
    {
        _loginFailedMessageVisibility = Visibility.Hidden;
        _loginCommand = new RelayCommand(async _ => await Login());
    }

    private async Task Login()
    {
        if (UserName != string.Empty && Password != string.Empty)
        {
            Kernel.ClientHub = new ClientHub(UserName, Password);
            try
            {
                await Kernel.ClientHub.V1.UserClient.GetMyUserAsync().ConfigureAwait(false);
                ChangeView?.Invoke();
            }
            catch
            {
                LoginFailedMessageVisibility = Visibility.Visible;
            }
        }
        else
        {
            LoginFailedMessageVisibility = Visibility.Visible;
        }
    }
}