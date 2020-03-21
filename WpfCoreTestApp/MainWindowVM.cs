using MvvmStandard;
using ObserverStandard;
using RandomStandard;
using ExtensionsStandard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using LocationStandard;

namespace WpfCoreTestApp
{
    public class MainWindowVM : ViewModelBase
    {
        public ICommand TestObserverCommand { get; set; }

        public Subject Notifier { get; set; }

        private string _Greeting;
        private List<PersonVM> _People = new List<PersonVM>();

        public string Greeting { get => _Greeting; set { _Greeting = value; OnPropertyChanged("Greeting"); } }

        public List<PersonVM> People { get => _People; set { _People = value; OnPropertyChanged("People"); } }

        public MainWindowVM()
        {
            Notifier = new Subject();
            TestObserverCommand = new RelayCommand(new Action<object>((o)=>
            {
                Greeting = "Eagles will beat the Pats";
                Notifier.Notify("Prediction", "Eagles > Pats");
            }));
            HydratePeople(10);
        }

        private void HydratePeople(int num)
        {
            People.Clear();
            for( int i=0; i<num; i++)
            {
                var name = RandomStrings.GetName();
                var loc = LocationFactory.GetLocation("NorthEast");
                People.Add(new PersonVM()
                {
                    FirstName = name.FName,
                    LastName = name.LName,
                    Age = RandomNumbers.GetInteger(10),
                    Town = loc.Town,
                    Region = loc.Region,
                    Lat = loc.Lat,
                    Lon = loc.Lon,
                    Rating = RandomNumbers.GetNormal(0.5, 0.2).TwoDec()
                });
            }
        }
    }
}
