using MachineVisionNodeEditor.Extension;
using MachineVisionNodeEditor.Models.NodeModels;
using MachineVisionNodeEditor.ViewModels.NodeViewModels;
using MachineVisionNodeEditor.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MachineVisionNodeEditor.Views.Nodes
{
    /// <summary>
    /// Interaction logic for Node_PortView.xaml
    /// </summary>

    public partial class Node_PortView : UserControl
    {
        #region Dependency Properties

        public bool IsConnected
        {
            get { return (bool)GetValue(IsConnectedProperty); }
            set { SetValue(IsConnectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsConnected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsConnectedProperty =
            DependencyProperty.Register(nameof(IsConnected), typeof(bool), typeof(Node_PortView), new PropertyMetadata(false));
        #endregion

        #region Routed Events
        // ===== PortMouseDown =====
        public static readonly RoutedEvent PortMouseDownEvent = EventManager.RegisterRoutedEvent(
            name: "PortMouseDown",
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedPropertyChangedEventHandler<PortModel>), // ← sửa thành PortModel
            ownerType: typeof(PortModel));

        public event RoutedPropertyChangedEventHandler<PortModel> PortMouseDown
        {
            add { AddHandler(PortMouseDownEvent, value); }
            remove { RemoveHandler(PortMouseDownEvent, value); }
        }

        // ===== PortMouseUp =====
        public static readonly RoutedEvent PortMouseUpEvent = EventManager.RegisterRoutedEvent(
            name: "PortMouseUp",
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedPropertyChangedEventHandler<PortModel>),
            ownerType: typeof(PortModel));

        public event RoutedPropertyChangedEventHandler<PortModel> PortMouseUp
        {
            add { AddHandler(PortMouseUpEvent, value); }
            remove { RemoveHandler(PortMouseUpEvent, value); }
        }

        #endregion

        public Node_PortViewModel Node_PortViewModel { get; }

        public Node_PortView()
        {
            InitializeComponent();
            Node_PortViewModel = this.DataContext as Node_PortViewModel;
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Lấy PortModel từ Tag của UserControl (không phải Tag của Button)
            var port = this.DataContext as PortModel;
            if (port == null) return;

            // Tìm Canvas cha để tính toạ độ tuyệt đối
            var canvas = UIHelper.FindVisualParent<Canvas>(this);
            if (canvas != null)
                port.Position = this.TransformToAncestor(canvas)
                                    .Transform(new Point(ActualWidth / 2, ActualHeight / 2));

            RaiseEvent(new RoutedPropertyChangedEventArgs<PortModel>(null, port)
            {
                RoutedEvent = PortMouseDownEvent
            });

            Node_NodeView.DraggingPort = port;
            Node_NodeView.DraggingPort.Position = port.Position;
            e.Handled = true;
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Lấy PortModel từ Tag của UserControl (không phải Tag của Button)
            var targetPort = this.DataContext as PortModel;
            if (targetPort == null) return;

            // Tìm Canvas cha để tính toạ độ tuyệt đối
            var canvas = UIHelper.FindVisualParent<Canvas>(this);
            if (canvas != null)
                targetPort.Position = this.TransformToAncestor(canvas)
                                    .Transform(new Point(ActualWidth / 2, ActualHeight / 2));

            var startPort = Node_NodeView.DraggingPort;


            if (startPort != null)
            {
                if (UIHelper.IsValidConnection(startPort, targetPort))
                {
                    var connectionViewModel = MainWindow.Instance.Window_MainWindowViewModel.AddConnection(startPort, targetPort, startPort.Position, targetPort.Position);
                    if (connectionViewModel != null)
                    {
                        connectionViewModel.ConnectionModel.FromPort = startPort;
                        connectionViewModel.ConnectionModel.ToPort = targetPort;

                        startPort.IsConnected = true;
                        targetPort.IsConnected = true;

                        #region Add new port when previous port was connected
                        //Node_NodeView view = UIHelper.FindNodeElement<Node_NodeView>(this);
                        //targetPort.Owner.InputPorts.Add(new PortModel() { Owner = targetPort.Owner, Position = new Point(startPort.Position.X, startPort.Position.Y + 20), Type = PortType.Output });

                        //if (view != null && view.DataContext is Node_NodeViewModel nodeViewModel)
                        //{
                        //    Node_PortView portView = new Node_PortView() { Margin = new Thickness(0, 5, 0, 0) };
                        //    Node_PortViewModel portViewModel = portView.DataContext as Node_PortViewModel;
                        //    portViewModel.PortModel = startPort.Owner.OutputPorts.Last();

                        //    view.StackPanel_InputPorts.Children.Add(portView);
                        //}
                        #endregion
                    }

                }

            }


            RaiseEvent(new RoutedPropertyChangedEventArgs<PortModel>(null, targetPort)
            {
                RoutedEvent = PortMouseUpEvent
            });

            e.Handled = true;
        }
    }
    
}
