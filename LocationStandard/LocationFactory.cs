using Newtonsoft.Json;
using RandomStandard;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LocationStandard
{
    public static class LocationFactory2
    {
        public static List<LocationModel> Locations = new List<LocationModel>();
        public static int locCounter = 0;

        public static LocationModel GetUniqueLocation()
        {
            if (Locations.Count == 0)
            {
                HydrateLocations();
            }

            if (locCounter >= Locations.Count)
            {
                return null;
            }
            else
            {
                return Locations[locCounter++];
            }
        }

        public static LocationModel GetLocation(string city = "")
        {
            if (Locations.Count == 0)
            {
                HydrateLocations();
            }
            if(!string.IsNullOrEmpty(city))
            {
                var loc = Locations.FirstOrDefault(l => l.City == city);
                return loc;
            }
            else
            {
                return Locations[RandomNumbers.GetInteger(Locations.Count)];
            }
        }

        public static List<LocationModel> GetLocations(string st)
        {
            if (Locations.Count == 0)
            {
                HydrateLocations();
            }
            return Locations.Where(l => l.State == st).ToList();
        }

        private static void HydrateLocations()
        {
            var assembly = typeof(LocationStandard.LocationModel).Assembly;
            Stream strm = assembly.GetManifestResourceStream("LocationStandard.resources.Towns.json");

            using (TextReader tr = new StreamReader(strm))
            {
                JsonSerializer serializer = new JsonSerializer();
                var nms = (List<LocationModel>)serializer.Deserialize(tr, typeof(List<LocationModel>));
                Locations = new List<LocationModel>(nms.Where(n => n.State != "XXX").ToList());
            }
        }
    }
}
