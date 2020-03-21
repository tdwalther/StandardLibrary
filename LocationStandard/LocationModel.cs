using System;
using System.Linq;
using System.Text;

namespace LocationStandard
{
    public class LocationModel
    {
        public string Town { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Region { get; set; }

        public LocationModel Clone()
        {
            return MemberwiseClone() as LocationModel;
        }

        public double DistanceTo(LocationModel loc)
        {
            return (Math.Sqrt(Math.Pow((Lat - loc.Lat), 2) + Math.Pow((Lon - loc.Lon), 2)) / 1.41);
        }
    }
}
