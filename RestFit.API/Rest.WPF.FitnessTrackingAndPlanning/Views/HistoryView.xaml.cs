using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using RestFit.Client.Abstract.Model;

namespace Rest.WPF.FitnessTrackingAndPlanning.Views;

/// <summary>
///     Interaktionslogik für HistoryView.xaml
/// </summary>
public sealed partial class HistoryView : UserControl
{
    #region Properties

    public SeriesCollection? SeriesCollectionExercisesSets { get; set; }
    public SeriesCollection? SeriesCollectionExercisesReps { get; set; }
    public SeriesCollection? SeriesCollectionExercisesWeight { get; set; }
    public List<string>? Labels { get; set; }

    #endregion

    public HistoryView()
    {
        InitializeComponent();

        SeriesCollectionExercisesSets = new SeriesCollection();
        SeriesCollectionExercisesReps = new SeriesCollection();
        SeriesCollectionExercisesWeight = new SeriesCollection();
        Labels = new List<string>();

        GetExercisesFromHistory();

        DataContext = this;
    }

    private async Task GetExercisesFromHistory()
    {
        IList<UnitDto> allExercises = new List<UnitDto>();
        try
        {
            allExercises = await Kernel.ClientHub?.V1.UnitClient.GetUnitsAsync()!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        foreach (Tuple<string, List<UnitDto>> exercise in SortAndOrderExercises(allExercises))
        {
            var lineSeriesExerciseSets = new LineSeries
                { Title = exercise.Item1, Values = new ChartValues<int>(), LineSmoothness = 0 };
            var lineSeriesExerciseReps = new LineSeries
                { Title = exercise.Item1, Values = new ChartValues<int>(), LineSmoothness = 0 };
            var lineSeriesExerciseWeight = new LineSeries
                { Title = exercise.Item1, Values = new ChartValues<double>(), LineSmoothness = 0 };

            foreach (UnitDto exerciseData in exercise.Item2)
            {
                if (lineSeriesExerciseSets.Values.Count == 0 && lineSeriesExerciseReps.Values.Count == 0 && lineSeriesExerciseWeight.Values.Count == 0)
                {
                    if (Labels != null)
                    {
                        for (int i = 0; i < Labels.IndexOf(exerciseData.DateUtc.Date.ToString("dd/MM/yyyy")); i++)
                        {
                            lineSeriesExerciseSets.Values.Add(0);
                            lineSeriesExerciseReps.Values.Add(0); 
                            lineSeriesExerciseWeight.Values.Add(0.0);
                        }
                    }
                }

                lineSeriesExerciseSets.Values.Add(exerciseData.Sets);
                lineSeriesExerciseReps.Values.Add(exerciseData.Repetitions);
                lineSeriesExerciseWeight.Values.Add(exerciseData.Weight);
            }

            SeriesCollectionExercisesSets?.Add(lineSeriesExerciseSets);
            SeriesCollectionExercisesReps?.Add(lineSeriesExerciseReps);
            SeriesCollectionExercisesWeight?.Add(lineSeriesExerciseWeight);
        }
    }

    private List<Tuple<string, List<UnitDto>>> SortAndOrderExercises(IList<UnitDto> allExercises)
    {
        IOrderedEnumerable<UnitDto> allOrderedExercises = allExercises.OrderBy(elem => elem.DateUtc);
        var sortedExercises = new List<Tuple<string, List<UnitDto>>>();

        foreach (UnitDto unit in allOrderedExercises)
        {
            if (sortedExercises.Any(elem => elem.Item1 == unit.Type))
            {
                sortedExercises.Single(elem => elem.Item1 == unit.Type).Item2.Add(unit);
            }
            else
            {
                sortedExercises.Add(new Tuple<string, List<UnitDto>>(unit.Type, new List<UnitDto> { unit }));
            }

            if (Labels != null && !Labels.Contains(unit.DateUtc.Date.ToString("dd/MM/yyyy")))
            {
                Labels.Add(unit.DateUtc.Date.ToString("dd/MM/yyyy"));
            }
        }

        return sortedExercises;
    }
}