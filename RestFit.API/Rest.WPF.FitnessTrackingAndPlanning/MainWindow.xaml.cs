using System.Windows;
using FitnessTrackingAndPlanning.ViewModels;

namespace FitnessTrackingAndPlanning
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;

            InitializeComponent();

            Show();
        }
    }
}
