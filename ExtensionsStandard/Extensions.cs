using System;
using System.Collections.Generic;

namespace ExtensionsStandard
{
    public static class Extensions
    {
        public static double TwoDec(this double d)
        {
            return Math.Round(d, 2);
        }

        public static double Sqr(this double d)
        {
            return Math.Pow(d, 2);
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}
