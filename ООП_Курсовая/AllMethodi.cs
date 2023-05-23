using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace ООП_Курсовая
{
    public abstract class AllMethodi
    {
        bool IsMaximized = false;
        public void Yvelichenie(MouseButtonEventArgs e, Window window)//Для многих
        {
            if (IsMaximized == false)
            {
                if (e.ClickCount == 2)
                {
                    window.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
            else
            {
                if (e.ClickCount == 2)
                {
                    window.WindowState = WindowState.Normal;
                    window.Width = 1080;
                    window.Height = 720;
                    IsMaximized = false;
                }
            }
        }
        public void Peremeshenie(MouseButtonEventArgs e, Window window)//Для многих
        {
            if (e.OriginalSource is Border)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    window.DragMove();
                }
            }
        }
        public abstract void AdminOrUserButton(Window window, int n);
    }
}
