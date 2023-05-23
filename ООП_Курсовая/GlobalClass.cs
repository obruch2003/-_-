using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ООП_Курсовая
{
    public class GlobalClass
    {
        static Random rnd = new Random();
        public class Cars
        {    
            public int Id { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public int Year { get; set; }
            public int Price { get; set; }
            public Brush BgColor { get; set; }
            public string Class { get; set; }
            public string TipKyzova { get; set; }
            public string TipDvigatelia { get; set; }
            public int Vdvigatelia { get; set; }
            public string KorobkaPeredach { get; set; }
            public int Probeg { get; set; }
            public string Foto { get; set; }

            public Cars() { }
            public Cars(int Id, string Brand, string Model, int Year, int Price, string Class, string TipKyzova, string TipDvigatelia, int Vdvigatelia, string KorobkaPeredach, int Probeg, string Foto)
            {
                var converter = new BrushConverter();
                this.Id = Id;
                this.Brand = Brand;
                this.Model = Model;
                this.Year = Year;
                this.Price = Price;
                this.BgColor = (Brush)converter.ConvertFromString("#" + rnd.Next(100000, 999999));
                this.Class = Class;
                this.TipKyzova = TipKyzova;
                this.TipDvigatelia = TipDvigatelia;
                this.Vdvigatelia = Vdvigatelia;
                this.KorobkaPeredach = KorobkaPeredach;
                this.Probeg = Probeg;
                this.Foto = Foto;
            }
        }
        public class CarsService
        {
            public int Number { get; set; }
            public string Brand { get; set; }
            public string Country { get; set; }
            public string CountryImage { get; set; }
            public string Carservice { get; set; }
            public string Office { get; set; }
            public string AllCars { get; set; }
            public Brush BgColor { get; set; }

            public CarsService() { }
            public CarsService(int Number, string Brand, string Country, string CountryImage,string Carservice,string Office, string AllCars)
            {
                var converter = new BrushConverter();
                this.Number = Number;
                this.Brand = Brand;
                this.Country = Country;
                this.CountryImage = CountryImage;
                this.Carservice = Carservice;
                this.Office = Office;
                this.AllCars = AllCars;
                this.BgColor = (Brush)converter.ConvertFromString("#" + rnd.Next(100000, 999999));
            }
        }
        public static ObservableCollection<Cars> cars = new ObservableCollection<Cars>();
        public static ObservableCollection<CarsService> carsService = new ObservableCollection<CarsService>();
    }
}
