using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using static ООП_Курсовая.GlobalClass;

namespace ООП_Курсовая
{
    public class AdminMethodi : Methodi, IAdminMethodi
    {
        public void Edit(SqlConnection cn, SqlConnectionStringBuilder bldr, DataGrid CarsDataGrid)//Для DataAdmin
        {
            if (CarsDataGrid.SelectedItem != null)
            {
                Cars selectedCar = (Cars)CarsDataGrid.SelectedItem;
                using (cn = new SqlConnection(bldr.ConnectionString))
                {
                    try
                    {
                        cn.Open();
                        string strUpd = "UPDATE Cars " + "SET Price=@Price WHERE Id = @Id";
                        SqlCommand cmdUpd = new SqlCommand(strUpd, cn);
                        cmdUpd.Parameters.AddWithValue("@Id", Convert.ToInt32(selectedCar.Id));
                        cmdUpd.Parameters.AddWithValue("@Price", Convert.ToInt32(selectedCar.Price));
                        cmdUpd.ExecuteNonQuery();
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        public void Remove(SqlConnection cn, SqlConnectionStringBuilder bldr, DataGrid CarsDataGrid)//Для DataAdmin
        {
            if (CarsDataGrid.SelectedItem != null)
            {
                Cars selectedCar = (Cars)CarsDataGrid.SelectedItem;
                int Id = selectedCar.Id;
                cars.Remove(selectedCar);
                using (cn = new SqlConnection(bldr.ConnectionString))
                {
                    try
                    {
                        cn.Open();
                        string strDel = "DELETE Cars WHERE Id=@Id";
                        SqlCommand cmdDel = new SqlCommand(strDel, cn);
                        cmdDel.Parameters.AddWithValue("@Id", Id);
                        cmdDel.ExecuteNonQuery();
                        cn.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        public void Dobavlenie(SqlConnection cn, SqlConnectionStringBuilder bldr, string strSQL, ComboBox ComboBox1, ComboBox ComboBox2, TextBox AddTextBox1, TextBox AddTextBox2, ComboBox ComboBox3, ComboBox ComboBox4, ComboBox ComboBox5, ComboBox ComboBox6, ComboBox ComboBox7, TextBox AddTextBox3)//Для AddWindow
        {
            int Year = 2010;
            int Price = 5000;
            int Probeg = 100000;
            try
            {
                if (Convert.ToInt32(AddTextBox1.Text) <= 2000)
                {
                    Year = 2000;
                }
                else if (Convert.ToInt32(AddTextBox1.Text) > 2023)
                {
                    Year = 2023;
                }
            }
            catch
            {
                Year = 2010;
            }
            try
            {
                if (Convert.ToInt32(AddTextBox2.Text) <= 1000)
                {
                    Price = 1000;
                }
                else if (Convert.ToInt32(AddTextBox2.Text) > 100000)
                {
                    Price = 10000;
                }
            }
            catch
            {
                Price = 5000;
            }
            try
            {
                if (Convert.ToInt32(AddTextBox3.Text) > 500000)
                {
                    Probeg = 500000;
                }
                else if (Convert.ToInt32(AddTextBox3.Text) <= 10000)
                {
                    Probeg = 10000;
                }
            }
            catch
            {
                Probeg = 100000;
            }
            strSQL = "SELECT * FROM Cars";
            using (cn = new SqlConnection(bldr.ConnectionString))
            {
                try
                {
                    cn.Open();
                    Cars car = new Cars
                    (
                         1,
                         ComboBox1.Text,
                         ComboBox2.Text,
                         Year,
                         Price,
                         ComboBox3.Text,
                         ComboBox4.Text,
                         ComboBox5.Text,
                         Convert.ToInt32(ComboBox6.Text),
                         ComboBox7.Text,
                         Probeg,
                         ComboBox2.Text
                    );
                    cars.Add(car);
                    SqlDataAdapter da = new SqlDataAdapter(strSQL, cn);
                    DataTable t = new DataTable("Cars");
                    da.Fill(t);
                    DataRow rowToInsert;
                    rowToInsert = t.Rows.Add(new object[]
                      {
                                 car.Id, car.Brand, car.Model, car.Year, car.Price, car.Class, car.TipKyzova, car.TipDvigatelia, car.Vdvigatelia, car.KorobkaPeredach, car.Probeg, car.Foto
                      });
                    string strInsert = "INSERT INTO Cars (Brand, Model, Year, Price, Class, TipKyzova, TipDvigatelia, Vdvigatelia, KorobkaPeredach, Probeg, Foto)" + "VALUES (@Brand, @Model, @Year, @Price, @Class, @TipKyzova, @TipDvigatelia, @Vdvigatelia, @KorobkaPeredach, @Probeg, @Foto)";
                    SqlCommand cmdInsert = new SqlCommand(strInsert, cn);
                    cmdInsert.Parameters.AddWithValue("@Brand", car.Brand);
                    cmdInsert.Parameters.AddWithValue("@Model", car.Model);
                    cmdInsert.Parameters.AddWithValue("@Year", Convert.ToInt32(car.Year));
                    cmdInsert.Parameters.AddWithValue("@Price", Convert.ToInt32(car.Price));
                    cmdInsert.Parameters.AddWithValue("@Class", car.Class);
                    cmdInsert.Parameters.AddWithValue("@TipKyzova", car.TipKyzova);
                    cmdInsert.Parameters.AddWithValue("@TipDvigatelia", car.TipDvigatelia);
                    cmdInsert.Parameters.AddWithValue("@Vdvigatelia", Convert.ToInt32(car.Vdvigatelia));
                    cmdInsert.Parameters.AddWithValue("@KorobkaPeredach", car.KorobkaPeredach);
                    cmdInsert.Parameters.AddWithValue("@Probeg", Convert.ToInt32(car.Probeg));
                    cmdInsert.Parameters.AddWithValue("@Foto", car.Foto);
                    cmdInsert.ExecuteNonQuery();
                    rowToInsert.AcceptChanges();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошло исключение: " + ex.Message);
                    AddWindow addWindow = new AddWindow();
                    addWindow.ShowDialog();
                }
            }
        }
        public void AddWindowButtons(Window addWindow)//Для AddWindow
        {
            addWindow.Close();
            DataAdmin dataAdmin = new DataAdmin(true);
            dataAdmin.ShowDialog();
        }
        public void ComboBox1Details(ComboBox ComboBox1, ComboBox ComboBox2)//Для AddWindow
        {
            string selectedValue = ((ComboBoxItem)ComboBox1.SelectedItem).Content.ToString();
            if (selectedValue == "Audi")
            {
                ComboBox2.Items.Clear();
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "A3" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "A4" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "A6" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "A8" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "Allroad" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "S60" });
            }
            else if (selectedValue == "Honda")
            {
                ComboBox2.Items.Clear();
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "Accord" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "CR-V" });
            }
            else if (selectedValue == "Mercedes-Benz")
            {
                ComboBox2.Items.Clear();
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "S600LW220" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "E320W211" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "CLK240W163" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "ML350W163" });
            }
            else if (selectedValue == "Peugeot")
            {
                ComboBox2.Items.Clear();
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "607" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "406 SR" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "307 Auto" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "307 SW" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "206" });
            }
            else if (selectedValue == "Toyota")
            {
                ComboBox2.Items.Clear();
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "LandCruiser UZJ105L" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "Camry ACV30L" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "Corolla ZZE121L" });
            }
            else if (selectedValue == "Volkswagen")
            {
                ComboBox2.Items.Clear();
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "Passat" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "Golf" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "Touareg V6" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "Phaeton V6" });
            }
            else if (selectedValue == "Volvo")
            {
                ComboBox2.Items.Clear();
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "S60" });
                ComboBox2.Items.Add(new ComboBoxItem() { Content = "S80" });
            }
        }
        public void ComboBox5Details(ComboBox ComboBox5, ComboBox ComboBox6)//Для AddWindow
        {
            string selectedValue = ((ComboBoxItem)ComboBox5.SelectedItem).Content.ToString();
            if (selectedValue == "Electric")
            {
                ComboBox6.Items.Clear();
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "0" });
            }
            else
            {
                ComboBox6.Items.Clear();
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "2000" });
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "2200" });
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "2400" });
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "2600" });
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "2800" });
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "3000" });
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "3200" });
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "3400" });
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "3600" });
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "3800" });
                ComboBox6.Items.Add(new ComboBoxItem() { Content = "4000" });
            }
        }
        public void CancelInPassword(RoutedEventArgs e, Window window)//Для PasswordWindow
        {
            AuthorizationMenu authorizationMenu = new AuthorizationMenu();
            window.Visibility = Visibility.Collapsed;
            authorizationMenu.ShowDialog();
        }
        public void AceptInPassword(RoutedEventArgs e, Window window, TextBox passwordTextBox)//Для PasswordWindow
        {
            if (passwordTextBox.Text == Convert.ToString("*****"))
            {
                DataAdmin dataAdmin = new DataAdmin(true);
                window.Visibility = Visibility.Collapsed;
                dataAdmin.ShowDialog();
            }
        }
    }
}
