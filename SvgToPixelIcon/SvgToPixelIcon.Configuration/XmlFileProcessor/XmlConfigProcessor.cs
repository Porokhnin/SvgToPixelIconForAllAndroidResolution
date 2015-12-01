using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Xml.Linq;
using SvgToPixelIcon.Configuration.Models;
using SvgToPixelIcon.Configuration.XmlFileProcessor.Extension;

namespace SvgToPixelIcon.Configuration.XmlFileProcessor
{
    /// <summary>
    /// Класс для разбора конфиг файла
    /// </summary>
    public class XmlConfigProcessor
    {
        /// <summary>
        /// Путь к конфиг файлу
        /// </summary>
        private readonly String _configFilePath;

        /// <summary>
        /// Класс для разбора конфиг файла
        /// </summary>
        /// <param name="configFilePath">Путь к конфиг файлу</param>
        public XmlConfigProcessor(String configFilePath)
        {
            _configFilePath = configFilePath;
        }
        /// <summary>
        /// Прочитать конфиг файл
        /// </summary>
        /// <returns>Результат чтения</returns>
        public XmlConfigReadingResult ReadConfigurationFile()
        {
            return ReadConfigurationFile(XDocument.Load(_configFilePath));
        }
        /// <summary>
        /// Прочитать конфиг файл
        /// </summary>
        /// <param name="document">XML документ</param>
        /// <returns>Результат чтения</returns>
        private XmlConfigReadingResult ReadConfigurationFile(XDocument document)
        {
            var readingResult = new XmlConfigReadingResult();

            IEnumerable<XElement> directoryElements = document.Descendants("directory").ToList();
            if (directoryElements.Any())
            {
                String directoryDimensionStr = directoryElements.First().AttributeSafe("dimension");
                String directorySourceColorStr = directoryElements.First().AttributeSafe("sourceColor");
                String directoryColorStr = directoryElements.First().AttributeSafe("color");
                String directoryWidthStr = directoryElements.First().AttributeSafe("width");
                String directoryHightStr = directoryElements.First().AttributeSafe("height");
                String directoryUnitTypeStr = directoryElements.First().AttributeSafe("unitType");

                readingResult.ConfigDirectoryInfo = new XmlConfigDirectoryInfo()
                {
                    Dimension = directoryDimensionStr,
                    SourceColor = ParseColorStr(directorySourceColorStr),
                    Color = ParseColorStr(directoryColorStr),
                    Size = ParseSizeStr(directoryWidthStr, directoryHightStr),
                    SizeUnitType = ParseSizeUnitTypeStr(directoryUnitTypeStr)
                };

                IEnumerable<XElement> fileConfigElements = directoryElements.Descendants("file").ToList();
                foreach (var fileConfigElement in fileConfigElements)
                {
                    String fileNameStr = fileConfigElement.AttributeSafe("name");
                    String fileDimensionStr = fileConfigElement.AttributeSafe("dimension");
                    String fileSourceColorStr = fileConfigElement.AttributeSafe("sourceColor");
                    String fileColorStr = fileConfigElement.AttributeSafe("color");
                    String fileWidthStr = fileConfigElement.AttributeSafe("width");
                    String fileHightStr = fileConfigElement.AttributeSafe("height");
                    String fileUnitTypeStr = fileConfigElement.AttributeSafe("unitType");

                    if (!String.IsNullOrEmpty(fileNameStr))
                    {
                        readingResult.ConfigFileInfoCollection.Add(new XmlConfigFileInfo()
                        {
                            FileName = fileNameStr,
                            Dimension = fileDimensionStr,
                            SourceColor = ParseColorStr(fileSourceColorStr),
                            Color = ParseColorStr(fileColorStr),
                            Size = ParseSizeStr(fileWidthStr, fileHightStr),
                            SizeUnitType = ParseSizeUnitTypeStr(fileUnitTypeStr)
                        });
                    }
                    else
                    {
                        readingResult.ConfigErrorCollection.Add(new XmlConfigError(fileConfigElement.ToString(), "Ошибка разбора полей для файла"));
                    }
                }
            }
            else
            {
                readingResult.ConfigErrorCollection.Add(new XmlConfigError(directoryElements.ToString(), "Ошибка разбора полей для директории"));
            }

            return readingResult;
        }

        /// <summary>
        /// Распарсить цвет
        /// </summary>
        /// <param name="сolorStr">Строковое представление цвета</param>
        /// <returns>Цвет</returns>
        private Color? ParseColorStr(string сolorStr)
        {
            Color? resultColor = null;

            if (!String.IsNullOrEmpty(сolorStr))
            {
                var convertFromString = ColorConverter.ConvertFromString(сolorStr);
                if (convertFromString != null)
                {
                    resultColor = (Color) convertFromString;
                }
            }
            return resultColor;
        }

        /// <summary>
        /// Распарсить размер
        /// </summary>
        /// <param name="widthStr">Строковое представление ширины</param>
        /// <param name="heightStr">Строковое представление высоты</param>
        /// <returns>Размер</returns>
        private Size? ParseSizeStr(string widthStr, string heightStr)
        {
            Size? resultSize = null;

            int width;
            int height;

            if (int.TryParse(widthStr, out width) && int.TryParse(heightStr, out height))
            {
                resultSize = new Size(width, height);
            }

            return resultSize;
        }        

        /// <summary>
        /// Распарсить единицы измерения размера
        /// </summary>
        /// <param name="unitTypeStr">Строковое представление единиц измерения размера</param>
        /// <returns>Единицы измерения размера</returns>
        private SizeUnitType ParseSizeUnitTypeStr(string unitTypeStr)
        {
            SizeUnitType result;
            Enum.TryParse(unitTypeStr, true, out result);
            return result;
        }
    }
}
