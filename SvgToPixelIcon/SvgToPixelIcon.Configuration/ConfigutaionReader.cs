using System;
using System.IO;
using System.Linq;
using SvgToPixelIcon.Configuration.Models;
using SvgToPixelIcon.Configuration.XmlFileProcessor;

namespace SvgToPixelIcon.Configuration
{
    /// <summary>
    /// Класс чтения конфигурации
    /// </summary>
    public class ConfigutaionReader
    {
        /// <summary>
        /// Путь к пакпке с svg файлами
        /// </summary>
        private readonly String _svgDirectoryPath;

        /// <summary>
        /// Конструктор чтения конфигурации
        /// </summary>
        /// <param name="svgDirectoryPath">Путь к пакпке с svg файлами</param>
        public ConfigutaionReader(String svgDirectoryPath)
        {
            _svgDirectoryPath = svgDirectoryPath;
        }

        /// <summary>
        /// Прочитать конфигурацию
        /// </summary>
        /// <returns>Конфигурация</returns>
        public DirectoriesConfigInfo Read()
        {
            return CreateDirectoryConfigInfo(new DirectoryInfo(_svgDirectoryPath));
        }

        /// <summary>
        /// Создать конфигурацию для директории
        /// </summary>
        /// <param name="directoryInfo">DirectoryInfo</param>
        /// <returns>Конфигурация</returns>
        private DirectoriesConfigInfo CreateDirectoryConfigInfo(DirectoryInfo directoryInfo)
        {
            DirectoriesConfigInfo directoryConfigInfo = new DirectoriesConfigInfo(directoryInfo);
            XmlConfigReadingResult configReadingResult = null;

            var configFile = FindConfigFile(directoryInfo);
            if (configFile != null)
            {
                configReadingResult = ReadXmlConfigurationFile(configFile.FullName);

                directoryConfigInfo.ConfigInfo = configReadingResult != null ? configReadingResult.ConfigDirectoryInfo : new ConfigInfo();
            }

            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryConfigInfo.Nodes.Add(CreateDirectoryConfigInfo(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                if (Path.GetExtension(file.Name).Contains("svg"))
                {
                    var fileConfigInfo = new FilesConfigInfo(file);
                    if (configReadingResult != null)
                    {
                        var configFileInfo = configReadingResult.ConfigFileInfoCollection.FirstOrDefault(f => f.FileName == Path.GetFileNameWithoutExtension(file.Name));
                        if (configFileInfo != null)
                        {
                            fileConfigInfo.ConfigInfo = configFileInfo;
                        }
                        else
                        {
                            fileConfigInfo.ConfigInfo = configReadingResult.ConfigDirectoryInfo;
                        }

                    }
                    directoryConfigInfo.Nodes.Add(fileConfigInfo);
                }
            }
            return directoryConfigInfo;
        }

        /// <summary>
        /// Найти конфиг файл
        /// </summary>
        /// <param name="directoryInfo">DirectoryInfo</param>
        /// <returns>FileInfo конфиг файла</returns>
        private FileInfo FindConfigFile(DirectoryInfo directoryInfo)
        {
            return directoryInfo.GetFiles().FirstOrDefault(f => f.Name.StartsWith("config") && Path.GetExtension(f.Name).Contains("xml"));
        }
        /// <summary>
        /// Прочитать конфиг файл
        /// </summary>
        /// <param name="configurationFilePath">Путь к конфиг файлу</param>
        /// <returns>Результат чтения конфиг файла</returns>
        private XmlConfigReadingResult ReadXmlConfigurationFile(String configurationFilePath)
        {
            var configurationProcessor = new XmlConfigProcessor(configurationFilePath);
            return configurationProcessor.ReadConfigurationFile();
        }
    }
}
