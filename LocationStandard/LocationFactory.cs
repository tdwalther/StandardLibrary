using ExtensionsStandard;
using Newtonsoft.Json;
using RandomStandard;
using System;
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

        public static LocationModel GetLocation()
        {
            if (Locations.Count == 0)
            {
                HydrateLocations();
            }
            return Locations[RandomNumbers.GetInteger(Locations.Count)];
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

    //public static class LocationFactory
    //{
    //    private static List<RegionModel> _Regions = GenerateRegions();

    //    public static List<RegionModel> Regions
    //    {
    //        get
    //        {
    //            return LocationFactory._Regions;
    //        }
    //        set { LocationFactory._Regions = value; }
    //    }

    //    public static LocationModel GetLocation()
    //    {
    //        LocationModel loc = new LocationModel()
    //        {
    //            City = RandomStrings.GetTownName(),
    //            Lat = RandomNumbers.GetDouble().TwoDec(),
    //            Lon = RandomNumbers.GetDouble().TwoDec()
    //        };

    //        loc.Region = Regions.FirstOrDefault(r => r.IsInRegion(loc))?.Name;

    //        return loc;
    //    }

    //    public static LocationModel GetLocation(string reg)
    //    {
    //        double minLat = Regions.First(r => r.Name == reg).MinLat;
    //        double maxLat = Regions.First(r => r.Name == reg).MaxLat;
    //        double minLon = Regions.First(r => r.Name == reg).MinLon;
    //        double maxLon = Regions.First(r => r.Name == reg).MaxLon;

    //        LocationModel loc = new LocationModel()
    //        {
    //            Lat = ((maxLat - minLat) * RandomNumbers.GetDouble() + minLat).TwoDec(),
    //            Lon = ((maxLon - minLon) * RandomNumbers.GetDouble() + minLon).TwoDec(),
    //            City = RandomStrings.GetTownName()
    //        };
    //        loc.Region = Regions.First(r => r.IsInRegion(loc)).Name;
    //        return loc;
    //    }

    //    private static List<RegionModel> GenerateRegions()
    //    {
    //        var regs = new List<RegionModel>();

    //        regs.Add(new RegionModel()
    //        {
    //            Name = "NorthEast",
    //            MinLat = 0.26,
    //            MaxLat = .5,
    //            MinLon = .76,
    //            MaxLon = 1.00
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "NewEngland",
    //            MinLat = 0.00,
    //            MaxLat = 0.25,
    //            MinLon = 0.76,
    //            MaxLon = 1.00
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Central",
    //            MinLat = 0.26,
    //            MaxLat = 0.5,
    //            MinLon = 0.51,
    //            MaxLon = 0.75
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Lakes",
    //            MinLat = 0.0,
    //            MaxLat = 0.25,
    //            MinLon = 0.51,
    //            MaxLon = 0.75
    //        });

    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Atlantic",
    //            MinLat = 0.51,
    //            MaxLat = .75,
    //            MinLon = .76,
    //            MaxLon = 1.00
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "SouthEast",
    //            MinLat = 0.76,
    //            MaxLat = 1.00,
    //            MinLon = 0.76,
    //            MaxLon = 1.00
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "South",
    //            MinLat = 0.76,
    //            MaxLat = 1.0,
    //            MinLon = 0.51,
    //            MaxLon = 0.75
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Ozarks",
    //            MinLat = 0.51,
    //            MaxLat = 0.75,
    //            MinLon = 0.51,
    //            MaxLon = 0.75
    //        });

    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Prarie",
    //            MinLat = 0.51,
    //            MaxLat = .75,
    //            MinLon = .26,
    //            MaxLon = 0.5
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "West",
    //            MinLat = 0.51,
    //            MaxLat = 0.75,
    //            MinLon = 0.0,
    //            MaxLon = 0.25
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Southwest",
    //            MinLat = 0.76,
    //            MaxLat = 1.0,
    //            MinLon = 0.26,
    //            MaxLon = 0.5
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Baja",
    //            MinLat = 0.76,
    //            MaxLat = 1.0,
    //            MinLon = 0.0,
    //            MaxLon = 0.25
    //        });

    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Mountain",
    //            MinLat = 0.26,
    //            MaxLat = .5,
    //            MinLon = .26,
    //            MaxLon = 0.5
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Plains",
    //            MinLat = 0.00,
    //            MaxLat = 0.25,
    //            MinLon = 0.26,
    //            MaxLon = 0.5
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Northwest",
    //            MinLat = 0.0,
    //            MaxLat = 0.25,
    //            MinLon = 0.0,
    //            MaxLon = 0.25
    //        });
    //        regs.Add(new RegionModel()
    //        {
    //            Name = "Pacific",
    //            MinLat = 0.26,
    //            MaxLat = 0.5,
    //            MinLon = 0.0,
    //            MaxLon = 0.25
    //        });

    //        return regs;
    //    }
    //}
}
