using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FitnessTrackingAndPlanning;
using Rest.WPF.FitnessTrackingAndPlanning.Views;
using RestFit.Client;
using RestFit.Client.Abstract.Model;

namespace Rest.WPF.FitnessTrackingAndPlanning.ViewModels
{
    public sealed class FriendsViewModel : ViewModelBase
    {
        #region Commands

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

        private ICommand _friendRequestsCommand;

        public ICommand FriendRequestsCommand
        {
            get => _friendRequestsCommand;
            set
            {
                _friendRequestsCommand = value;
                OnPropertyChanged(nameof(FriendRequestsCommand));
            }
        }

        #endregion



        #region Properties
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

        private List<FriendDto> _friends;

        public List<FriendDto> Friends
        {
            get => _friends;
            set
            {
                _friends = value;
                OnPropertyChanged(nameof(Friends));
            }
        }

        private string _addUserString;

        public string AddUserString
        {
            get => _addUserString;
            set
            {
                _addUserString = value;
                OnPropertyChanged(nameof(AddUserString));
            }
        }
        #endregion

        public FriendsViewModel()
        {
            GetFriendsFromDatabase().ConfigureAwait(true);

            _addFriendCommand = new RelayCommand(async _ => await AddFriendWithUsername());
            _friendRequestsCommand = new RelayCommand(async _ => await AcceptFriendWithUserId());
        }

        private async Task GetFriendsFromDatabase()
        {
            try
            {
                var a = await Kernel.ClientHub?.V1.FriendClient.GetFriendsAsync()!;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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

        private async Task AcceptFriendWithUserId()
        {
            try
            {
                await Kernel.ClientHub?.V1.FriendClient.AcceptFriendRequestAsync(_addUserString)!;
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler bei der Freundschaftsannahme von " + _addUserString + ":" + e.Message);
            }
        }
    }
}