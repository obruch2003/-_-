using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ООП_Курсовая
{
    public partial class AddWindow : Window
    {
        string strSQL;
        SqlConnectionStringBuilder bldr;
        SqlConnection cn;
        AdminMethodi adminMethodi;
        public AddWindow()
        {
            InitializeComponent();
            bldr = new SqlConnectionStringBuilder();
            bldr.DataSource = @"DESKTOP-0610CVP\MSSQLSERVER01";
            bldr.IntegratedSecurity = true;
            bldr.InitialCatalog = "CarShop";
            adminMethodi = new AdminMethodi();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            adminMethodi.Peremeshenie(e, this);
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            adminMethodi.Dobavlenie(cn, bldr, strSQL, ComboBox1, ComboBox2, AddTextBox1, AddTextBox2, ComboBox3, ComboBox4, ComboBox5, ComboBox6, ComboBox7, AddTextBox3);
            adminMethodi.AddWindowButtons(this);
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            adminMethodi.AddWindowButtons(this);
        }

        private void Combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            adminMethodi.ComboBox1Details(ComboBox1, ComboBox2);
        }

        private void Combobox5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            adminMethodi.ComboBox5Details(ComboBox5,ComboBox6);
        }     
    }
}
