using Rest.WPF.FitnessTrackingAndPlanning;
using RestFit.Client.Abstract.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessTrackingAndPlanning.ViewModels
{
    public class TrainingViewModel : ViewModelBase
    {
        #region Commands
        private ICommand _saveTrainingsDataCommand;

        public ICommand SaveTrainingsDataCommand
        {
            get => _saveTrainingsDataCommand;
            set
            {
                _saveTrainingsDataCommand = value;
                OnPropertyChanged(nameof(SaveTrainingsDataCommand));
            }
        }

        private ICommand _addExerciseCommand;

        public ICommand AddExerciseCommand
        {
            get => _addExerciseCommand;
            set
            {
                _addExerciseCommand = value;
                OnPropertyChanged(nameof(AddExerciseCommand));
            }
        }
        #endregion

        #region Properties
        private ObservableCollection<UnitDto> _exercises;

        public ObservableCollection<UnitDto> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                OnPropertyChanged(nameof(Exercises));
            }
        }
        #endregion

        public TrainingViewModel()
        {
            _exercises = new ObservableCollection<UnitDto> { new UnitDto() };

            _saveTrainingsDataCommand = new RelayCommand(async _ => await SaveTrainingsData());
            _addExerciseCommand = new RelayCommand(p => AddExercise());
        }

        private void AddExercise()
        {
            _exercises.Add(new UnitDto());
        }

        private async Task SaveTrainingsData()
        {
            foreach (var exercise in _exercises)
            {
                if(exercise != new UnitDto())
                {
                    try
                    {
                        await Kernel.ClientHub.V1.UnitClient.AddUnitAsync(exercise);
                    }
                    catch
                    {
                        //TODO: Implementieren
                        Console.WriteLine("Fehler bei " + exercise.Type);
                    }
                }
            }
        }
    }
}