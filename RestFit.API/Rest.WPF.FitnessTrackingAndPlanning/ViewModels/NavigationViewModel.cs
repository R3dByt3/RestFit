namespace FitnessTrackingAndPlanning.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        private ViewModelBase _trainingPage;

        public ViewModelBase TrainingPage
        {
            get => _trainingPage;
            set
            {
                _trainingPage = value;
                OnPropertyChanged(nameof(TrainingPage));
            }
        }

        private ViewModelBase _friendsPage;

        public ViewModelBase FriendsPage
        {
            get => _friendsPage;
            set
            {
                _friendsPage = value;
                OnPropertyChanged(nameof(FriendsPage));
            }
        }

        private ViewModelBase _healthDataPage;

        public ViewModelBase HealthDataPage
        {
            get => _healthDataPage;
            set
            {
                _healthDataPage = value;
                OnPropertyChanged(nameof(HealthDataPage));
            }
        }

        private ViewModelBase _historyPage;

        public ViewModelBase HistoryPage
        {
            get => _historyPage;
            set
            {
                _historyPage = value;
                OnPropertyChanged(nameof(HistoryPage));
            }
        }

        public NavigationViewModel()
        {
            TrainingPage = new TrainingViewModel();
            FriendsPage = new FriendsViewModel();
            HealthDataPage = new HealthDataViewModel();
            //HistoryPage = new HistoryViewModel();
        }

    }
}