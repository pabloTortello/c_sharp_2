using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    abstract class Worker
    {
        protected Worker(string fio) { FIO = fio; }

        public string FIO { get; set; }
    }

    class Hourly : Worker
    {
        public int Stavka { set; get; }

        public Hourly(string fio, int stavka) : base(fio)
        {
            Stavka = stavka;
        }

        public void PrintZP()
        {
            Console.WriteLine($"{FIO}: среднемесячная зарплата составляет {20.8 * 8 * Stavka} рублей");
        }
    }

    class Salary : Worker
    {
        public int Oklad { set; get; }

        public Salary(string fio, int oklad) : base(fio)
        {
            Oklad = oklad;
        }

        public void PrintZP()
        {
            Console.WriteLine($"{FIO}: среднемесячная зарплата составляет {Oklad} рублей");
        }
    }





    
}
