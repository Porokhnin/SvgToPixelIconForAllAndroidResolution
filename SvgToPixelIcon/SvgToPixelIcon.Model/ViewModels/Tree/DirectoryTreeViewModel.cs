using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Waf.Applications;
using SvgToPixelIcon.Model.Models.Tree;
using SvgToPixelIcon.Model.Views.Tree;

namespace SvgToPixelIcon.Model.ViewModels.Tree
{
    [Export]
    public class DirectoryTreeViewModel: ViewModel<IDirectoryTreeView>
    {           
        /// <summary>
        /// Корневые объекты дерева
        /// </summary>
        public ObservableCollection<TreeItemModel> RootTreeItems { get; set; }

        [ImportingConstructor]
        public DirectoryTreeViewModel(IDirectoryTreeView view) : base(view)
        {
            RootTreeItems = new ObservableCollection<TreeItemModel>();
        }

        /// <summary>
        /// Обновить конфигурацию
        /// </summary>
        public void UpdateConfiguration(string path)
        {
            RootTreeItems.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            RootTreeItems.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private TreeItemModel CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeDirectory(directoryInfo);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));

            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Nodes.Add(new TreeFile(file));

            return directoryNode;
        }
    }
}
