using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuassonGeneration
{
    static class Checker
    {
        /// <summary>
        /// Проверка последовательности времен на распределеине по Пуассону
        /// </summary>
        /// <param name="x">последовательность</param>
        /// <param name="name">Название для вывода(однродное/неоднородное)</param>
        public static void Check(List<double> x, string name, int T)
        {
            List<int> count_in_interval = new List<int>();

            for (int i = 0; i < T; i++)
                count_in_interval.Add(0);

            for (int i = 0; i < x.Count; i++)

                count_in_interval[(int)x[i]]++;

            List<int> counts = new List<int>();

            int max = count_in_interval.Max();
            for (int i = 0; i <= max; i++)

                counts.Add(0);

            for (int i = 0; i < count_in_interval.Count; i++)
            {
                counts[count_in_interval[i]]++;
                Console.WriteLine($"Элементов в промежутке {i}-{i + 1} = {count_in_interval[i]}");
            }

            Console.WriteLine();

            for (int i = 0; i < counts.Count; i++)
                Console.WriteLine($"Промежутков с {i} событиями {counts[i]}");

            if (PuassonTest(counts))
                Console.WriteLine(name + " тест на распределение Пуассона пройден");
            else
                Console.WriteLine(name + " тест на распределение Пуассона не пройден");
            //}
        }
        /// <summary>
        /// Проверка на соответствие распределению Пуассона на уровне значимости 0.05
        /// СМ Гмурман (~280 страница)
        /// </summary>
        /// <param name="S">Список значений xn</param>
        /// <param name="Sn">Список частот значений n[i]</param>
        /// <returns>true если распределеине Пуассона, иначе false</returns>

        public static bool PuassonTest(List<int> Sn)
        {
            //int n = S.Count;
            double cnt = 0;
            double mid = 0;
            for (int i = 0; i < Sn.Count; i++)
            {
                mid += i * Sn[i];
                cnt += Sn[i];
            }
            mid /= cnt;
            Console.WriteLine($"Среднее = {mid}");
            ///n[i] теоретическое
            var n_i = new List<double>();
            List<int> i_list = new List<int>();///список индексов для запоминания

            for (int i = 0; i < Sn.Count; i++)
                n_i.Add((Pn(i, mid) * cnt));


            double hi_sq = 0;
            for (int i = 0; i < n_i.Count; i++)
                hi_sq += Math.Pow(Sn[i] - n_i[i], 2) / n_i[i];


            //хи-кважрат для уровня значимости 0.05
            double[] hi_sq_t = {3.8, 6.0, 7.8, 9.5, 11.1, 12.6, 14.1, 15.5, 16.9, 18.3, 19.7, 21, 22.4, 23.7, 25, 26.3, 27.6, 28.9, 30.1, 31.4,
            32.7, 33.9, 35.2, 36.4, 37.7, 38.9, 40.1, 41.3, 42.6, 42.8};

            //индекс для хи-квадрат 
            int index = n_i.Count - 3;
            if (index - 1 < 0)
                index = 0;
            else
            if (index - 1 >= hi_sq_t.Length)
                index = hi_sq_t.Length - 1;


            return hi_sq < hi_sq_t[index];

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public static double Pn(int i, double lambda) => (Math.Pow(lambda, i) * Math.Exp(-lambda)) / ((double)f(i));
        /// <summary>
        /// Факториал
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Int64 f(int i) => i <= 1 ? 1 : i * f(i - 1);
    }
}
