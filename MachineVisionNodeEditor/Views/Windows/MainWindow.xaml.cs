using MachineVisionNodeEditor.Builders;
using MachineVisionNodeEditor.Extension;
using MachineVisionNodeEditor.Models.NodeModels;
using MachineVisionNodeEditor.ViewModels.NodeViewModels;
using MachineVisionNodeEditor.ViewModels.WindowViewModels;
using MachineVisionNodeEditor.Views.Nodes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MachineVisionNodeEditor.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow instance;
        private static readonly object _lock = new object();

        public static MainWindow Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new MainWindow();
                        
                    }
                    return instance;
                }
            }

        }

        public Window_MainWindowViewModel Window_MainWindowViewModel {  get; } = new Window_MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Window_MainWindowViewModel;
            instance = this;
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            var cursorCoordinate = e.GetPosition(MainCanvas);
            TextBlock_CursorCoordinate.Text = $"(X: {cursorCoordinate.X:0}, Y: {cursorCoordinate.Y:0})";

            if (e.LeftButton != MouseButtonState.Pressed) return;

            if (NodeControl.DraggingPort == null) return;

            //var startEl = UIHelper.FindPortElement(NodeControl.DraggingPort);
            var startEl = NodeControl.DraggingPort.View;
            if (startEl == null) return;

            Point start = UIHelper.GetCenter(startEl, MainCanvas);
            Point end = e.GetPosition(MainCanvas);

            PreviewPath.Data = MakeBezier(start, end);

            e.Handled = true;
        }
        private void MainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var dragging = NodeControl.DraggingPort;
            if (dragging == null) return;

            try
            {
                // Hit-test toàn bộ visual tree tại vị trí thả chuột
                var pos = e.GetPosition(MainCanvas);
                PortModel? target = UIHelper.HitTestPort(pos);

                if (target != null && UIHelper.IsValidConnection(dragging, target))
                {
                    var fromEl = UIHelper.FindPortElement(dragging);
                    var toEl = UIHelper.FindPortElement(target);

                    if (fromEl != null && toEl != null)
                    {
                        Point startPt = UIHelper.GetCenter(fromEl, MainCanvas);
                        Point endPt = UIHelper.GetCenter(toEl, MainCanvas);
                        
                    }
                }
            }
            finally
            {
                NodeControl.ClearDraggingPort();
                PreviewPath.Data = null;
                e.Handled = true;
            }
        }

        private static PathGeometry MakeBezier(Point p1, Point p2)
        {
            double dx = Math.Abs(p2.X - p1.X) * 0.6;
            var fig = new PathFigure { StartPoint = p1, IsFilled = false };
            fig.Segments.Add(new BezierSegment(
                new Point(p1.X + dx, p1.Y),
                new Point(p2.X - dx, p2.Y),
                p2, isStroked: true));
            return new PathGeometry(new[] { fig });
        }

        private void RibbonButton_ImageImport_Click(object sender, RoutedEventArgs e)
        {
            var random = new Random();
            var nodeModel = new NodeBuilder()
                .SetNodeType(NodeType.ImageImport)
                .SetCoordinate(random.Next(100, 200), random.Next(100, 200))
                .Build();
            Window_MainWindowViewModel.AddNode(nodeModel);
        }

        private void RibbonButton_Test_Click(object sender, RoutedEventArgs e)
        {
            var random = new Random();
            var nodeModel = new NodeBuilder()
                .SetNodeType(NodeType.Node)
                .SetCoordinate(random.Next(300, 400), random.Next(50, 200))
                .Build();
            Window_MainWindowViewModel.AddNode(nodeModel);
        }
    }
}