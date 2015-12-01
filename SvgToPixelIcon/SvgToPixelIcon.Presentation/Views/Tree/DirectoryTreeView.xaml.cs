using System.ComponentModel.Composition;
using SvgToPixelIcon.Model.Views.Tree;

namespace SvgToPixelIcon.Presentation.Views.Tree
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    [Export(typeof(IDirectoryTreeView))]
    public partial class DirectoryTreeView : IDirectoryTreeView
    {
        public DirectoryTreeView()
        {
            InitializeComponent();
        }
    }
}
