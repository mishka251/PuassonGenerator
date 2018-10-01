using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuassonGeneration
{
    delegate double lambda_f(double t);
    static class Generator
    {
        public static List<double> CreateUniform(double T, double lambda)
        {
            var r = new Random(/*DateTime.Now.Millisecond*/);
            double t = 0;
            List<double> S = new List<double>();

            while (t < T)
            {
                var U = r.NextDouble();

                t -= (Math.Log(U) / Math.Log(2)) / lambda;
                if (t < T)
                    S.Add(t);
            }
            return S;
        }

        public static List<double> CreateNotUniform(double T, double lambda, lambda_f lambda_t)
        {
            var r = new Random(DateTime.Now.Millisecond);
            double t = 0;
            List<double> S = new List<double>();
            while (t < T)
            {
                var U1 = r.NextDouble();
                t -= 1 / lambda * Math.Log(U1, 2);
                if (t <= T)
                {
                    Random r2 = new Random(DateTime.Now.Millisecond);
                    var U2 = r2.NextDouble();
                    if (U2 < lambda_t(t) / lambda)
                        S.Add(t);
                }
            }
            return S;
        }

    }


}
