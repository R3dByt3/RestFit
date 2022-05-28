using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FitnessTrackingAndPlanning;
using RestFit.Client.Abstract.Model;

namespace Rest.WPF.FitnessTrackingAndPlanning.ViewModels;

public sealed class TrainingViewModel : ViewModelBase
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
        _exercises = new ObservableCollection<UnitDto> { new() };

        _saveTrainingsDataCommand = new RelayCommand(async _ => await SaveTrainingsData());
        _addExerciseCommand = new RelayCommand(_ => AddExercise());
    }

    private void AddExercise()
    {
        _exercises.Add(new UnitDto());
    }

    private async Task SaveTrainingsData()
    {
        foreach (UnitDto exercise in _exercises)
        {
            if (exercise == new UnitDto() || exercise.Type == string.Empty || exercise.Sets == 0 || exercise.Repetitions == 0)
            {
                continue;
            }

            try
            {
                exercise.DateUtc = DateTime.Today;
                await Kernel.ClientHub?.V1.UnitClient.AddUnitAsync(exercise)!;
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler beim Speichern von " + exercise.Type + ":" + e.Message);
            }
        }
    }
}