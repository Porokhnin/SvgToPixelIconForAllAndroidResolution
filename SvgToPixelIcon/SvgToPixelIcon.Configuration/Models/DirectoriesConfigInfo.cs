using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SvgToPixelIcon.Configuration.Models
{
    /// <summary>
    /// Конфигурационая информация для директории
    /// </summary>
    public class DirectoriesConfigInfo : FileSystemConfigInfo
    {
        /// <summary>
        /// Системная информация о директории
        /// </summary>
        public DirectoryInfo DirectoryInfo { get { return FileSystemInfo as DirectoryInfo; } }

        /// <summary>
        /// Дочерние элементы
        /// </summary>
        public List<FileSystemConfigInfo> Nodes { get; private set; }

        /// <summary>
        /// Конфигурационая информация для директории
        /// </summary>
        /// <param name="fileSystemInfo">Системная информация</param>
        public DirectoriesConfigInfo(FileSystemInfo fileSystemInfo) : base(fileSystemInfo)
        {
            Nodes = new List<FileSystemConfigInfo>();
        }

        /// <summary>
        /// Получить директории
        /// </summary>
        /// <returns>Директории</returns>
        public IEnumerable<DirectoriesConfigInfo> GetDirectories()
        {
            return Nodes.Where(itemConfigInfo => itemConfigInfo.FileSystemInfo is DirectoryInfo).Cast<DirectoriesConfigInfo>().ToList();
        }

        /// <summary>
        /// Получить файлы
        /// </summary>
        /// <returns>Файлы</returns>
        public IEnumerable<FilesConfigInfo> GetFiles()
        {
            return Nodes.Where(itemConfigInfo => itemConfigInfo.FileSystemInfo is FileInfo).Cast<FilesConfigInfo>().ToList();
        }
    }
}
