using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineVisionNodeEditor.ViewModels;

namespace MachineVisionNodeEditor.Models.NodeModels
{
    public enum PortType
    {
        Input,
        Output
    }
    public class PortModel : BaseViewModel
    {
        public PortType Type { get; set; }

        public bool IsConnected { get; set; } = false;

        private NodeModel owner;
        public NodeModel Owner { get => owner; set { owner = value; OnPropertyChanged(); } }

        private Point position;
        public Point Position { get => position; set { position = value; OnPropertyChanged(); } }
    }
}
 