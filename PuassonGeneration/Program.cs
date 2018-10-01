using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuassonGeneration
{
    class Program
    {
        static lambda_f[] lambdas =
             {
             (t) => 1 / (t + 1),
             (t) => 1 / Math.Pow((t + 1), 2),
             (t) => 1 / Math.Exp(t + t / 2),
             (t)=>Math.Sin(t)+2,
             (t)=>Math.Cos(t+1)+1
        };

        static double T, lambda;
        static int index;

        static void Input()
        {
            Console.WriteLine("T=");
            double.TryParse(Console.ReadLine(), out  T);

            Console.WriteLine("Lambda=");
            double.TryParse(Console.ReadLine(), out  lambda);

            Console.WriteLine("Lambda(t): 1) 1/(t+1); 2)1/(t+1)^2; 3) 1/(e^(t+1)) 4) sin(t)+2, 5) cos(t+1)+1");
            int.TryParse(Console.ReadLine(), out index);
        }

        static void Check(List<double> x, string name)
        {
            if (x.Count >= 20)
                Console.WriteLine("Невозможно проверить "+name+" из-за переполнения при вычислении !");
            else
            {
                List<int> counts = new List<int>();
                for (int i = 0; i < x.Count; i++)
                    counts.Add(1);
                if (PuassonTest(x, counts))
                    Console.WriteLine(name+" тест на распределение пройден");
                else
                    Console.WriteLine(name+" тест на распределение не пройден");
            }
        }

        static void OldMain()
        {
            Input();

            var Uniform = Generator.CreateUniform(T, lambda);
            //var NonUniform = Generator.CreateNotUniform(T, lambda, lambdas[index - 1]);

            Console.WriteLine("Однородный");
            WriteList(Uniform);
            Console.WriteLine();


            //Console.WriteLine("Неоднородный");
            //WriteList(NonUniform);
            //Console.WriteLine();


            Check(Uniform, "Однородный");
            //Check(NonUniform, "НеОднородный");

        }

        static void Main(string[] args)
        {
            Input();

            var Uniform = Generator.CreateUniform(T, lambda);
            var NonUniform = Generator.CreateNotUniform(T, lambda, lambdas[index - 1]);

            Console.WriteLine("Однородный");
            WriteList(Uniform);
            Console.WriteLine();


            Console.WriteLine("Неоднородный");
            WriteList(NonUniform);
            Console.WriteLine();


            Check(Uniform, "Однородный");
            Check(NonUniform, "НеОднородный");


            Console.ReadKey();
        }

        static void WriteList<T>(List<T> list)
        {
            Console.WriteLine("Элементов " + list.Count);
            for (int i = 0; i < list.Count; i++)
                Console.Write(list[i] + "  ");
            Console.WriteLine();
        }

        public static bool PuassonTest(List<double> S, List<int> Sn)
        {
            int n = S.Count;
            double cnt = 0;
            double mid = 0;
            for (int i = 0; i < n; i++)
            {
                mid += S[i] * Sn[i];
                cnt += Sn[i];
            }
            mid /= cnt;
            //Console.WriteLine("mid=" + mid);
         
            var n_i = new List<double>();
            var n_i_old = new List<double>();
            List<int> i_list = new List<int>();
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                count += Sn[i];
                i_list.Add(i);
                if(count>=5)
                {
                    n_i_old.Add(count);
                    double sum = 0;
                    for (int j = 0; j < i_list.Count; j++)
                        sum += (Pn(i_list[j], mid) * cnt);
                    n_i.Add(sum);
                    //Console.WriteLine("n = " + count + "  n` = " + n_i[n_i.Count - 1]);
                    count = 0;
                    i_list.Clear();
                }
                
            }



        
            double hi_sq = 0;
            for (int i = 0; i < n_i.Count; i++)
            {
                hi_sq += Math.Pow(n_i_old[i] - n_i[i], 2) / n_i[i];
               // Console.WriteLine("+="+(Math.Pow(n_i_old[i] - n_i[i], 2) / n_i[i]));
            }


            //Console.WriteLine("hi_sq = " + hi_sq);

            double[] hi_sq_t = {3.8, 6.0, 7.8, 9.5, 11.1, 12.6, 14.1, 15.5, 16.9, 18.3, 19.7, 21, 22.4, 23.7, 25, 26.3, 27.6, 28.9, 30.1, 31.4,
            32.7, 33.9, 35.2, 36.4, 37.7, 38.9, 40.1, 41.3, 42.6, 42.8};

            int index = n_i.Count - 3;
            if (index - 1 < 0)
                index = 0;
            else
            if (index - 1 >= hi_sq_t.Length)
                index = hi_sq_t.Length - 1;


            return hi_sq < hi_sq_t[index];

        }

        public static double Pn(int i, double lambda) => (Math.Pow(lambda, i) * Math.Exp(-lambda)) / ((double)f(i));
        public static Int64 f(int i) => i <= 1 ? 1 : i * f(i - 1);

    }
}
