using System.ComponentModel;

namespace Rest.WPF.FitnessTrackingAndPlanning.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{
    #region INotifyPropertyChanged-Members

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}