using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FitnessTrackingAndPlanning.ViewModels
{
    public class TrainingViewModel : ViewModelBase
    {

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

        private ObservableCollection<Exercise> _exercises;

        public ObservableCollection<Exercise> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                OnPropertyChanged(nameof(Exercises));
            }
        }

        private ExercisesNames _selectedExercise;
        public ExercisesNames SelectedExercise
        {
            get { return _selectedExercise; }
            set
            {
                _selectedExercise = value;
                OnPropertyChanged(nameof(_selectedExercise));
            }
        }

        public TrainingViewModel()
        {
            Exercises = new ObservableCollection<Exercise>();
            Exercises.Add(new Exercise("Kniebeuge", 3, 10, 10, ""));
            Exercises.Add(new Exercise("Pushup", 4, 10, 0, ""));

            _saveTrainingsDataCommand = new RelayCommand(p => SaveTrainingsData());
            _addExerciseCommand = new RelayCommand(p => AddExercise());
        }

        private void AddExercise()
        {
            Exercises.Add(new Exercise("", 0, 0, 0, ""));
        }

        private void SaveTrainingsData()
        {
            //TODO: Implementieren
        }
    }

    public class Exercise
    {
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
        public string Comment { get; set; }

        public Exercise(string name, int sets, int reps, float weight, string comment)
        {
            Name = name;
            Sets = sets;
            Reps = reps;
            Weight = weight;
            Comment = comment;
        }
    }

    public enum ExercisesNames
    {
        Kniebeuge,
        Pushup
    }
}