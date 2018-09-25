using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    class Workmen : Worker
    {
        public int Stavka { set; get; }
        public bool Flag { set; get; } //если тру - на окладе, нет - почасово
        public Workmen(string fio, int stavka, bool flag) : base(fio)
        {
            FIO = fio;
            Stavka = stavka;
            Flag = flag;
        }
        public double SrednZP()
        {
            if (Flag) return Stavka;
            else return 20.8 * 8 * Stavka;
        }
    }
}
