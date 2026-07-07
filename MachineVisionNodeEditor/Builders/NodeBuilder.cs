using MachineVisionNodeEditor.Interfaces.NodeInterfaces;
using MachineVisionNodeEditor.Models.NodeModels;
using MachineVisionNodeEditor.ViewModels.NodeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVisionNodeEditor.Builders
{
    public class NodeBuilder
    {
        private readonly NodeModel NodeModel = new();

        public NodeBuilder SetNodeType(NodeType type)
        {
            NodeModel.Type = type;
            return this;
        }

        public NodeBuilder SetCoordinate(double X, double Y)
        {
            NodeModel.X = X;
            NodeModel.Y = Y;
            return this;
        }

        public NodeBuilder SetTitle(string title)
        {
            NodeModel.Title = title;
            return this;
        }

        public NodeBuilder SetDescription (string desciption)
        {
            NodeModel.Description = desciption;
            return this;
        }

        public NodeModel Build()
        {
            return NodeModel;
        }
    }
}
