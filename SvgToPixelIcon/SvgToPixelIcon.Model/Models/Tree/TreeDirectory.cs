using System;
using System.Collections.ObjectModel;
using System.IO;

namespace SvgToPixelIcon.Model.Models.Tree
{
    /// <summary>
    /// Базовый класс для отображения файла в дереве
    /// </summary>
    public class TreeDirectory : TreeItemModel
    {
        /// <summary>
        /// Системная информация
        /// </summary>
        public DirectoryInfo DirectoryInfo { get { return FileSystemInfo as DirectoryInfo; } }

        /// <summary>
        /// Коллекция дочерних элементов
        /// </summary>
        public ObservableCollection<TreeItemModel> Nodes { get; set; }

        public TreeDirectory(DirectoryInfo directoryInfo, String configInfo = null) : base(directoryInfo, configInfo)
        {
            Nodes = new ObservableCollection<TreeItemModel>();
        }
    }
}
