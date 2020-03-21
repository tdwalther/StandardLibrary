using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RandomStandard;

namespace XUnitTestProject1
{
    public class UnitTest2
    {
        [Fact]
        public void TestNames()
        {
            var name = RandomStrings.GetName();
        }
    }
}
