namespace FitnessTrackingAndPlanning.ViewModels
{
    public sealed class HistoryViewModel : ViewModelBase
    {
        public delegate void Notify();

        #region Commands

        #endregion

        #region Properties

        #endregion

        public event Notify? ChangeView;

        public HistoryViewModel()
        {

        }
    }
}