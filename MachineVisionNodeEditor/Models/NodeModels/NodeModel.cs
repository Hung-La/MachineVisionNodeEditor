using MachineVisionNodeEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVisionNodeEditor.Models.NodeModels
{
    public class NodeModel : BaseViewModel
    {
        private string _title;
        private double _x, _y;

        public string Title
        {
            get => _title;
            set { _title = value; }
        }

        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(); }
        }

        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(); }
        }

        public ObservableCollection<PortModel> InputPorts { get; set; } = new ObservableCollection<PortModel>() { };//Type = PortType.Input };
        public ObservableCollection<PortModel> OutputPorts { get; set; } = new ObservableCollection<PortModel>() { };//Type = PortType.Output };

        public NodeModel(string title, double x, double y)
        {
            Title = title;
            X = x;
            Y = y;
            if (InputPorts.Count != 0)
            {
                foreach (PortModel port in InputPorts)
                {
                    port.Owner = this;
                }
            }

            if (OutputPorts.Count != 0)
            {
                foreach (PortModel port in OutputPorts)
                {
                    port.Owner = this;
                }
            }
            //InputPort.Owner = this;
            //OutputPort.Owner = this;

            //InputPort.Type = PortType.Input;
            //OutputPort.Type = PortType.Output;
        }

        public NodeModel() { }
    }
}
