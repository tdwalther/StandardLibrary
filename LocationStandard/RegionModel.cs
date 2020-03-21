namespace LocationStandard
{
    public class RegionModel
    {
        public string Name { get; set; }
        public double MinLat { get; set; }
        public double MaxLat { get; set; }
        public double MinLon { get; set; }
        public double MaxLon { get; set; }

        public bool IsInRegion(LocationModel loc)
        {
            return (loc.Lat >= MinLat & loc.Lat <= MaxLat & loc.Lon >= MinLon & loc.Lon <= MaxLon);
        }
    }
}
