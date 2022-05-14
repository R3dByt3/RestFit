namespace FitnessTrackingAndPlanning.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties

        private ViewModelBase? _currentPage;

        public ViewModelBase? CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));

            }
        }
        
        #endregion

        /// <summary>
        /// Konstruktor für das MainWindowViewModel
        /// </summary>
        public MainWindowViewModel()
        {
            CurrentPage = new LoginViewModel();
            if (CurrentPage is LoginViewModel loginViewModel)
                loginViewModel.ChangeView += OnChangeView;
        }

        private void OnChangeView()
        {
            CurrentPage = new NavigationViewModel();
        }
    }
}