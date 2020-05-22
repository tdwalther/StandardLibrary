using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RandomStandard;
using System.Diagnostics;

namespace XUnitTestProject1
{
    public class UnitTest2
    {
        [Fact]
        public void TestNames()
        {
            var name = RandomStrings.GetName();
        }

        [Fact]
        public void TestTowns()
        {
            var town1 = LocationStandard.LocationFactory2.GetLocation();
            var town2 = LocationStandard.LocationFactory2.GetLocation();

            var dist = town1.DistanceTo(town2, 'M');

            Debug.WriteLine($"{town1.City} {town1.State} to {town2.City} {town2.State}:  {dist}");

        }
    }
}
