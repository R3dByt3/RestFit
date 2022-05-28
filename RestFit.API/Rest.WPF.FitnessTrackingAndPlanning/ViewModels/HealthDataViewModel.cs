using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using FitnessTrackingAndPlanning;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;

namespace Rest.WPF.FitnessTrackingAndPlanning.ViewModels;

public sealed class HealthDataViewModel : ViewModelBase
{
    #region Commands

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

    #endregion

    #region Properties

    private HealthUnitDto _healthDataDto;

    public HealthUnitDto HealthDataDto
    {
        get => _healthDataDto;
        set
        {
            _healthDataDto = value;
            OnPropertyChanged(nameof(HealthDataDto));
        }
    }

    private bool _savingEnabled;

    public bool SavingEnabled
    {
        get => _savingEnabled;
        set
        {
            _savingEnabled = value;
            OnPropertyChanged(nameof(SavingEnabled));
        }
    }

    #endregion

    public HealthDataViewModel()
    {
        _healthDataDto = new HealthUnitDto();
        SavingEnabled = true;

        IsSavingHealthDataEnabled().ConfigureAwait(true);

        _saveHealthDataCommand = new RelayCommand(async _ => await SaveHealthData());
    }

    private async Task IsSavingHealthDataEnabled()
    {
        IList<HealthUnitDto> todaysHealthData =
            await Kernel.ClientHub?.V1.HealthUnitClient.GetHealthUnitsAsync(new HealthUnitSearchDto
                { DateUtc = DateTime.Today })!;

        if (todaysHealthData.Count != 0)
        {
            SavingEnabled = false;
        }
    }

    private async Task SaveHealthData()
    {
        HealthDataDto.DateUtc = DateTime.Today;

        try
        {
            await Kernel.ClientHub?.V1.HealthUnitClient.AddHealthUnitAsync(HealthDataDto)!;
            SavingEnabled = false;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Speichern von der Gesundheitsdaten: " + e.Message);
        }
    }
}