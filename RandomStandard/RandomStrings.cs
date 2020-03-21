using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RandomStandard
{
    public static class RandomStrings
    {
        private static Queue<NameModel> _Names = new Queue<NameModel>();
        private static List<string> _Towns = new List<string>();
        private static Int32 _CurrentName = 0;
        private static Int32 _CurrentTown = 0;

        public static NameModel GetName()
        {
            if (_Names == null || _Names.Count == 0)
            {
                HydrateNames();
            }

            return _Names.Dequeue();
        }

        public static string GetTownName()
        {
            if (_Towns == null || _Towns.Count == 0)
            {
                HydrateTownNames();
            }

            return _Towns[_CurrentTown++];
        }

        private static void HydrateNames()
        {
            var assembly = typeof(RandomStandard.RandomStrings).Assembly;
            Stream strm = assembly.GetManifestResourceStream("RandomStandard.resources.names.json");

            using (TextReader tr = new StreamReader(strm))
            {
                JsonSerializer serializer = new JsonSerializer();
                var nms = (List<NameModel>)serializer.Deserialize(tr, typeof(List<NameModel>));            
                _Names = new Queue<NameModel>( nms.OrderBy(n => RandomNumbers.GetDouble()).ToList());
            }
        }

        private static void HydrateTownNames()
        {
            var assembly = typeof(RandomStandard.RandomStrings).Assembly;
            Stream strm = assembly.GetManifestResourceStream("RandomStandard.resources.TownNames.txt");

            using (TextReader tr = new StreamReader(strm))
            {
                _Towns.AddRange(tr.ReadToEnd().Split(",".ToCharArray()));
            }

            _Towns = _Towns.OrderBy(n => RandomNumbers.GetDouble()).ToList();

        }
    }
    public class NameModel
    {
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
    }
}
