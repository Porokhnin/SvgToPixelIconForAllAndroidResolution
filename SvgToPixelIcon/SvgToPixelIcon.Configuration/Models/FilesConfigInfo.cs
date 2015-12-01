using System.IO;

namespace SvgToPixelIcon.Configuration.Models
{
    /// <summary>
    /// Конфигурационая информация для файла
    /// </summary>
    public class FilesConfigInfo : FileSystemConfigInfo
    {
        /// <summary>
        /// Системная информация о файле
        /// </summary>
        public FileInfo FileInfo { get { return FileSystemInfo as FileInfo; } }

        /// <summary>
        /// Конфигурационая информация для файла
        /// </summary>
        /// <param name="fileSystemInfo">Системная информация</param>
        public FilesConfigInfo(FileSystemInfo fileSystemInfo) : base(fileSystemInfo)
        {

        }
    }
}
