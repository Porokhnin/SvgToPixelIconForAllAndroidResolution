using System;
using System.Text;
using SvgToPixelIcon.Configuration.Models;

namespace SvgToPixelIcon.Configuration.XmlFileProcessor
{
    /// <summary>
    /// Конфигурционная информация о файле из xml
    /// </summary>
    public class XmlConfigFileInfo : ConfigInfo
    {
        /// <summary>
        /// Имя файла в конфиге
        /// </summary>
        public String FileName { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(String.Format("{0} ", FileName));

            if (Color.HasValue)
                builder.Append(String.Format("{0} ", Color.Value));

            if (Size.HasValue)
                builder.Append(Size.Value);

            return builder.ToString();
        }
    }
}
