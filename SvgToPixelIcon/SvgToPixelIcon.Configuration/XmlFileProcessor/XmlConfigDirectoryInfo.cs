using System;
using System.Text;
using SvgToPixelIcon.Configuration.Models;

namespace SvgToPixelIcon.Configuration.XmlFileProcessor
{
    /// <summary>
    /// Конфигурционная информация о директории из xml
    /// </summary>
    public class XmlConfigDirectoryInfo : ConfigInfo
    {
        public override string ToString()
        {
            var builder = new StringBuilder();

            if (Color.HasValue)
                builder.Append(String.Format("{0} ", Color.Value));

            if (Size.HasValue)
                builder.Append(Size.Value);

            return builder.ToString();
        }
    }
}
