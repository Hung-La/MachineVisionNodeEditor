using MachineVisionNodeEditor.ViewModels;
using MachineVisionNodeEditor.ViewModels.NodeViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineVisionNodeEditor.Models.NodeModels
{
    public enum NodeType
    {
        Node,
        ImageImport
    }
    public class NodeModel : BaseViewModel
    {
        private string _title;
        private string _description;
        private double _x, _y;

        public string Title
        {
            get => _title;
            set { _title = value; }
        }

        public string Description 
        { 
            get => _description; 
            set => _description = value; 
        }

        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(nameof(X)); }
        }

        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(nameof(Y)); }
        }

        public ObservableCollection<Node_PortViewModel> InputPorts { get; set; } = new ObservableCollection<Node_PortViewModel>() { };//Type = PortType.Input };
        public ObservableCollection<Node_PortViewModel> OutputPorts { get; set; } = new ObservableCollection<Node_PortViewModel>() { };//Type = PortType.Output };

        public FrameworkElement View
        {
            get;
            set;
        }

        public NodeType Type { get; set; }

        public NodeModel(string title, double x, double y)
        {
            Title = title;
            X = x;
            Y = y;
            if (InputPorts.Count != 0)
            {
                foreach (Node_PortViewModel port in InputPorts)
                {
                    port.PortModel.Owner = this;
                }
            }

            if (OutputPorts.Count != 0)
            {
                foreach (Node_PortViewModel port in OutputPorts)
                {
                    port.PortModel.Owner = this;
                }
            }
            //InputPort.Owner = this;
            //OutputPort.Owner = this;

            //InputPort.Type = PortType.Input;
            //OutputPort.Type = PortType.Output;
        }

        public NodeModel() { }

        public Node_PortViewModel AddPort(PortType type)
        {
            var port = new Node_PortViewModel();
            port.PortModel.Type = type;
            port.PortModel.Owner = this;
            //port.PortModel.Connected += OnPortConnected;

            if (type == PortType.Input)
            {
                InputPorts.Add(port);
            }
            else if ( type == PortType.Output)
            {
                OutputPorts.Add(port);
            }

            return port;
        }

        private void OnPortConnected(PortModel connectedPort)
        {
            // Ensure there is always at least one free (unconnected) port.
            if (connectedPort.Type == PortType.Input)
            {
                bool hasFree = false;
                foreach (var p in InputPorts)
                    if (!p.PortModel.IsConnected) { hasFree = true; break; }
                if (!hasFree) AddPort(PortType.Input);
            }
            else
            {
                bool hasFree = false;
                foreach (var p in OutputPorts)
                    if (!p.PortModel.IsConnected) { hasFree = true; break; }
                if (!hasFree) AddPort(PortType.Output);
            }
        }
    }
}
