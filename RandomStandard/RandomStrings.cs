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
        private static List<string> _FemaleNames = new List<string>();
        private static List<string> _MaleNames = new List<string>();
        private static List<string> _StreetNames = new List<string>();
        private static Int32 _CurrentName = 0;
        private static Int32 _CurrentTown = 0;

        public static NameModel GetName(string gender = "M")
        {
            if (_Names == null || _Names.Count == 0)
            {
                HydrateNames();
                HydrateStreetNames();
            }

            var value = _Names.Dequeue();

            if (gender == "F")
            {
                value.FName = GetFemaleName();
                value.MName = GetFemaleName();
            }
            else
            {
                value.MName = GetMaleName();
            }

            return value;
        }

        public static string GetStreetName()
        {
            if (_StreetNames == null || _StreetNames.Count == 0)
            {
                HydrateStreetNames();
            }

            return _StreetNames[RandomStandard.RandomNumbers.GetInteger(_StreetNames.Count)];
        }

        private static void HydrateStreetNames()
        {
            var assembly = typeof(RandomStandard.RandomStrings).Assembly;
            Stream strm = assembly.GetManifestResourceStream("RandomStandard.resources.Streets.txt");
            string result;

            using (TextReader tr = new StreamReader(strm))
            {
                result = tr.ReadToEnd();
                result = result.Replace("\r", "");
                _StreetNames = result.Split("\n".ToCharArray()).ToList();
            }
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
                _Names = new Queue<NameModel>(nms.OrderBy(n => RandomNumbers.GetDouble()).ToList());
            }
        }

        private static void HydrateFemaleNames()
        {
            var assembly = typeof(RandomStandard.RandomStrings).Assembly;
            Stream strm = assembly.GetManifestResourceStream("RandomStandard.resources.FemaleNames.txt");
            string result;

            using (TextReader tr = new StreamReader(strm))
            {
                result = tr.ReadToEnd().Replace("\r\n", "");
                _FemaleNames = result.Split(",".ToCharArray()).ToList();
            }
        }

        private static void HydrateMaleNames()
        {
            var assembly = typeof(RandomStandard.RandomStrings).Assembly;
            Stream strm = assembly.GetManifestResourceStream("RandomStandard.resources.MaleNames.txt");
            string result;

            using (TextReader tr = new StreamReader(strm))
            {
                result = tr.ReadToEnd().Replace("\r", "");
                _MaleNames = result.Split("\n".ToCharArray()).ToList();
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

        private static string GetMaleName()
        {
            if (_MaleNames == null || _MaleNames.Count == 0)
            {
                HydrateMaleNames();
            }

            return _MaleNames[RandomStandard.RandomNumbers.GetInteger(_MaleNames.Count)];
        }


        private static string GetFemaleName()
        {
            if (_FemaleNames == null || _FemaleNames.Count == 0)
            {
                HydrateFemaleNames();
            }

            return _FemaleNames[RandomStandard.RandomNumbers.GetInteger(_FemaleNames.Count)];
        }
    }
    public class NameModel
    {
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
    }
}
