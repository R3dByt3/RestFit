using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using FitnessTrackingAndPlanning;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;

namespace Rest.WPF.FitnessTrackingAndPlanning.ViewModels;

public sealed class FriendsViewModel : ViewModelBase
{
    #region Commands

    private ICommand _selectedFriendChangedCommand;

    public ICommand SelectedFriendChangedCommand
    {
        get => _selectedFriendChangedCommand;
        set
        {
            _selectedFriendChangedCommand = value;
            OnPropertyChanged(nameof(SelectedFriendChangedCommand));
        }
    }

    private ICommand _selectedExerciseChangedCommand;

    public ICommand SelectedExerciseChangedCommand
    {
        get => _selectedExerciseChangedCommand;
        set
        {
            _selectedExerciseChangedCommand = value;
            OnPropertyChanged(nameof(SelectedExerciseChangedCommand));
        }
    }

    private ICommand _addFriendCommand;

    public ICommand AddFriendCommand
    {
        get => _addFriendCommand;
        set
        {
            _addFriendCommand = value;
            OnPropertyChanged(nameof(AddFriendCommand));
        }
    }

    private ICommand _acceptAcceptFriendRequestCommand;

    public ICommand AcceptFriendRequestCommand
    {
        get => _acceptAcceptFriendRequestCommand;
        set
        {
            _acceptAcceptFriendRequestCommand = value;
            OnPropertyChanged(nameof(AcceptFriendRequestCommand));
        }
    }

    private ICommand _declineFriendRequestCommand = null!;

    public ICommand DeclineFriendRequestCommand
    {
        get => _declineFriendRequestCommand;
        set
        {
            _declineFriendRequestCommand = value;
            OnPropertyChanged(nameof(DeclineFriendRequestCommand));
        }
    }

    #endregion

    #region Properties

    private List<FriendDto> _friends = null!;

    public List<FriendDto> Friends
    {
        get => _friends;
        set
        {
            _friends = value;
            OnPropertyChanged(nameof(Friends));
        }
    }

    private FriendDto _selectedFriend = null!;

    public FriendDto SelectedFriend
    {
        get => _selectedFriend;
        set
        {
            _selectedFriend = value;
            OnPropertyChanged(nameof(SelectedFriend));
        }
    }

    private List<UnitAggregationDto> _exercisesList = null!;

    public List<UnitAggregationDto> ExercisesList
    {
        get => _exercisesList;
        set
        {
            _exercisesList = value;
            OnPropertyChanged(nameof(ExercisesList));
        }
    }

    private UnitAggregationDto? _selectedExercise;

    public UnitAggregationDto? SelectedExercise
    {
        get => _selectedExercise;
        set
        {
            _selectedExercise = value;
            OnPropertyChanged(nameof(SelectedExercise));
        }
    }

    private double _averageSets;

    public double AverageSets
    {
        get => _averageSets;
        set
        {
            _averageSets = value;
            OnPropertyChanged(nameof(AverageSets));
        }
    }

    private double _averageReps;

    public double AverageReps
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

    private string _addUserString = null!;

    public string AddUserString
    {
        get => _addUserString;
        set
        {
            _addUserString = value;
            OnPropertyChanged(nameof(AddUserString));
        }
    }

    private string _friendRequestString = null!;

    public string FriendRequestString
    {
        get => _friendRequestString;
        set
        {
            _friendRequestString = value;
            OnPropertyChanged(nameof(FriendRequestString));
        }
    }

    private UserDto? _currentPendingFriendRequest;

    public UserDto? CurrentPendingFriendRequest
    {
        get => _currentPendingFriendRequest;
        set
        {
            _currentPendingFriendRequest = value;
            OnPropertyChanged(nameof(CurrentPendingFriendRequest));
        }
    }

    private bool _hasIncomingFriendRequest;

    public bool HasInComingFriendRequest
    {
        get => _hasIncomingFriendRequest;
        set
        {
            _hasIncomingFriendRequest = value;
            OnPropertyChanged(nameof(HasInComingFriendRequest));
        }
    }

    #endregion

