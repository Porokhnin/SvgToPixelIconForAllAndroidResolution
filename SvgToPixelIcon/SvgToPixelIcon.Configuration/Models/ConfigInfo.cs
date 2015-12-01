using System;
using System.Text;
using System.Windows.Media;

namespace SvgToPixelIcon.Configuration.Models
{
    /// <summary>
    /// Конфигурционная информация
    /// </summary>
    public class ConfigInfo
    {        
        /// <summary>
        /// Цвет который нужно заменить
        /// </summary>
        public Color? SourceColor { get; set; }

        /// <summary>
        /// Цвет
        /// </summary>
        public Color? Color { get; set; }

        /// <summary>
        /// Размер
        /// </summary>
        public Size? Size { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public SizeUnitType SizeUnitType { get; set; }

        /// <summary>
        /// Имя директорияи с разрешением
        /// </summary>
        public String Dimension { get; set; }

        public ConfigInfo()
        {
            SizeUnitType = SizeUnitType.Dp;
        }

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
