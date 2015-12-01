using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using SvgToPixelIcon.Configuration.Models;
using SvgToPixelIcon.Model.Models.Tree;
using SvgToPixelIcon.Model.Views.Tree;

namespace SvgToPixelIcon.Model.ViewModels.Tree
{
    [Export]
    public class ConfigurationTreeViewModel: ViewModel<IConfigurationTreeView>
    {           
        /// <summary>
        /// Корневые объекты дерева
        /// </summary>
        public ObservableCollection<TreeItemModel> RootTreeItems { get; set; }

        [ImportingConstructor]
        public ConfigurationTreeViewModel(IConfigurationTreeView view) : base(view)
        {
            RootTreeItems = new ObservableCollection<TreeItemModel>();
        }

        /// <summary>
        /// Обновить конфигурацию
        /// </summary>
        /// <param name="configuration"></param>
        public void UpdateConfiguration(DirectoriesConfigInfo configuration)
        {
            RootTreeItems.Clear();
            RootTreeItems.Add(CreateDirectoryNode(configuration));
        }

        private TreeItemModel CreateDirectoryNode(DirectoriesConfigInfo directoryConfigInfo)
        {
            var directoryConfigNode = new TreeDirectory(directoryConfigInfo.DirectoryInfo, directoryConfigInfo.ConfigInfo.ToString());
            foreach (var directory in directoryConfigInfo.GetDirectories())
                directoryConfigNode.Nodes.Add(CreateDirectoryNode(directory));

            foreach (var file in directoryConfigInfo.GetFiles())
                directoryConfigNode.Nodes.Add(new TreeFile(file.FileInfo, file.ConfigInfo.ToString()));

            return directoryConfigNode;
        }
    }
}
