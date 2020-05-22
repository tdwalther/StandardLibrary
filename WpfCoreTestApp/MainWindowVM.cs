using MvvmStandard;
using ObserverStandard;
using RandomStandard;
using ExtensionsStandard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using LocationStandard;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace WpfCoreTestApp
{
    public class MainWindowVM : ViewModelBase
    {
        public ICommand TestObserverCommand { get; set; }
        public ICommand RunCommand { get; set; }
        public ICommand LocationsCommand { get; set; }

        public Subject Notifier { get; set; }

        private ObservableCollection<CircleVM> _Rectangles = new ObservableCollection<CircleVM>();
        private string _Greeting;
        private List<PersonVM> _People = new List<PersonVM>();

        public string Greeting { get => _Greeting; set { _Greeting = value; OnPropertyChanged("Greeting"); } }

        public List<PersonVM> People { get => _People; set { _People = value; OnPropertyChanged("People"); } }

        public ObservableCollection<CircleVM> Rectangles { get => _Rectangles; set { _Rectangles = value; OnPropertyChanged("Rectangles"); } }

        public MainWindowVM()
        {
            Notifier = new Subject();
            TestObserverCommand = new RelayCommand(new Action<object>((o) =>
            {
                Greeting = "Eagles will beat the Pats";
                Notifier.Notify("Prediction", "Eagles > Pats");
            }));

            RunCommand = new RelayCommand(new Action<object>((o) =>
            {
                HydrateCircles(500);
                Task.Run(() =>
                {
                    while (true)
                    {
                        foreach (var cir in Rectangles)
                        {
                            cir.Pause = cir.Pause == 0 ? 0 : cir.Pause - 1;
                            if (cir.Pause == 0)
                            {
                                cir.Color = "Blue";
                                cir.X += RandomNumbers.GetInteger(4);
                                cir.Y += RandomNumbers.GetInteger(3) - 1;
                            }
                        }

                        foreach (var rct1 in Rectangles)
                        {
                            foreach (var rct2 in Rectangles)
                            {
                                if (rct1 != rct2)
                                {
                                    if (rct1.DoesOverlap(rct2))
                                    {
                                        rct1.Color = "Green";
                                        rct2.Color = "Green";
                                        if (rct1.Pause == 0)
                                        {
                                            rct1.Pause = 2;
                                        }
                                    }
                                }
                            }
                        }
                        Thread.Sleep(2);
                    }
                });
            }));

            LocationsCommand = new RelayCommand(new Action<object>((o)=> 
            {
                List<LocationModel> locations = new List<LocationModel>();
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\tdw12\Desktop\Towns.csv");

                foreach( var l in lines.Skip(1))
                {
                    var loc = l.Split(",".ToCharArray());
                    locations.Add(new LocationModel()
                    {
                        City = loc[0].Trim(),
                        State = loc[1].Trim(),
                        Country = loc[2].Trim(),
                        Lat = Convert.ToDouble( loc[3].Trim()),
                        Lon = Convert.ToDouble(loc[4].Trim()),
                        Region = ""
                    }) ;
                }

                var jsonString = JsonSerializer.Serialize(locations);
                File.WriteAllText(@"C:\Users\tdw12\Desktop\Towns.json", jsonString);

            }));

            HydratePeople(10);
        }

        private void HydrateCircles(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Rectangles.Add(new CircleVM()
                {
                    Radius = 10,
                    X = RandomNumbers.GetInteger(800) - 800,
                    Y = 50 + RandomNumbers.GetInteger(400),
                    Color = "Blue"
                });

            }
        }

        private void HydratePeople(int num)
        {
            People.Clear();
            for (int i = 0; i < num; i++)
            {

                var name = RandomNumbers.GetDouble() > 0.5 ? RandomStrings.GetName("F") : RandomStrings.GetName("M");
                var loc = LocationFactory2.GetLocation();
                People.Add(new PersonVM()
                {
                    FirstName = name.FName,
                    MiddleName = name.MName,
                    LastName = name.LName,
                    Age = RandomNumbers.GetInteger(10),
                    Town = loc.City,
                    Region = loc.Region,
                    Lat = loc.Lat,
                    Lon = loc.Lon,
                    Rating = RandomNumbers.GetNormal(0.5, 0.2).TwoDec()
                });
            }
        }
    }

    public class CircleVM : ViewModelBase
    {
        private int _X;
        private int _Y;
        private int _Radius;
        private string _Color;
        private int _Pause;

        public int X { get => _X; set { _X = value; OnPropertyChanged("X"); } }
        public int Y { get => _Y; set { _Y = value; OnPropertyChanged("Y"); } }
        public int Height { get => 2 * Radius; }
        public int Width { get => 2 * Radius; }

        public string Color { get => _Color; set { _Color = value; OnPropertyChanged("Color"); } }

        public int Radius { get => _Radius; set => _Radius = value; }
        public int Pause { get => _Pause; set { _Pause = value; OnPropertyChanged("Pause"); } }
    }

    public static class RectangleExtensions
    {
        public static bool DoesOverlap(this CircleVM cir1, CircleVM cir2)
        {

            return (Math.Sqrt(Math.Pow(cir1.X - cir2.X, 2) + Math.Pow(cir1.Y - cir2.Y, 2)) < 2 * cir1.Radius);


        }
    }
}
