using System;
using System.IO;

namespace SvgToPixelIcon.Model.Models.Tree
{
    /// <summary>
    /// Базовый класс для отображения объектов в дереве
    /// </summary>
    public abstract class TreeItemModel : System.Waf.Foundation.Model
    {                
        /// <summary>
        /// Системная информация
        /// </summary>
        private FileSystemInfo _fileSystemInfo;

        /// <summary>
        /// Имя
        /// </summary>
        private String _name;

        /// <summary>
        /// Конфигурационная информация
        /// </summary>
        private String _configInfo;

        /// <summary>
        /// Системная информация
        /// </summary>
        protected FileSystemInfo FileSystemInfo
        {
            get { return _fileSystemInfo; }
            set
            {
                _fileSystemInfo = value;

                if (_fileSystemInfo != null)
                {
                    Name = _fileSystemInfo.Name;
                }
            }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public String Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        /// <summary>
        /// Конфигурационная информация
        /// </summary>
        public String ConfigInfo
        {
            get { return _configInfo; }
            set { SetProperty(ref _configInfo, value); }
        }

        protected TreeItemModel(FileSystemInfo fileSystemInfo, String configInfo)
        {
            FileSystemInfo = fileSystemInfo;
            ConfigInfo = configInfo;
        }
    }
}
