using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.Fake
{
    public class RandomStub : Random
    {
        public int ExpectedNext { get; set; }

        public override int Next()
        {
            return ExpectedNext;
        }

        public override int Next(int minValue, int maxValue)
        {
            return ExpectedNext;
        }
    }
}
