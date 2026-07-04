using MachineVisionNodeEditor.Models.NodeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVisionNodeEditor.ViewModels.NodeViewModels
{
    public class Node_ConnectionViewModel : BaseViewModel
    {
        private ConnectionModel _connectionModel;

        public ConnectionModel ConnectionModel { get => _connectionModel; set { _connectionModel = value; OnPropertyChanged(); } }

        public Node_ConnectionViewModel (ConnectionModel connectionModel)
        {
            ConnectionModel = connectionModel;
        }

        public Node_ConnectionViewModel() { }
    }
}
