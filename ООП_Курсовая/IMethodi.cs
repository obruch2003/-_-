using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows;

namespace ООП_Курсовая
{
    internal interface IMethodi
    {
        void Vivod(SqlConnection cn, SqlConnectionStringBuilder bldr, string strSQL, DataGrid CarsDataGrid);//Для DataAdmin
        void Vivod2(SqlConnection cn, SqlConnectionStringBuilder bldr, string strSQL, DataGrid CarsService);//Для DataAdmin
        void Filter(DataGrid CarsDataGrid, TextBox FilterTextBox);//Для DataAdmin
        void DopInfaInDataGrid(DataGrid datagrid);//Для DataAdmin
        void SkritieIOtkritieMenu(Grid MenuGrid, Grid OsnovaGrid);//Для DataAdmin
        void DopInfaDliaBrand(DataGrid CarsServiceDataGrid);//Для DataAdmin
        void FastChoice(SqlConnection cn, SqlConnectionStringBuilder bldr, string strSQL, DataGrid CarsDataGrid, Button Allcars, object sender, TextBox FilterTextBox, ref string Country);//Для DataAdmin
        void AdminOrUserButton(Window window, int n);//Для AuthorizationMenu
        void MachineRangeButton(DataGrid CarsDataGrid, DataGrid CarsServiceDataGrid, StackPanel FastChoice, Grid FilterGrid, TextBlock AutoText);//Для DataAdmin
        void CarServiceButton(DataGrid CarsDataGrid, DataGrid CarsServiceDataGrid, StackPanel FastChoice, Grid FilterGrid, TextBlock AutoText);//Для DataAdmin
        void AuthorizationButton(Window window);//Для DataAdmin
        void AddButton(Window window);//Для DataAdmin
        void FilterText(TextBox FilterTextBox, TextBlock TextBlockFilter);//Для DataAdmin      
    }
}
