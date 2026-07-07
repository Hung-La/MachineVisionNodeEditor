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
            nodeModel.Title = "Test Node";
            NodeModel = nodeModel;
            EnsureInitialPorts();

        }

        public Node_NodeViewModel() 
        {
            NodeModel.Title = "Test Node";
            EnsureInitialPorts();

        }

        private void EnsureInitialPorts()
        {
            if (NodeModel.InputPorts.Count == 0) NodeModel.AddPort(PortType.Input);
            if (NodeModel.OutputPorts.Count == 0) NodeModel.AddPort(PortType.Output);
        }

    }
}
