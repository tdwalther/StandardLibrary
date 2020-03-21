using MvvmStandard;

namespace WpfCoreTestApp
{
    public class PersonVM : ViewModelBase
    {
        private string _FirstName;
        private string _LastName;
        private int _Age;
        private double _Rating;
        private string _Town;
        private string _Region;
        private double _Lat;
        private double _Lon;

        public string FirstName { get => _FirstName; set { _FirstName = value; OnPropertyChanged("FirstName"); } }
        public string LastName { get => _LastName; set { _LastName = value; OnPropertyChanged("LastName"); } }
        public int Age { get => _Age; set { _Age = value; OnPropertyChanged("Age"); } }
        public double Rating { get => _Rating; set { _Rating = value; OnPropertyChanged("Rating"); } }
        public string Town { get => _Town; set { _Town = value; OnPropertyChanged("Town"); } }
        public string Region { get => _Region; set { _Region = value; OnPropertyChanged("Region"); } }
        public double Lat { get => _Lat; set { _Lat = value; OnPropertyChanged("Lat"); } }
        public double Lon { get => _Lon; set { _Lon = value; OnPropertyChanged("Lon"); } }
    }
}
