using System.Collections.Generic;

namespace SvgToPixelIcon.Configuration.XmlFileProcessor
{
    /// <summary>
    /// Результат чтения xml конфига
    /// </summary>
    public class XmlConfigReadingResult
    {
        /// <summary>
        /// Конфигурционная информация о директории из xml
        /// </summary>
        public XmlConfigDirectoryInfo ConfigDirectoryInfo { get; set; }

        /// <summary>
        /// Коллекция конфигурционной информации о файле из xml
        /// </summary>
        public List<XmlConfigFileInfo> ConfigFileInfoCollection { get; private set; }

        /// <summary>
        /// Ошибки
        /// </summary>
        public List<XmlConfigError> ConfigErrorCollection { get; private set; }

        public XmlConfigReadingResult()
        {
            ConfigFileInfoCollection = new List<XmlConfigFileInfo>();
            ConfigErrorCollection = new List<XmlConfigError>();
        }
    }
}
