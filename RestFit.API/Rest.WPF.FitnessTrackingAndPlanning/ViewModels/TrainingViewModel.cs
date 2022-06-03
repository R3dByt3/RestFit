using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FitnessTrackingAndPlanning;
using RestFit.Client.Abstract.KnownSearches;
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

    private readonly List<UnitDto> _exercisesFailedToSave;

    public TrainingViewModel()
    {
        _exercises = new ObservableCollection<UnitDto> { new() };
        _exercisesFailedToSave = new List<UnitDto>();

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
            if (exercise == new UnitDto() ||
                exercise.Type == string.Empty ||
                exercise.Sets <= 0 ||
                exercise.Repetitions <= 0 ||
                exercise.Weight < 0 ||
                await CheckIfExerciseAlreadyExistsForToday(exercise).ConfigureAwait(false))
            {
                _exercisesFailedToSave.Add(exercise);
                continue;
            }

            try
            {
                exercise.DateUtc = DateTime.UtcNow;
                await Kernel.ClientHub?.V1.UnitClient.AddUnitAsync(exercise)!;
            }
            catch (Exception e)
            {
                _exercisesFailedToSave.Add(exercise);
                Console.WriteLine("Fehler beim Speichern von " + exercise.Type + ":" + e.Message);
            }
        }

        if (_exercisesFailedToSave.Count != 0)
        {
            string exercisesString =
                _exercisesFailedToSave.Aggregate<UnitDto, string>(null!,
                    (current, exercise) => current + $"[{exercise.Type}]");

            MessageBox.Show("Es konnten folgende Übungen nicht gespeichert werden: " + exercisesString,
                "Speichern fehlgeschlagen", MessageBoxButton.OK);
        }
    }

    private async Task<bool> CheckIfExerciseAlreadyExistsForToday(UnitDto exercise)
    {
        try
        {
            IList<UnitDto> foundExercise = await Kernel.ClientHub?.V1.UnitClient.GetUnitsAsync(new UnitSearchDto
            {
                Type = exercise.Type,
                DateUtc = exercise.DateUtc
            })!;

            return foundExercise.Count != 0;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim kontrollieren von " + exercise.Type + ":" + e.Message);
            return true;
        }
    }
}