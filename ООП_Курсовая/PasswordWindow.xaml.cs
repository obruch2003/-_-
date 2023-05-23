using System.Windows;
using System.Windows.Input;

namespace ООП_Курсовая
{
    public partial class PasswordWindow : Window
    {
        AdminMethodi adminMethodi;
        public PasswordWindow()
        {
            InitializeComponent();
            adminMethodi = new AdminMethodi();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            adminMethodi.Peremeshenie(e, this);
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            adminMethodi.CancelInPassword(e, this);
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            adminMethodi.AceptInPassword(e, this, passwordTextBox);
        }
    }
}
