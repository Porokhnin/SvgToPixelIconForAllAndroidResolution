namespace SvgToPixelIcon.Model.Controllers
{
    /// <summary>
    /// Контроллер приложения
    /// </summary>
    public interface IApplicationController
    {
        /// <summary>
        /// Инициализировать
        /// </summary>
        void Initialize();
        /// <summary>
        /// Запустить
        /// </summary>
        void Run();
        /// <summary>
        /// Завершить
        /// </summary>
        void Shutdown();
    }
}
