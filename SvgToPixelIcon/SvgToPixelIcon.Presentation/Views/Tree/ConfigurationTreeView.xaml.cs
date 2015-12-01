using System.ComponentModel.Composition;
using SvgToPixelIcon.Model.Views.Tree;

namespace SvgToPixelIcon.Presentation.Views.Tree
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    [Export(typeof(IConfigurationTreeView))]
    public partial class ConfigurationTreeView : IConfigurationTreeView
    {
        public ConfigurationTreeView()
        {
            InitializeComponent();
        }
    }
}
