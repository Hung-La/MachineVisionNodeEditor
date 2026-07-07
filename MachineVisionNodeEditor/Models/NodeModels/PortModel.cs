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

        public event Action<PortModel>? Connected;

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                if (SetField(ref _isConnected, value) && value)
                    Connected?.Invoke(this);
            }
        }

        private NodeModel owner;
        public NodeModel Owner { get => owner; set { owner = value; OnPropertyChanged(); } }

        private Point position;
        public Point Position { get => position; set { position = value; OnPropertyChanged(); } }

        public FrameworkElement View
        {
            get;
            set;
        }
    }
}
