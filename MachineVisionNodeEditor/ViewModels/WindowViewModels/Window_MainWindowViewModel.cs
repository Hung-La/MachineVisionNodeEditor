using MachineVisionNodeEditor.Models.NodeModels;
using MachineVisionNodeEditor.ViewModels.NodeViewModels.ImageImport_NodeViewModels;
using MachineVisionNodeEditor.ViewModels.NodeViewModels;
using MachineVisionNodeEditor.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using MachineVisionNodeEditor.Models.NodeModels.ImageImport_NodeModels;
using MachineVisionNodeEditor.Views.Nodes;

namespace MachineVisionNodeEditor.ViewModels.WindowViewModels
{
    public class Window_MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private bool isChecked_ToggleTheme;
        private Node_ConnectionViewModel _pendingConnection;
        private bool _isDraggingConnection;
        #endregion

        #region Properties
        public bool IsChecked_ToggleTheme { get => isChecked_ToggleTheme; set { isChecked_ToggleTheme = value; OnPropertyChanged(); } }

        public Node_ConnectionViewModel PendingConnection
        {
            get => _pendingConnection;
            set { _pendingConnection = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Node_NodeViewModel> Nodes { get; set; } = new ObservableCollection<Node_NodeViewModel>();
        public ObservableCollection<ImageImport_NodeViewModel> ImageImportNodes { get; set; } = new ObservableCollection<ImageImport_NodeViewModel>();

        public ObservableCollection<Node_ConnectionViewModel> Connections { get; set; } = new();
        #endregion

        #region Commands
        public ICommand ChangeTheme {  get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand PortMouseDownCommand { get; set; }
        public ICommand CanvasMouseMoveCommand { get; set; }
        public ICommand CanvasMouseUpCommand { get; set; }
        #endregion

        public Window_MainWindowViewModel() 
        {
            ChangeTheme = new RelayCommand<Window>((p) => { return true; },(p)=>
            {
                if (IsChecked_ToggleTheme)
                {
                    AppTheme.ChangeTheme(new Uri("Resources/Themes/DarkTheme.xaml", UriKind.Relative));
                }
                else { AppTheme.ChangeTheme(new Uri("Resources/Themes/LightTheme.xaml", UriKind.Relative)); }
            });

            var node1 = new NodeModel("Node 1", 80, 120);
            var node2 = new NodeModel("Node 2", 380, 200);
            var node3 = new NodeModel("Node 3", 680, 120);

            Nodes.Add(new Node_NodeViewModel(node1));
            Nodes.Add(new Node_NodeViewModel(node2));
            Nodes.Add(new Node_NodeViewModel(node3));


            // ===== Bắt đầu kéo từ port =====
            //PortMouseDownCommand = new RelayCommand<PortModel>(
            //    p => true,
            //    port =>
            //    {
            //        _isDraggingConnection = true;

            //        PendingConnection = new Node_ConnectionViewModel
            //        {
            //            FromPort = port,
            //            Start = port.Position,
            //            End = port.Position
            //        };
            //        PendingConnection.UpdateControls();
            //        Connections.Add(PendingConnection);
            //    });

            // ===== Di chuột =====
            CanvasMouseMoveCommand = new RelayCommand<Point>(
                p => _isDraggingConnection,
                mousePos =>
                {
                    if (PendingConnection == null) return;
                    PendingConnection.ConnectionModel.End = mousePos;
                    PendingConnection.ConnectionModel.UpdateControls();
                });

            // ===== Thả chuột =====
            CanvasMouseUpCommand = new RelayCommand<PortModel>(
                p => _isDraggingConnection,
                targetPort =>
                {
                    _isDraggingConnection = false;

                    if (targetPort != null
                        && targetPort != PendingConnection.ConnectionModel.FromPort
                        && targetPort.Owner != PendingConnection.ConnectionModel.FromPort.Owner)
                    {
                        // ✅ Nối thành công
                        PendingConnection.ConnectionModel.ToPort = targetPort;
                        PendingConnection.ConnectionModel.End = targetPort.Position;
                        PendingConnection.ConnectionModel.UpdateControls();
                    }
                    else
                    {
                        // ❌ Hủy
                        Connections.Remove(PendingConnection);
                    }

                    PendingConnection = null;
                });

        }

        public Node_ConnectionViewModel? AddConnection(PortModel fromPort, PortModel toPort, Point startPoint, Point endPoint)
        {
            // Không cho nối 2 lần cùng cặp port
            if (Connections.Any(c => c.ConnectionModel.FromPort == fromPort && c.ConnectionModel.ToPort == toPort))
                return null;

            var model = new ConnectionModel();
            model.FromPort = fromPort; model.ToPort = toPort;
            model.Start = startPoint;
            model.End = endPoint;
            var vm = new Node_ConnectionViewModel(model);
            vm.ConnectionModel.UpdateControls();
            Connections.Add(vm);
            return vm;
        }

        public void AddNewNode()
        {
            var r = new Random();
            var node = new NodeModel("Node", r.Next(50, 400), r.Next(50, 300));
            Nodes.Add(new Node_NodeViewModel(node));
        }

        public void AddNewImageImportNode()
        {
            var r = new Random();
            var node = new ImageImport_NodeModel();
            node.Title = "Image Import";
            node.X = r.Next(50, 400);
            node.Y = r.Next(50, 300);
            Nodes.Add(new ImageImport_NodeViewModel(node));
        }
    }
}
