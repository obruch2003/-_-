using System.Windows;
using System.Windows.Input;

namespace ООП_Курсовая
{
    public partial class AuthorizationMenu : Window
    {
        Methodi methodi;
        public AuthorizationMenu()
        {
            InitializeComponent();
            methodi = new Methodi();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            methodi.Peremeshenie(e, this);
        }
        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            methodi.AdminOrUserButton(this,1);
        }
        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            methodi.AdminOrUserButton(this,2);
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
