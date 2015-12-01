using System;

namespace SvgToPixelIcon.Configuration.XmlFileProcessor
{
    /// <summary>
    /// Ошибка из xml
    /// </summary>
    public class XmlConfigError
    {
        /// <summary>
        /// Строка
        /// </summary>
        public String Line { get; private set; }

        /// <summary>
        /// Ошибка
        /// </summary>
        public String Error { get; private set; }

        public XmlConfigError(String line, String error)
        {
            Line = line;
            Error = error;
        }
    }
}
