using System.ComponentModel.Composition;
using SvgToPixelIcon.Model.Views;

namespace SvgToPixelIcon.Presentation.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(IMainView))]
    public partial class MainWindow : IMainView
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
