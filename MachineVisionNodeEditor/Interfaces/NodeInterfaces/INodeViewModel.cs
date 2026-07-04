using MachineVisionNodeEditor.Models.NodeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVisionNodeEditor.Interfaces.NodeInterfaces
{
    public interface INodeViewModel
    {
        NodeModel NodeModel { get; }
    }
}
