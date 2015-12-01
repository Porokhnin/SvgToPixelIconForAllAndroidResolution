using System.IO;

namespace SvgToPixelIcon.Configuration.Models
{
    /// <summary>
    /// Базовый класс конфигурационой информации
    /// </summary>
    public abstract class FileSystemConfigInfo
    {
        /// <summary>
        /// Базовый класс для системной информации
        /// </summary>
        public FileSystemInfo FileSystemInfo { get; private set; }

        /// <summary>
        /// Конфигурционная информация
        /// </summary>
        public ConfigInfo ConfigInfo { get; set; }

        /// <summary>
        /// Базовый класс конфигурационой информации
        /// </summary>
        /// <param name="fileSystemInfo">Системная информация</param>
        protected FileSystemConfigInfo(FileSystemInfo fileSystemInfo)
        {
            FileSystemInfo = fileSystemInfo;
        }
    }
}
