using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            dict = new Dictionary<int, int>();

            list = new List<int>(10);
            for (int i = 0; i < 10; i++)
            {
                list.Add(rnd.Next(1, 5));
                Console.Write(list[i] + " ");
            }

            Console.WriteLine();

            #region Вторая задача для целых чисел
            foreach (int num in list)
            {
                if (!dict.TryGetValue(num, out int result))
                    dict.Add(num, 0);
                dict[num]++;
            }

            Console.WriteLine("Без Linq");

            foreach (var item in dict)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
            #endregion


            #region Вторая задача используя Linq
            dict = new Dictionary<int, int>();

            foreach (int num in list)
            {
                if (!dict.TryGetValue(num, out int result))
                    //dict.Add(num, (from n in list
                    //               where n == num
                    //               select n).Count());
                    dict.Add(num, list.Where(n => n == num).Count());
            }

            Console.WriteLine("Используя Linq");

            foreach (var item in dict)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
            #endregion

            Console.ReadKey();
        }

        public static List<int> list;
        public static Dictionary<int, int> dict;
    }
}
