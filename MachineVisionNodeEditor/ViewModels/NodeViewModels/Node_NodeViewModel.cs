using MachineVisionNodeEditor.Interfaces.NodeInterfaces;
using MachineVisionNodeEditor.Models.NodeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVisionNodeEditor.ViewModels.NodeViewModels
{
    public class Node_NodeViewModel : NodeControl_NodeViewModel
    {
        //private NodeModel _nodeModel;

        //public NodeModel NodeModel { get => _nodeModel; set { _nodeModel = value; OnPropertyChanged(); } }

        public Node_NodeViewModel(NodeModel nodeModel)
        {
            NodeModel = nodeModel;
        }

        public Node_NodeViewModel() { }

     
    }
}
