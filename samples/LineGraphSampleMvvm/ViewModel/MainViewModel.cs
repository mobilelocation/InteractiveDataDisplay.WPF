using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InteractiveDataDisplay.WPF;

namespace LineGraphSampleMvvm.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<LineGraphViewModel> _lineGraphViewModels;
        private string _title;
        private TicksProvider _xAxisTicksProvider;
        private bool _isXAxisReversed;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            var lineGraphVms = new List<LineGraphViewModel>();

            double[] x = new double[200];
            for (int i = 0; i < x.Length; i++)
                x[i] = 3.1415 * i / (x.Length - 1);

            for (int i = 0; i < 25; i++)
            {
                var lg = new LineGraphViewModel
                {
                    Stroke = new SolidColorBrush(Color.FromArgb(255, 0, (byte)(i * 10), 0)),
                    Description = string.Format("Data series {0}", i + 1),
                    StrokeThickness = 2,
                    Points = new PointCollection(x.Select(v => new Point(v, Math.Sin(v + i / 10.0))))
                };

                lineGraphVms.Add(lg);
            }

            LineGraphViewModels = new ObservableCollection<LineGraphViewModel>(lineGraphVms);

            Title = "Line graph legnend sample MVVM";

            var ticksProvider = new TicksProvider();
            ticksProvider.MinorProvider.TicksCount = 4;
            XAxisTicksProvider = ticksProvider;

            IsXAxisReversed = true;
        }

        public bool IsXAxisReversed { get => _isXAxisReversed; set => Set(ref _isXAxisReversed, value); }

        /// <summary>
        /// The collection of ViewModels for the LineGraphs.
        /// </summary>
        public ObservableCollection<LineGraphViewModel> LineGraphViewModels { get => _lineGraphViewModels; set => Set(ref _lineGraphViewModels, value); }

        public TicksProvider XAxisTicksProvider { get => _xAxisTicksProvider; set => Set(ref _xAxisTicksProvider, value); }

        /// <summary>
        /// The title
        /// </summary>
        public string Title { get => _title; set => Set(ref _title, value); }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}