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

    public SeriesCollection? SeriesCollectionHealthData { get; }
    public List<string>? HealthDataLabels { get; }
    public SeriesCollection? SeriesCollectionExercisesSets { get; }
    public SeriesCollection? SeriesCollectionExercisesReps { get; }
    public SeriesCollection? SeriesCollectionExercisesWeight { get; }
    public List<string>? ExerciseLabels { get; }

    #endregion

    public HistoryView()
    {
        InitializeComponent();

        SeriesCollectionHealthData = new SeriesCollection();

        SeriesCollectionExercisesSets = new SeriesCollection();
        SeriesCollectionExercisesReps = new SeriesCollection();
        SeriesCollectionExercisesWeight = new SeriesCollection();
        HealthDataLabels = new List<string>();
        ExerciseLabels = new List<string>();

        GetHealthDataFromHistory().ConfigureAwait(false);
        GetExercisesFromHistory().ConfigureAwait(false);

        DataContext = this;
    }

    private async Task GetHealthDataFromHistory()
    {
        IList<HealthUnitDto> allHealthData;
        try
        {
            allHealthData = await Kernel.ClientHub?.V1.HealthUnitClient.GetHealthUnitsAsync()!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var lineSeriesHealthDataWeight = new LineSeries
            { Title = "Gewicht", Values = new ChartValues<double>(), LineSmoothness = 0 };
        var lineSeriesHealthDataArmSize = new LineSeries
            { Title = "Armumfang", Values = new ChartValues<double>(), LineSmoothness = 0 };
        var lineSeriesHealthDataWaistSize = new LineSeries
            { Title = "Taillenumfang", Values = new ChartValues<double>(), LineSmoothness = 0 };
        var lineSeriesHealthDataHipSize = new LineSeries
            { Title = "Hüftumfang", Values = new ChartValues<double>(), LineSmoothness = 0 };
        var lineSeriesHealthDataThighSize = new LineSeries
            { Title = "Oberschenkelumfang", Values = new ChartValues<double>(), LineSmoothness = 0 };

        foreach (HealthUnitDto healthData in allHealthData.OrderBy(elem => elem.DateUtc))
        {
            if ((lineSeriesHealthDataWeight.Values.Count == 0 && lineSeriesHealthDataArmSize.Values.Count == 0 &&
                 lineSeriesHealthDataWaistSize.Values.Count == 0)
                || (lineSeriesHealthDataHipSize.Values.Count == 0 && lineSeriesHealthDataThighSize.Values.Count == 0))
            {
                if (HealthDataLabels != null)
                {
                    for (int i = 0;
                         i < HealthDataLabels.IndexOf(healthData.DateUtc.Date.ToString("dd/MM/yyyy"));
                         i++)
                    {
                        lineSeriesHealthDataWeight.Values.Add(0.0);
                        lineSeriesHealthDataArmSize.Values.Add(0.0);
                        lineSeriesHealthDataWaistSize.Values.Add(0.0);
                        lineSeriesHealthDataHipSize.Values.Add(0.0);
                        lineSeriesHealthDataThighSize.Values.Add(0.0);
                    }
                }
            }

            HealthDataLabels?.Add(healthData.DateUtc.Date.ToString("dd/MM/yyyy"));

            lineSeriesHealthDataWeight.Values.Add(healthData.Weight);
            lineSeriesHealthDataArmSize.Values.Add(healthData.ArmSize);
            lineSeriesHealthDataWaistSize.Values.Add(healthData.WaistSize);
            lineSeriesHealthDataHipSize.Values.Add(healthData.HipSize);
            lineSeriesHealthDataThighSize.Values.Add(healthData.ThightSize);
        }

        SeriesCollectionHealthData?.Add(lineSeriesHealthDataWeight);
        SeriesCollectionHealthData?.Add(lineSeriesHealthDataArmSize);
        SeriesCollectionHealthData?.Add(lineSeriesHealthDataWaistSize);
        SeriesCollectionHealthData?.Add(lineSeriesHealthDataHipSize);
        SeriesCollectionHealthData?.Add(lineSeriesHealthDataThighSize);
    }

    private async Task GetExercisesFromHistory()
    {
        IList<UnitDto> allExercises;
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
                { Title = exercise.Item1, Values = new ChartValues<long>(), LineSmoothness = 0 };
            var lineSeriesExerciseReps = new LineSeries
                { Title = exercise.Item1, Values = new ChartValues<long>(), LineSmoothness = 0 };
            var lineSeriesExerciseWeight = new LineSeries
                { Title = exercise.Item1, Values = new ChartValues<double>(), LineSmoothness = 0 };

            foreach (UnitDto exerciseData in exercise.Item2)
            {
                if (lineSeriesExerciseSets.Values.Count == 0 && lineSeriesExerciseReps.Values.Count == 0 &&
                    lineSeriesExerciseWeight.Values.Count == 0)
                {
                    if (ExerciseLabels != null)
                    {
                        for (int i = 0;
                             i < ExerciseLabels.IndexOf(exerciseData.DateUtc.Date.ToString("dd/MM/yyyy"));
                             i++)
                        {
                            lineSeriesExerciseSets.Values.Add((long)0);
                            lineSeriesExerciseReps.Values.Add((long)0);
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

            if (ExerciseLabels != null && !ExerciseLabels.Contains(unit.DateUtc.Date.ToString("dd/MM/yyyy")))
            {
                ExerciseLabels.Add(unit.DateUtc.Date.ToString("dd/MM/yyyy"));
            }
        }

        return sortedExercises;
    }
}