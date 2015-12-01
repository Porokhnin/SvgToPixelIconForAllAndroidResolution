using System;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Applications;
using System.Windows.Forms;
using SvgToPixelIcon.Configuration;
using SvgToPixelIcon.Configuration.Models;
using SvgToPixelIcon.Model.ViewModels.Tree;
using SvgToPixelIcon.Model.Views;
using SvgToPixelIcon.Painter;

namespace SvgToPixelIcon.Model.ViewModels
{
    [Export]
    public class MainViewModel: ViewModel<IMainView>, IDisposable
    {
        #region Поля        
        
        private readonly DirectoryTreeViewModel _directoryTreeViewModel;
        private readonly ConfigurationTreeViewModel _configurationTreeViewModel;

        private bool _treeVisibility;
        private bool _configurationProgressVisibility;

        private string _svgDirectoryPath;
        private string _outputDirectoryPath;
        private string _outputText;

        private DirectoriesConfigInfo _configuration;

        #endregion Поля

        #region Свойства

        public DirectoryTreeViewModel DirectoryTreeViewModel
        {
            get { return _directoryTreeViewModel; }
        }

        public Boolean TreeVisibility
        {
            get { return _treeVisibility; }
            set { SetProperty(ref _treeVisibility, value); }
        }

        public ConfigurationTreeViewModel ConfigurationTreeViewModel
        {
            get { return _configurationTreeViewModel; }
        }

        public Boolean ConfigurationProgressVisibility
        {
            get { return _configurationProgressVisibility; }
            set { SetProperty(ref _configurationProgressVisibility, value); }
        }

        public String SvgDirectoryPath
        {
            get { return _svgDirectoryPath; }
            set { SetProperty(ref _svgDirectoryPath, value); }
        }

        public String OutputDirectoryPath
        {
            get { return _outputDirectoryPath; }
            set { SetProperty(ref _outputDirectoryPath, value); }
        }

        public String OutputText
        {
            get { return _outputText; }
            set { SetProperty(ref _outputText, value); }
        }
        public int OutputTextCaretIndex { get; set; }

        #endregion Свойства

        #region Команды        
        /// <summary>
        /// Выход
        /// </summary>
        public DelegateCommand ExitCommand { get; private set; }
        /// <summary>
        /// Открыть svg директорию
        /// </summary>
        public DelegateCommand SelectSvgDirectoryCommand { get; private set; }
        /// <summary>
        /// Открыть svg директорию
        /// </summary>
        public DelegateCommand SelectOutputDirectoryCommand { get; private set; }

        public DelegateCommand PaintSvgToPngCommand { get; set; }
        #endregion Команды

        [ImportingConstructor]
        public MainViewModel(IMainView view, 
                             ExportFactory<DirectoryTreeViewModel> directoryTreeViewModel,
                             ExportFactory<ConfigurationTreeViewModel> configurationTreeViewModel)
            : base(view)
        {
            ExitCommand = new DelegateCommand(Close);
            SelectSvgDirectoryCommand = new DelegateCommand(SelectSvgDirectory);
            SelectOutputDirectoryCommand = new DelegateCommand(SelectOutputDirectory);
            PaintSvgToPngCommand = new DelegateCommand(PaintSvgToPng);

            _directoryTreeViewModel = directoryTreeViewModel.CreateExport().Value;
            _configurationTreeViewModel = configurationTreeViewModel.CreateExport().Value;
        }

        #region Методы
        public void Initialize()
        {            

        }

        public void Show()
        {
            ViewCore.Show();
        }

        public void Close()
        {
            ViewCore.Close();
        }

        public void Dispose()
        {
        }

        private async void SelectSvgDirectory()
        {
            SvgDirectoryPath = String.Empty;
            
            var folderBrowserDialog = new FolderBrowserDialog{Description = "Выберите папку с SVG"};
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SvgDirectoryPath = folderBrowserDialog.SelectedPath;
                TreeVisibility = true;

                _directoryTreeViewModel.UpdateConfiguration(SvgDirectoryPath);

                ConfigurationProgressVisibility = true;
                _configuration = await ReadConfigutaion(SvgDirectoryPath);
                _configurationTreeViewModel.UpdateConfiguration(_configuration);
                ConfigurationProgressVisibility = false;
            }
        }

        private Task<DirectoriesConfigInfo> ReadConfigutaion(string svgDirectoryPath)
        {
            ConfigutaionReader reader = new ConfigutaionReader(svgDirectoryPath);
            return Task.Factory.StartNew(() => reader.Read());
        }

        private void SelectOutputDirectory()
        {
            OutputDirectoryPath = String.Empty;
            OutputText = String.Empty;

            var folderBrowserDialog = new FolderBrowserDialog(){Description = "Выберите выходную папку"};
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                OutputDirectoryPath = folderBrowserDialog.SelectedPath;
            }
        }

        private async void PaintSvgToPng ()
        {
            if (!String.IsNullOrEmpty(OutputDirectoryPath) && _configuration != null)
            {
                await PaintConfiguration();
            }
        }

        private Task PaintConfiguration()
        {
            OutputText = String.Empty;
            var outputBuilder = new StringBuilder();
            var svgPainter = new SvgPainter(_configuration);
            svgPainter.FilePainted += (sender, args) =>
            {
                if (args.Exception != null)
                {
                    outputBuilder.AppendLine(args.Exception.ToString());
                    OutputText = outputBuilder.ToString();
                }
                if (args.FileConfigInfo != null)
                {
                    outputBuilder.AppendLine(String.Format("{0} {1}-{2}", args.FileName, args.DensityKey, args.DocumentSize));
                    OutputText = outputBuilder.ToString();
                }
                OutputTextCaretIndex = OutputText.Length;
            };
            return Task.Factory.StartNew(() => svgPainter.StartPaint(OutputDirectoryPath));
        }

        #endregion Методы
    }
}
