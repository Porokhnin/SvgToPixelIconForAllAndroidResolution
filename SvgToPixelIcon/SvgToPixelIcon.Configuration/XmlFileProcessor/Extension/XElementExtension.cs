using System;
using System.Xml.Linq;

namespace SvgToPixelIcon.Configuration.XmlFileProcessor.Extension
{
    public static class XElementExtension
    {
        /// <summary>
        /// Считать значение атрибута или вернуть null в случае его отсутствия
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="name">Имя атрибута</param>
        /// <returns>Значение атрибута или null</returns>
        public static string AttributeSafe(this XElement element, String name)
        {
            var attribute = element.Attribute(name);
            if (attribute != null)
            {
                return attribute.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
