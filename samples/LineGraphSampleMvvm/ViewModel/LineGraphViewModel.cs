using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LineGraphSampleMvvm.ViewModel
{
    public class LineGraphViewModel : ViewModelBase
    {
        private Brush _stroke;
        private string _description;
        private double _strokeThickness;
        private PointCollection _points;

        public Brush Stroke { get => _stroke; set => Set(ref _stroke, value); }

        public string Description { get => _description; set => Set(ref _description, value); }

        public double StrokeThickness { get => _strokeThickness; set => Set(ref _strokeThickness, value); }

        public PointCollection Points { get => _points; set => Set(ref _points, value); }
    }
}
