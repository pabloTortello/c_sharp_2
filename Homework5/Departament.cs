using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    [Serializable]
    public class Departament
    {
        public string NameDep { set; get; }

        public Departament(string name)
        {
            NameDep = name;
        }
    }
}
