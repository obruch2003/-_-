using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using static ООП_Курсовая.GlobalClass;

namespace ООП_Курсовая
{
    public class Methodi : AllMethodi, IMethodi
    {
        public ObservableCollection<Cars> filtercars = new ObservableCollection<Cars>();
        public ObservableCollection<Cars> filtercarsCountry = new ObservableCollection<Cars>();
        DataGridRow previouslySelectedRow;
        Button selectedButton;
     
        public override void AdminOrUserButton(Window window, int n)//Для AuthorizationMenu
        {
            if (n == 1)
            {
                PasswordWindow passwordWindow = new PasswordWindow();
                //window.Close();
                window.Visibility = Visibility.Collapsed;
                passwordWindow.ShowDialog();
            }
            else if (n == 2)
            {
                DataAdmin dataUser = new DataAdmin(false);
                //window.Close();
                window.Visibility = Visibility.Collapsed;
                dataUser.ShowDialog();
            }
        }
        public void Vivod(SqlConnection cn, SqlConnectionStringBuilder bldr, string strSQL, DataGrid CarsDataGrid)//Для DataAdmin
        {
            strSQL = "SELECT * FROM Cars";
            using (cn = new SqlConnection(bldr.ConnectionString))
            {
                try
                {
                    cn.Open();
                    cars.Clear();
                    SqlDataAdapter da = new SqlDataAdapter(strSQL, cn);
                    DataTable t = new DataTable();
                    da.Fill(t);
                    foreach (DataRow row in t.Rows)
                    {
                        cars.Add(new Cars
                        (
                            (int)row["Id"],
                            (string)row["Brand"],
                            (string)row["Model"],
                            (int)row["Year"],
                            (int)row["Price"],
                            (string)row["Class"],
                            (string)row["TipKyzova"],
                            (string)row["TipDvigatelia"],
                            (int)row["Vdvigatelia"],
                        (string)row["KorobkaPeredach"],
                            (int)row["Probeg"],
                            "Images\\Cars\\" + (string)row["Foto"] + ".jpg"
                        ));
                    }
                    CarsDataGrid.ItemsSource = cars;
                    cn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void Vivod2(SqlConnection cn, SqlConnectionStringBuilder bldr, string strSQL, DataGrid CarsService)//Для DataAdmin
        {
            strSQL = "SELECT * FROM CarsService";
            using (cn = new SqlConnection(bldr.ConnectionString))
            {
                try
                {
                    cn.Open();
                    carsService.Clear();
                    SqlDataAdapter da = new SqlDataAdapter(strSQL, cn);
                    DataTable t = new DataTable();
                    da.Fill(t);
                    foreach (DataRow row in t.Rows)
                    {
                        carsService.Add(new CarsService
                        (
                            (int)row["Number"],
                            (string)row["Brand"],
                            (string)row["Country"],
                            "Images\\Countries\\" + (string)row["CountryImage"] + ".jpg",
                        (string)row["Carservice"],
                            "Images\\Offices\\" + (string)row["Office"] + ".jpg",
                             string.Empty
                        ));
                    }
                    CarsService.ItemsSource = carsService;
                    cn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void Filter(DataGrid CarsDataGrid, TextBox FilterTextBox)//Для DataAdmin
        {
            filtercars.Clear();
            for (int i = 0; i < filtercarsCountry.Count; i++)
            {
                if (filtercarsCountry[i].Brand.ToLower().Contains(FilterTextBox.Text.ToLower()) || filtercarsCountry[i].Model.ToLower().Contains(FilterTextBox.Text.ToLower()) || filtercarsCountry[i].Year.ToString().Contains(FilterTextBox.Text))
                {
                    filtercars.Add(new Cars(filtercarsCountry[i].Id, filtercarsCountry[i].Brand, filtercarsCountry[i].Model, filtercarsCountry[i].Year, filtercarsCountry[i].Price,
                    filtercarsCountry[i].Class, filtercarsCountry[i].TipKyzova, filtercarsCountry[i].TipDvigatelia, filtercarsCountry[i].Vdvigatelia, filtercarsCountry[i].KorobkaPeredach,
                    filtercarsCountry[i].Probeg, filtercarsCountry[i].Foto));
                }
            }
            CarsDataGrid.ItemsSource = filtercars;
        }
        public void DopInfaInDataGrid(DataGrid datagrid)//Для DataAdmin
        {
            var selectedRow = datagrid.ItemContainerGenerator.ContainerFromItem(datagrid.SelectedItem) as DataGridRow;

            if (selectedRow != null && selectedRow.IsSelected)
            {
                if (selectedRow == previouslySelectedRow)
                {
                    // Скрываем дополнительную информацию
                    selectedRow.DetailsVisibility = Visibility.Collapsed;
                    previouslySelectedRow = null;
                }
                else
                {
                    // Показываем дополнительную информацию для новой выбранной строки
                    selectedRow.DetailsVisibility = Visibility.Visible;
                    // Скрываем дополнительную информацию для предыдущей выбранной строки, если такая есть
                    if (previouslySelectedRow != null)
                    {
                        previouslySelectedRow.DetailsVisibility = Visibility.Collapsed;
                    }
                    // Сохраняем текущую выбранную строку как предыдущую
                    previouslySelectedRow = selectedRow;
                }
            }
        }
        public void SkritieIOtkritieMenu(Grid MenuGrid, Grid OsnovaGrid)//Для DataAdmin
        {
            if (MenuGrid.Visibility == Visibility.Visible)
            {
                MenuGrid.Visibility = Visibility.Collapsed;
                Grid.SetColumn(OsnovaGrid, 0);
                Grid.SetColumnSpan(OsnovaGrid, 2);
                OsnovaGrid.Margin = new Thickness(60, 50, 30, 25);
            }
            else
            {
                OsnovaGrid.Margin = new Thickness(30, 50, 30, 25);
                MenuGrid.Visibility = Visibility.Visible;
                Grid.SetColumn(MenuGrid, 0);
                Grid.SetColumnSpan(MenuGrid, 1);
                Grid.SetColumn(OsnovaGrid, 1);
                Grid.SetColumnSpan(OsnovaGrid, 1);
            }
        }
        public void DopInfaDliaBrand(DataGrid CarsServiceDataGrid)//Для DataAdmin
        {
            if (CarsServiceDataGrid.SelectedIndex != -1)
            {
                (CarsServiceDataGrid.SelectedItem as CarsService).AllCars = string.Empty;
                foreach (Cars car in cars)
                {
                    if (car.Brand == (CarsServiceDataGrid.SelectedItem as CarsService).Brand)
                    {
                        (CarsServiceDataGrid.SelectedItem as CarsService).AllCars += "\n" + new string(' ', 24) + $"{(car.Id),4}" + new string(' ', 5) + $"{car.Model}";
                    }
                }
                if ((CarsServiceDataGrid.SelectedItem as CarsService).AllCars == string.Empty)
                {
                    (CarsServiceDataGrid.SelectedItem as CarsService).AllCars += "\nnot available";
                }
            }
        }
        public void FastChoice(SqlConnection cn, SqlConnectionStringBuilder bldr, string strSQL, DataGrid CarsDataGrid, Button Allcars, object sender, TextBox FilterTextBox,ref string Country)//Для DataAdmin
        {
            if (Allcars.BorderBrush != null)
            {
                Allcars.BorderBrush = Brushes.Transparent;
            }
            if (selectedButton != null)
            {
                selectedButton.BorderBrush = Brushes.Transparent;
            }
            Button clickedButton = (Button)sender;
            clickedButton.BorderBrush = Brushes.Blue;
            selectedButton = clickedButton;
            Country = clickedButton.Name;
            if (selectedButton.Name.Equals("Allcars"))
            {
                strSQL = "SELECT * FROM Cars";
            }
            else
            {
                strSQL = "SELECT * FROM Cars JOIN CarsService ON Cars.Brand = CarsService.Brand WHERE CarsService.Country = '" + selectedButton.Name + "'";
            }
            using (cn = new SqlConnection(bldr.ConnectionString))
            {
                try
                {
                    cn.Open();
                    filtercarsCountry.Clear();
                    SqlDataAdapter da = new SqlDataAdapter(strSQL, cn);
                    DataTable t = new DataTable();
                    da.Fill(t);
                    foreach (DataRow row in t.Rows)
                    {
                        filtercarsCountry.Add(new Cars
                        (
                            (int)row["Id"],
                            (string)row["Brand"],
                            (string)row["Model"],
                            (int)row["Year"],
                            (int)row["Price"],
                            (string)row["Class"],
                            (string)row["TipKyzova"],
                            (string)row["TipDvigatelia"],
                            (int)row["Vdvigatelia"],
                            (string)row["KorobkaPeredach"],
                            (int)row["Probeg"],
                            "Images\\Cars\\" + (string)row["Foto"] + ".jpg"
                        ));
                    }
                    CarsDataGrid.ItemsSource = filtercarsCountry;
                    cn.Close();
                    Filter(CarsDataGrid, FilterTextBox);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void MachineRangeButton(DataGrid CarsDataGrid, DataGrid CarsServiceDataGrid, StackPanel FastChoice, Grid FilterGrid, TextBlock AutoText)//Для DataAdmin
        {
            CarsServiceDataGrid.Visibility = Visibility.Collapsed;
            CarsDataGrid.Visibility = Visibility.Visible;
            FastChoice.Visibility = Visibility.Visible;
            FilterGrid.Visibility = Visibility.Visible;
            AutoText.Margin = new Thickness(0, 20, 0, 0);
        }
        public void CarServiceButton(DataGrid CarsDataGrid, DataGrid CarsServiceDataGrid, StackPanel FastChoice, Grid FilterGrid, TextBlock AutoText)//Для DataAdmin
        {
            CarsDataGrid.Visibility = Visibility.Collapsed;
            CarsServiceDataGrid.Visibility = Visibility.Visible;
            FastChoice.Visibility = Visibility.Collapsed;
            FilterGrid.Visibility = Visibility.Collapsed;
            AutoText.Margin = new Thickness(0, 20, 0, 84);
        }
        public void AuthorizationButton(Window window)//Для DataAdmin
        {
            AuthorizationMenu authorizationMenu = new AuthorizationMenu();
            window.Visibility = Visibility.Collapsed;
            authorizationMenu.ShowDialog();
        }
        public void AddButton(Window window)//Для DataAdmin
        {
            AddWindow addWindow = new AddWindow();
            window.Visibility = Visibility.Collapsed;
            addWindow.ShowDialog();
        }
        public void FilterText(TextBox FilterTextBox, TextBlock TextBlockFilter)//Для DataAdmin
        {
            if (string.IsNullOrEmpty(FilterTextBox.Text))
            {
                TextBlockFilter.Text = "Search in cars ...";
            }
        }
        public void Report(DataGrid CarsDataGrid)
        {
            if (CarsDataGrid.Items.Count > 0 && CarsDataGrid.Columns.Count > 0)
            {
                Excel.Application exApp = new Excel.Application();
                exApp.Visible = true;
                Excel.Workbook workbook = exApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;

                for (int i = 0; i < CarsDataGrid.Items.Count; i++)
                {
                    var car = (Cars)CarsDataGrid.Items[i];
                    for (int j = 0; j < CarsDataGrid.Columns.Count; j++)
                    {
                        var propertyInfo = car.GetType().GetProperty(CarsDataGrid.Columns[j].Header.ToString());
                        if (propertyInfo != null)
                        {
                            var value = propertyInfo.GetValue(car);
                            worksheet.Cells[i + 1, j + 1] = value != null ? value.ToString() : string.Empty;
                        }
                    }
                }

                // Получение пути к рабочему столу пользователя
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                // Формирование полного пути к файлу Excel на рабочем столе
                string fileName = "Report.xlsx";
                string filePath = Path.Combine(desktopPath, fileName);

                // Проверка наличия файла с таким же именем
                int count = 1;
                while (File.Exists(filePath))
                {
                    fileName = $"Report ({count}).xlsx";
                    filePath = Path.Combine(desktopPath, fileName);
                    count++;
                }
                workbook.SaveAs(filePath);
            }
            else
            {
                // Обработка случая отсутствия данных
                MessageBox.Show("No data available for export to Excel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
