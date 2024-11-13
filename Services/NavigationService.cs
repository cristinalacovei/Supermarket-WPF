using System.Windows;
using System.Windows.Controls;

namespace WPF_LoginForm.Services
{
    public static class NavigationService
    {
        private static Frame _mainFrame;

        public static void Initialize(Frame frame)
        {
            _mainFrame = frame;
        }

        public static void Navigate(Page page)
        {
            _mainFrame?.Navigate(page);
        }
    }
}
