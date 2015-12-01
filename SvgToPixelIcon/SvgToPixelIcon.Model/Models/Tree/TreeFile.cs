using System;
using System.IO;

namespace SvgToPixelIcon.Model.Models.Tree
{
    /// <summary>
    /// Базовый класс для отображения файла в дереве
    /// </summary>
    public class TreeFile : TreeItemModel
    {
        /// <summary>
        /// Системная информация
        /// </summary>
        public FileInfo FileInfo { get {return FileSystemInfo as FileInfo;} }

        public TreeFile(FileInfo fileInfo, String configInfo = null) : base(fileInfo, configInfo)
        {
        }      
    }
}
