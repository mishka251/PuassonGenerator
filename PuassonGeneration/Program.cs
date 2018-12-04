﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuassonGeneration
{
    class Program
    {
        /// <summary>
        /// Массив функций - вариантов для генерации неоднородной
        /// </summary>
        static lambda_f[] lambdas =
             {
             (t) => 1 / (t + 1),
             (t) => 1 / Math.Pow((t + 1), 2),
             (t) => 1 / Math.Exp(t + t / 2),
             (t)=>Math.Sin(t)+1,
             (t)=>Math.Cos(t+1)+1
        };

        static double  lambda;
        static int T, functionNumber;

        /// <summary>
        /// Ввод параметров
        /// </summary>
        static void Input()
        {
            Console.WriteLine("T=");
            int.TryParse(Console.ReadLine(), out T);

            Console.WriteLine("Лямбда=");
            double.TryParse(Console.ReadLine().Replace('.', ','), out lambda);

            Console.WriteLine("Lambda(t) для неодноородного: 1) 1/(t+1); 2)1/(t+1)^2; 3) 1/(e^(t+1)) 4) sin(t)+1, 5) cos(t+1)+1");
            int.TryParse(Console.ReadLine(), out functionNumber);
        }
        /// <summary>
        /// Вывод списка
        /// </summary>
        /// <typeparam name="T">Тип элементов списка</typeparam>
        /// <param name="list">список</param>
        static void WriteList<T>(List<T> list)
        {
            Console.WriteLine("Элементов " + list.Count);
            for (int i = 0; i < list.Count; i++)
                Console.Write(list[i] + "  ");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Input();

            var Uniform = Generator.CreateUniform(T, lambda);
            var NonUniform = Generator.CreateNotUniform(T, lambda, lambdas[functionNumber - 1]);

            Console.WriteLine("Однородный");
            WriteList(Uniform);
            Console.WriteLine();
            

            Checker.Check(Uniform, "Однородный", T);
            Console.WriteLine("Нажмите для продллжения");
            Console.ReadKey();


            Console.WriteLine("Неоднородный");
            WriteList(NonUniform);
            Console.WriteLine();
            Checker.Check(NonUniform, "НеОднородный", T);

            Console.ReadKey();
        }
    }
}
