using System;
using System.Linq;
using System.Text;

namespace LocationStandard
{
    public class LocationModel
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Region { get; set; }

        public LocationModel Clone()
        {
            return MemberwiseClone() as LocationModel;
        }

        public double DistanceTo(LocationModel loc, char unit)
        {
            double theta = this.Lon - loc.Lon;
            double dist = Math.Sin(deg2rad(this.Lat)) * Math.Sin(deg2rad(loc.Lat)) + 
                Math.Cos(deg2rad(this.Lat)) * Math.Cos(deg2rad(loc.Lat)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        /*::  This function converts decimal degrees to radians             :*/
        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        /*::  This function converts radians to decimal degrees             :*/
        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        private double rad2deg(double rad)
        {
            return (rad * 180.0 / Math.PI);
        }

        public double DistanceTo(LocationModel loc)
        {
            return (Math.Sqrt(Math.Pow((Lat - loc.Lat), 2) + Math.Pow((Lon - loc.Lon), 2)) / 1.41);
        }
    }
}