    public FriendsViewModel()
    {
        Friends = new List<FriendDto>();
        ExercisesList = new List<UnitAggregationDto>();

        GetFriendsFromDatabase().ConfigureAwait(false);
        CheckForNewFriendRequests();

        _selectedFriendChangedCommand = new RelayCommand(_ => UpdateExercisesComboBox());
        _selectedExerciseChangedCommand = new RelayCommand(_ => UpdateExerciseAverage());
        _addFriendCommand = new RelayCommand(async _ => await AddFriendWithUsername());
        _acceptAcceptFriendRequestCommand = new RelayCommand(async _ => await AcceptFriendRequest());
        _declineFriendRequestCommand = new RelayCommand(async _ => await DeclineFriendRequest());

        var timer = new Timer();
        timer.Interval = 30000;
        timer.Elapsed += CheckFrequentlyForNewFriendRequests;
        timer.Start();
    }

    private async Task GetFriendsFromDatabase()
    {
        try
        {
            UserDto myUser = await Kernel.ClientHub!.V1.UserClient.GetMyUserAsync().ConfigureAwait(false);
            List<FriendDto> friendsFromDatabase = await Kernel.ClientHub?.V1.FriendClient.GetFriendsAsync(
                new FriendSearchDto
                {
                    Ids = myUser.FriendUserIds
                })!;

            if (friendsFromDatabase.Count != 0)
            {
                Friends = new List<FriendDto>();
                Friends.AddRange(friendsFromDatabase);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void UpdateExercisesComboBox()
    {
        ExercisesList = new List<UnitAggregationDto>();
        SelectedExercise = null;
        ExercisesList = SelectedFriend.UnitAggregationDtos.ToList();
    }

    private void UpdateExerciseAverage()
    {
        if (SelectedExercise == null)
        {
            return;
        }

        AverageSets = SelectedExercise.AverageSets;
        AverageReps = SelectedExercise.AverageRepitions;
        AverageWeight = SelectedExercise.AverageWeight;
    }

    private async Task AddFriendWithUsername()
    {
        try
        {
            await Kernel.ClientHub?.V1.FriendClient.CreateFriendRequestAsync(_addUserString)!;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler bei der Freundschaftsanfrage von " + _addUserString + ":" + e.Message);
        }
    }

    private async Task AcceptFriendRequest()
    {
        try
        {
            await Kernel.ClientHub?.V1.FriendClient.AcceptFriendRequestAsync(_currentPendingFriendRequest?.Id!)!;
            await GetFriendsFromDatabase().ConfigureAwait(true);
            UpdateExerciseAverage();
            CheckForNewFriendRequests();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler bei der Freundschaftsannahme von " + _currentPendingFriendRequest?.Username +
                              ":" + e.Message);
        }
    }

    private async Task DeclineFriendRequest()
    {
        try
        {
            await Kernel.ClientHub?.V1.FriendClient.DeclineFriendRequestAsync(_currentPendingFriendRequest?.Id!)!;
            CheckForNewFriendRequests();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Ablehnen der Freundschaftanfrage von " +
                              _currentPendingFriendRequest?.Username + ":" + e.Message);
        }
    }

    private void CheckFrequentlyForNewFriendRequests(object? sender, ElapsedEventArgs e)
    {
        CheckForNewFriendRequests();
        GetFriendsFromDatabase().ConfigureAwait(true);
    }

    private async void CheckForNewFriendRequests()
    {
        try
        {
            UserDto myUser = await Kernel.ClientHub!.V1.UserClient.GetMyUserAsync().ConfigureAwait(true);

            List<UserDto> users = await Kernel.ClientHub!.V1.UserClient.GetUsersAsync().ConfigureAwait(false);

            CurrentPendingFriendRequest =
                users.FirstOrDefault(elem => elem?.Id == myUser.PendingInFriendRequestUserIds.FirstOrDefault());

            if (CurrentPendingFriendRequest != null)
            {
                FriendRequestString = "Freundschaftsanfrage von " + CurrentPendingFriendRequest.Username;
                HasInComingFriendRequest = true;
            }
            else
            {
                FriendRequestString = "Keine Freundschaftsanfragen vorhanden";
                HasInComingFriendRequest = false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Abrufen der Freundschaftsanfragen" + ": " + e.Message);
        }
    }
}