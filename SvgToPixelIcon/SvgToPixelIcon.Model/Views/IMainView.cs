using System.Waf.Applications;

namespace SvgToPixelIcon.Model.Views
{
    public interface IMainView: IView
    {
        void Show();

        void Close();
    }
}
