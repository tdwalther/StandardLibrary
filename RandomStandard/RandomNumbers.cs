using System;
using System.Collections.Generic;
using System.Text;

namespace RandomStandard
{
    public static class RandomNumbers
    {
        static Random rnd = new Random(System.DateTime.Now.Millisecond);
        private static bool m_StoredUniformDeviateIsGood = false;
        private static double m_StoredUniformDeviate = 0;

        public static double GetDouble()
        {
            return Math.Round(rnd.NextDouble(), 2);
        }

        public static double GetDouble(double ctr)
        {
            double hiband, loband;
            hiband = 1 - ctr;
            loband = ctr;

            if (hiband > loband)
            {
                ctr += hiband * GetDouble() * GetDouble() * GetDouble();
                ctr -= loband * GetDouble() * GetDouble();
            }
            else
            {
                ctr += hiband * GetDouble() * GetDouble();
                ctr -= loband * GetDouble() * GetDouble() * GetDouble();
            }

            return Math.Round(ctr, 2);
        }

        public static int GetInteger(int max)
        {
            if (rnd.Next() == 0)
            {
                rnd = new Random();
            }
            return rnd.Next(max);
        }

        public static double GetNormal(
            double mean,
            double stddev)
        {
            double u1 = 1.0 - rnd.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rnd.NextDouble();
            double randNormal = -1;

            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            randNormal = mean + stddev * randStdNormal; //random normal(mean,stdDev^2)

            randNormal = randNormal < 0 ? Math.Abs(randNormal) : randNormal;
            randNormal = randNormal > 1 ? randNormal - Math.Floor(randNormal) : randNormal;
            return randNormal;
        }

        private static double GetNormal()
        {
            double u1 = GetDouble();
            double u2 = GetDouble();
            double r = Math.Sqrt(-2.0 * Math.Log(0.98 * u1 + 0.01));
            double theta = 2.0 * Math.PI * u2;
            double val = r * Math.Sin(theta);

            return val;
        }

        public static double NextNormal()
        {
            // based on algorithm from Numerical Recipes
            if (m_StoredUniformDeviateIsGood)
            {
                m_StoredUniformDeviateIsGood = false;
                return m_StoredUniformDeviate;
            }
            else
            {
                double rsq = 0.0;
                double v1 = 0.0, v2 = 0.0, fac = 0.0;
                while (rsq >= 1.0 || rsq == 0.0)
                {
                    v1 = 2.0 * GetDouble() - 1.0;
                    v2 = 2.0 * GetDouble() - 1.0;
                    rsq = v1 * v1 + v2 * v2;
                }
                fac = System.Math.Sqrt(-2.0
                   * System.Math.Log(rsq, System.Math.E) / rsq);
                m_StoredUniformDeviate = ((0.125 * v1 * fac) + 0.51);
                m_StoredUniformDeviateIsGood = true;
                double x = ((0.125 * v2 * fac) + 0.51);
                x = x > 1 ? 1 : x;
                x = x < 0 ? 0 : x;
                return x;
            }
        }
    }
}
