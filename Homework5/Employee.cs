using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Homework5
{
    [Serializable]
    public class Employee
    {
        public int ID { set; get; }
        public string Last_name { set; get; }
        public string Name { set; get; }
        public string Middle_name { set; get; }
        public string DR { set; get; }
        public string Doljnost { set; get; }
        public string Department { set; get; }

        public Employee(int id, string last_name, string name, string middle_name, string dr, string doljnost, string departament)
        {
            ID = id;
            Last_name = last_name;
            Name = name;
            Middle_name = middle_name;
            DR = dr;
            Doljnost = doljnost;
            Department = departament;
        }
    }

    
}
