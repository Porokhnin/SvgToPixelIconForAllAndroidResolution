using System.ComponentModel.Composition;
using SvgToPixelIcon.Model.ViewModels;

namespace SvgToPixelIcon.Model.Controllers
{
    /// <summary>
    /// Контроллер приложения
    /// </summary>
    [Export(typeof(IApplicationController))]
    internal class ApplicationController: IApplicationController
    {
        private readonly ExportFactory<MainViewModel> _mainViewModelExportFactory;
        private ExportLifetimeContext<MainViewModel> _mainViewModelExport;
        private MainViewModel _mainViewModel;

        [ImportingConstructor]
        public ApplicationController(ExportFactory<MainViewModel> mainViewModelExportFactory)
        {
            _mainViewModelExportFactory = mainViewModelExportFactory;
        }

        /// <summary>
        /// Инициализировать
        /// </summary>
        public void Initialize()
        {
            InitializeMainViewModel();
        }

        /// <summary>
        /// Запустить
        /// </summary>
        public void Run()
        {
            RunMainViewModel();
        }

        /// <summary>
        /// Завершить
        /// </summary>
        public void Shutdown()
        {
            CloseMainViewModel();
        }
   
        /// <summary>
        /// Инициализировать главное окно
        /// </summary>
        private void InitializeMainViewModel()
        {
            _mainViewModelExport = _mainViewModelExportFactory.CreateExport();
            _mainViewModel = _mainViewModelExport.Value;
            _mainViewModel.Initialize();
        }

        /// <summary>
        /// Открыть главное окно
        /// </summary>
        private void RunMainViewModel()
        {
            _mainViewModel.Show();
        }

        /// <summary>
        /// Закрыть главное окно
        /// </summary>
        private void CloseMainViewModel()
        {
            if (_mainViewModel != null)
            {
                _mainViewModel.Dispose();
                _mainViewModel.Close();
                _mainViewModelExport.Dispose();
                _mainViewModel = null;
                _mainViewModelExport = null;
            }
        }
    }
}
