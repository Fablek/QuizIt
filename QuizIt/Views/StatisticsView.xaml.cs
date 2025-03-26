using System;
using System.Linq;
using System.Windows.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using QuizIt.Models;

namespace QuizIt.Views
{
    public partial class StatisticsView : UserControl
    {
        private readonly ViewModels.MainViewModel _viewModel;

        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public StatisticsView()
        {
            InitializeComponent();

            var main = App.Current.MainWindow as MainWindow;
            _viewModel = main.DataContext as ViewModels.MainViewModel;

            var flashcards = _viewModel.Results
                .Select(r => r.FlashcardTitle)
                .Distinct()
                .ToList();

            FlashcardSelector.ItemsSource = flashcards;
            if (flashcards.Any())
                FlashcardSelector.SelectedIndex = 0;
        }

        private void FlashcardSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FlashcardSelector.SelectedItem is string selectedFlashcard)
            {
                var filtered = _viewModel.Results
                    .Where(r => r.FlashcardTitle == selectedFlashcard)
                    .OrderBy(r => r.Date)
                    .ToList();

                Series = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        Values = filtered.Select(r => r.Percentage).ToArray(),
                        Fill = null,
                        GeometrySize = 8,
                        Stroke = new SolidColorPaint(SKColors.OrangeRed, 2),
                        Name = selectedFlashcard
                    }
                };

                XAxes = new Axis[]
                {
                    new Axis
                    {
                        Labels = filtered.Select(r => r.Date.ToShortDateString()).ToArray(),
                        LabelsRotation = 15,
                        Name = "Data"
                    }
                };

                YAxes = new Axis[]
                {
                    new Axis
                    {
                        MinLimit = 0,
                        MaxLimit = 100,
                        Name = "Skuteczność (%)"
                    }
                };

                DataContext = null;
                DataContext = this;
            }
        }
    }
}
