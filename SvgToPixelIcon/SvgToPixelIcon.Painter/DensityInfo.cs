namespace SvgToPixelIcon.Painter
{
    /// <summary>
    /// Информация о плоности экрана
    /// </summary>
    public class DensityInfo
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Плотность
        /// </summary>
        public float Density { get; private set; }
        
        /// <summary>
        /// Информация о плоности экрана
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="density">Плотность</param>
        public DensityInfo(string key, float density)
        {
            Key = key;
            Density = density;
        }
    }
}
