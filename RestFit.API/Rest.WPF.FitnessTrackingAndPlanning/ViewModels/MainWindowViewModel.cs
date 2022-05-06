using System.Windows.Input;

namespace FitnessTrackingAndPlanning.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentPage;

        public ViewModelBase CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        private ICommand _changeToNavigationCommand;

        public ICommand ChangeToNavigationCommand
        {
            get => _changeToNavigationCommand;
            set
            {
                _changeToNavigationCommand = value;
                OnPropertyChanged(nameof(ChangeToNavigationCommand));
            }
        }

        public MainWindowViewModel()
        {
            CurrentPage = new LoginViewModel();
            _changeToNavigationCommand = new RelayCommand(p => ChangeToNavigationViewModel());
        }

        public void ChangeToNavigationViewModel()
        {
            if (CurrentPage is LoginViewModel loginViewModel)
            {
                loginViewModel.LoginWasSuccessful = true;
            }

            CurrentPage = new NavigationViewModel();
        }
    }
}