namespace Rest.WPF.FitnessTrackingAndPlanning.ViewModels
{
    public sealed class FriendsViewModel : ViewModelBase
    {
        private int _averageSets;

        public int AverageSets
        {
            get => _averageSets;
            set
            {
                _averageSets = value;
                OnPropertyChanged(nameof(AverageSets));
            }
        }

        private int _averageReps;

        public int AverageReps
        {
            get => _averageReps;
            set
            {
                _averageReps = value;
                OnPropertyChanged(nameof(AverageReps));
            }
        }

        private double _averageWeight;

        public double AverageWeight
        {
            get => _averageWeight;
            set
            {
                _averageWeight = value;
                OnPropertyChanged(nameof(AverageWeight));
            }
        }

        private string _motivationString;

        public string MotivationString
        {
            get => _motivationString;
            set
            {
                _motivationString = value;
                OnPropertyChanged(nameof(MotivationString));
            }
        }
    }
}