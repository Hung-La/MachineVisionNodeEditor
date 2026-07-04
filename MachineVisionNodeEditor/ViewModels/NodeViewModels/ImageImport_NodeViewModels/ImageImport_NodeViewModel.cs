using MachineVisionNodeEditor.Interfaces.NodeInterfaces;
using MachineVisionNodeEditor.Models.NodeModels;
using MachineVisionNodeEditor.Models.NodeModels.ImageImport_NodeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVisionNodeEditor.ViewModels.NodeViewModels.ImageImport_NodeViewModels
{
    public class ImageImport_NodeViewModel : Node_NodeViewModel
    {
        //private ImageImport_NodeModel _imageImport_NodeModel;

        //public ImageImport_NodeModel ImageImport_NodeModel { get => _imageImport_NodeModel; set { _imageImport_NodeModel = value; OnPropertyChanged(); } }

        //private NodeModel nodeModel;

        //public NodeModel NodeModel { get => nodeModel; set { nodeModel = value; OnPropertyChanged(); } }

        public ImageImport_NodeViewModel (ImageImport_NodeModel nodeModel)
        {
            NodeModel = nodeModel;
        }
        public ImageImport_NodeViewModel (){}
    }
}
