using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            bool flag = true;

            Hourly pavel = new Hourly("Левченко Павел Николаевич", 100);
            pavel.PrintZP();

            Salary alexandr = new Salary("Кузьмин Александр Сергеевич", 20000);
            alexandr.PrintZP();

            StreamReader file = new StreamReader("Names.txt");
            Workmen[] workmen = new Workmen[10];
            for (int i = 0; i < 10; i++)
            {
                workmen[i] = new Workmen(file.ReadLine(), rnd.Next(100, 50000), flag);
                flag = !flag; //чтобы не заморачиваться: один - на окладе, один - почасово
            }

            foreach (Workmen worker in workmen)
            {
                Console.WriteLine($"{worker.FIO}: средняя зарплата {worker.SrednZP()} рублей");
            }

            Console.ReadKey();

        }
    }
}
