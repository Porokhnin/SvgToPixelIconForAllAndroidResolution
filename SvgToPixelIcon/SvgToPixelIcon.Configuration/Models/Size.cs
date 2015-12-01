namespace SvgToPixelIcon.Configuration.Models
{
    /// <summary>
    /// Размер
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// Ширина
        /// </summary>
        public float Width { get; private set; }

        /// <summary>
        /// Высота
        /// </summary>
        public float Height { get; private set; }

        /// <summary>
        /// Размер
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        public Size(float width, float height): this()
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}", Width, Height);
        }
    }
}