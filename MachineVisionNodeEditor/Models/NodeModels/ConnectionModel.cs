using MachineVisionNodeEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MachineVisionNodeEditor.Models.NodeModels
{
    public class ConnectionModel : BaseViewModel
    {
        private Point _start, _end, _control1, _control2;

        public Point Start
        {
            get => _start;
            set { _start = value; OnPropertyChanged(); } 
        }

        public Point End
        {
            get => _end;
            set { _end = value; OnPropertyChanged(); }
        }

        public Point Control1
        {
            get => _control1;
            set { _control1 = value; OnPropertyChanged(); OnPropertyChanged(nameof(PathData)); }
        }

        public Point Control2
        {
            get => _control2;
            set { _control2 = value; OnPropertyChanged(); OnPropertyChanged(nameof(PathData)); }
        }

        public PortModel FromPort { get; set; }
        public PortModel ToPort { get; set; }

        public void UpdateControls()
        {
            double dx = (End.X - Start.X) * 0.6;
            Control1 = new Point(Start.X + dx, Start.Y);
            Control2 = new Point(End.X - dx, End.Y);
        }

        public Geometry PathData
        {
            get
            {
                var p1 = Start;
                var p2 = End;
                double dx = Math.Abs(p2.X - p1.X) * 0.6;

                var fig = new PathFigure { StartPoint = p1, IsFilled = false };
                fig.Segments.Add(new BezierSegment(
                    new Point(p1.X + dx, p1.Y),
                    new Point(p2.X - dx, p2.Y),
                    p2, isStroked: true));

                return new PathGeometry(new[] { fig });
            }
        }
    }
}
