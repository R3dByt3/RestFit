using System.Windows.Input;

namespace FitnessTrackingAndPlanning.ViewModels
{
    public sealed class HealthDataViewModel : ViewModelBase
    {
        private double _weight;

        public double Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        private double _armSize;

        public double ArmSize
        {
            get => _armSize;
            set
            {
                _armSize = value;
                OnPropertyChanged(nameof(ArmSize));
            }
        }

        private double _waistSize;

        public double WaistSize
        {
            get => _waistSize;
            set
            {
                _waistSize = value;
                OnPropertyChanged(nameof(WaistSize));
            }
        }

        private double _hipSize;

        public double HipSize
        {
            get => _hipSize;
            set
            {
                _hipSize = value;
                OnPropertyChanged(nameof(HipSize));
            }
        }

        private double _thighSize;

        public double ThighSize
        {
            get => _thighSize;
            set
            {
                _thighSize = value;
                OnPropertyChanged(nameof(ThighSize));
            }
        }

        private ICommand _saveHealthDataCommand;

        public ICommand SaveHealthDataCommand
        {
            get => _saveHealthDataCommand;
            set
            {
                _saveHealthDataCommand = value;
                OnPropertyChanged(nameof(SaveHealthDataCommand));
            }
        }

        public HealthDataViewModel()
        {
            _saveHealthDataCommand = new RelayCommand(p => SaveHealthData());
            Weight = 100;
            ArmSize = 80;
            WaistSize = 90;
            HipSize = 30;
            ThighSize = 50;
        }

        public void SaveHealthData()
        {
            //TODO: Implementieren (inkl. Checken ob neue Werte!)
        }
    }
}