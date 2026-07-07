using MachineVisionNodeEditor.Extension;
using MachineVisionNodeEditor.Interfaces.NodeInterfaces;
using MachineVisionNodeEditor.Models.NodeModels;
using MachineVisionNodeEditor.ViewModels.NodeViewModels;
using MachineVisionNodeEditor.ViewModels.NodeViewModels.ImageImport_NodeViewModels;
using MachineVisionNodeEditor.Views.Nodes.ImageImport;
using MachineVisionNodeEditor.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineVisionNodeEditor.Views.Nodes
{
    public abstract class NodeControl : UserControl
    {
        public static PortModel? DraggingPort { get; set; }
        public NodeControl_NodeViewModel? NodeControl_NodeViewModel { get; set; }

        protected bool _dragging;
        protected Point _dragOrigin;
        protected Canvas? _canvas;

        public NodeControl()
        {
            
        }

        protected void NodeControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is NodeControl_NodeViewModel viewModel)
            {
                viewModel.NodeModel.View = this;
            }
        }

        public static void ClearDraggingPort() => DraggingPort = null;

        public virtual FrameworkElement? GetPortElement(PortModel port) => null;

        protected void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            _canvas = UIHelper.FindVisualParent<Canvas>(this);
            _dragging = true;
            _dragOrigin = e.GetPosition(_canvas);
            ((UIElement)sender).CaptureMouse();
            e.Handled = true;
        }

        protected void Header_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            if (!_dragging || _canvas == null) return;

            var pos = e.GetPosition(_canvas);
            var delta = pos - _dragOrigin;
            _dragOrigin = pos;

            //if (DataContext is Node_NodeViewModel m)
            //{
            //    m.NodeModel.X += delta.X;
            //    m.NodeModel.Y += delta.Y;

            //    m.NodeModel.OutputPort.Position += delta;
            //    m.NodeModel.InputPort.Position += delta;

            //    var outputConnections = UIHelper.FindConnectionElement(m.NodeModel.OutputPort);
            //    if (outputConnections != null)
            //        foreach (var item in outputConnections)
            //            if (item.DataContext is Node_ConnectionViewModel vm)
            //            {
            //                vm.ConnectionModel.Start = m.NodeModel.OutputPort.Position;
            //                vm.ConnectionModel.UpdateControls();
            //            }

            //    var inputConnections = UIHelper.FindConnectionElement(m.NodeModel.InputPort);
            //    if (inputConnections != null)
            //        foreach (var item in inputConnections)
            //            if (item.DataContext is Node_ConnectionViewModel vm)
            //            {
            //                vm.ConnectionModel.End = m.NodeModel.InputPort.Position;
            //                vm.ConnectionModel.UpdateControls();
            //            }
            //}

            // Dùng INodeViewModel thay vì Node_NodeViewModel
            if (DataContext is INodeViewModel m)
            {
                m.NodeModel.X += delta.X;
                m.NodeModel.Y += delta.Y;

                m.NodeModel.OutputPorts.Select(p => p.PortModel.Position += delta).ToList();
                m.NodeModel.InputPorts.Select(p => p.PortModel.Position += delta).ToList();

                //foreach (var item in m.NodeModel.OutputPorts)
                //{
                //    item.Position += delta;
                //}

                //foreach (var item in m.NodeModel.InputPorts)
                //{
                //    item.Position += delta;
                //}

                if (m.NodeModel.OutputPorts.Count != 0)
                {
                    var outputConnections = UIHelper.FindConnectionElement(m.NodeModel.OutputPorts[0].PortModel);
                    if (outputConnections != null)
                        foreach (var item in outputConnections)
                        {
                            if (item.DataContext is Node_ConnectionViewModel vm)
                            {
                                vm.ConnectionModel.Start = m.NodeModel.OutputPorts[0].PortModel.Position;
                                vm.ConnectionModel.UpdateControls();
                            }
                        }

                }

                if (m.NodeModel.InputPorts.Count != 0)
                {
                    var inputConnections = UIHelper.FindConnectionElement(m.NodeModel.InputPorts[0].PortModel);
                    if (inputConnections != null)
                        foreach (var item in inputConnections)
                        {
                            if (item.DataContext is Node_ConnectionViewModel vm)
                            {
                                vm.ConnectionModel.End = m.NodeModel.InputPorts[0].PortModel.Position;
                                vm.ConnectionModel.UpdateControls();
                            }
                        }
                }

            }
        }

        protected void Header_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _dragging = false;
            ((UIElement)sender).ReleaseMouseCapture();
        }

        protected void Port_PortMouseUp(object sender, RoutedPropertyChangedEventArgs<PortModel> e)
        {
            ClearDraggingPort();
            MainWindow.Instance.PreviewPath.Data = null;
        }
    }
}
