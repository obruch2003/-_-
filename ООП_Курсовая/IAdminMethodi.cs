using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows;

namespace ООП_Курсовая
{
    internal interface IAdminMethodi
    {
        void Edit(SqlConnection cn, SqlConnectionStringBuilder bldr, DataGrid CarsDataGrid);//Для DataAdmin
        void Remove(SqlConnection cn, SqlConnectionStringBuilder bldr, DataGrid CarsDataGrid);//Для DataAdmin
        void Dobavlenie(SqlConnection cn, SqlConnectionStringBuilder bldr, string strSQL, ComboBox ComboBox1, ComboBox ComboBox2, TextBox AddTextBox1, TextBox AddTextBox2, ComboBox ComboBox3, ComboBox ComboBox4, ComboBox ComboBox5, ComboBox ComboBox6, ComboBox ComboBox7, TextBox AddTextBox3);//Для AddWindow
        void AddWindowButtons(Window addWindow);//Для AddWindow
        void CancelInPassword(RoutedEventArgs e, Window window);//Для PasswordWindow
        void AceptInPassword(RoutedEventArgs e, Window window, TextBox passwordTextBox);//Для PasswordWindow
    }
}
