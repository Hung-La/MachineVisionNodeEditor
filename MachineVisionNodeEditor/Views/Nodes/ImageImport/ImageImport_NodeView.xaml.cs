using MachineVisionNodeEditor.Interfaces.NodeInterfaces;
using MachineVisionNodeEditor.Models.NodeModels;
using MachineVisionNodeEditor.ViewModels.NodeViewModels;
using MachineVisionNodeEditor.ViewModels.NodeViewModels.ImageImport_NodeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MachineVisionNodeEditor.Views.Nodes.ImageImport
{
    /// <summary>
    /// Interaction logic for ImageImport_NodeView.xaml
    /// </summary>
    public partial class ImageImport_NodeView : NodeControl
    {
        public Node_PortViewModel Output_PortViewModel { get; }
        public ImageImport_NodeViewModel ImageImport_NodeViewModel { get; private set; } = new ImageImport_NodeViewModel();
        public ImageImport_NodeView()
        {
            InitializeComponent();
            
        }

        private void ImageImport_NodeView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is Node_NodeViewModel vm)
            {
                ImageImport_NodeViewModel = (ImageImport_NodeViewModel)vm;
                ImageImport_NodeViewModel.NodeModel.OutputPorts.Add(new PortModel() { Type = PortType.Output, Owner = ImageImport_NodeViewModel.NodeModel });
                OutputPort.DataContext = ImageImport_NodeViewModel.NodeModel.OutputPorts[0];
            }
        }
    }
}
