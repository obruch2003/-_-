using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ООП_Курсовая
{
    public partial class DataAdmin : Window
    {
        string strSQL;
        SqlConnectionStringBuilder bldr;
        SqlConnection cn;
        AdminMethodi adminMethodi;
        Methodi methodi;
        delegate void Vivodi(SqlConnection cn, SqlConnectionStringBuilder bldr, string strSQL, DataGrid CarsDataGrid);
        event Vivodi eventVivodi;
        string Country = "Allcars";
        public DataAdmin(bool AdminCheck)
        {
            InitializeComponent();
            bldr = new SqlConnectionStringBuilder();
            bldr.DataSource = @"DESKTOP-0610CVP\MSSQLSERVER01";
            bldr.IntegratedSecurity = true;
            bldr.InitialCatalog = "CarShop";
            methodi = new Methodi();
            eventVivodi += methodi.Vivod;

            if (AdminCheck == true)
            {
                adminMethodi = new AdminMethodi();
                AddButton.Click += AddButton_Click;
                AddButton.Opacity = 100;
                DataGridTextColumn priceColumn = CarsDataGrid.Columns[4] as DataGridTextColumn;
                priceColumn.IsReadOnly = false;
                Operations.Visibility = Visibility.Visible;
            }
        }
        private void ButtonEmpty_Click(object sender, RoutedEventArgs e)//Пустой клик
        {

        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)//Перемещение окна
        {
            methodi.Peremeshenie(e, this);
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)//Увеличение окна
        {
            methodi.Yvelichenie(e, this);
        }
        private void MyDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)//Показ дополнительной информации Cars
        {
            methodi.DopInfaInDataGrid(CarsDataGrid);
        }
        private void MyDataGrid_MouseLeftButtonUp2(object sender, MouseButtonEventArgs e)//Показ дополнительной информации CarsService
        {
            methodi.DopInfaInDataGrid(CarsServiceDataGrid);
        }
        private void DopInfaDliaBrand(object sender, SelectedCellsChangedEventArgs e)//Показ всей информации по автосервису Id и Model
        {
            methodi.DopInfaDliaBrand(CarsServiceDataGrid);
        }
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)//Фильтер
        {
            methodi.Filter(CarsDataGrid, FilterTextBox);
        }
        private void FilterTextBox_GotFocus(object sender, RoutedEventArgs e)//Фильтер
        {
            TextBlockFilter.Text = "";
        }
        private void FilterTextBox_LostFocus(object sender, RoutedEventArgs e)//Фильтер
        {
            methodi.FilterText(FilterTextBox, TextBlockFilter);
        }
        private void CarsDataGrid_Loaded(object sender, RoutedEventArgs e)//Вывод данных
        {
            eventVivodi(cn, bldr, strSQL, CarsDataGrid);
            methodi.Vivod2(cn, bldr, strSQL, CarsServiceDataGrid);
            methodi.FastChoice(cn, bldr, strSQL, CarsDataGrid, Allcars, Allcars, FilterTextBox,ref Country);//Лютейший костыль с кликом по Allcars при создании by serpak
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)//Выход
        {
            Application.Current.Shutdown();
        }
        private void MachineRangeButton_Click(object sender, RoutedEventArgs e)//Перейти к авто
        {
            methodi.MachineRangeButton(CarsDataGrid, CarsServiceDataGrid, FastChoice, FilterGrid, AutoText);
        }
        private void CarServiceButton_Click(object sender, RoutedEventArgs e)//Перейти к автосервисам
        {
            methodi.Vivod2(cn, bldr, strSQL, CarsServiceDataGrid);
            methodi.CarServiceButton(CarsDataGrid, CarsServiceDataGrid, FastChoice, FilterGrid, AutoText);
        }
        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)//Перейти в окно авторизации
        {
            methodi.AuthorizationButton(this);
        }
        bool isMenuOpen = true;
        private void MenuButton_Click(object sender, RoutedEventArgs e)//Открыть и закрыть меню
        {
            methodi.SkritieIOtkritieMenu(MenuGrid, OsnovaGrid);
        }
        private void gridRemoveButton_Click(object sender, RoutedEventArgs e)//Удаление записи
        {
            adminMethodi.Remove(cn, bldr, CarsDataGrid);
            eventVivodi(cn, bldr, strSQL, CarsDataGrid);
            if(Country.Equals("Allcars"))
                methodi.FastChoice(cn, bldr, strSQL, CarsDataGrid, Allcars, Allcars, FilterTextBox, ref Country);
            if (Country.Equals("Germany"))
                methodi.FastChoice(cn, bldr, strSQL, CarsDataGrid, Allcars, Germany, FilterTextBox, ref Country);
            if (Country.Equals("Japan"))
                methodi.FastChoice(cn, bldr, strSQL, CarsDataGrid, Allcars, Japan, FilterTextBox, ref Country);
            if (Country.Equals("France"))
                methodi.FastChoice(cn, bldr, strSQL, CarsDataGrid, Allcars, France, FilterTextBox, ref Country);
            if (Country.Equals("Sweden"))
                methodi.FastChoice(cn, bldr, strSQL, CarsDataGrid, Allcars, Sweden, FilterTextBox,ref Country);
        }
        private void gridEditButton_Click(object sender, RoutedEventArgs e)//Изменение записи
        {
            adminMethodi.Edit(cn, bldr, CarsDataGrid);
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)//Добавление записи
        {
            adminMethodi.AddButton(this);
        }
        private void FastСhoiceAuto(object sender, RoutedEventArgs e)
        {           
            methodi.FastChoice(cn, bldr, strSQL, CarsDataGrid, Allcars, sender, FilterTextBox,ref Country);
        }
        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            methodi.Report(CarsDataGrid); 
        }
    }
}

