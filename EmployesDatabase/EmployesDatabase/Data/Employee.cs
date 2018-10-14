using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployesDatabase.Data
{
    class Employee : ViewModel
    {
        private int _Id;

        public int Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }

        private string _Name;

        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        private string _Birthday;

        public string Birthday
        {
            get => _Birthday;
            set => Set(ref _Birthday, value);
        }

        private string _Phone;

        public string Phone
        {
            get => _Phone;
            set => Set(ref _Phone, value);
        }

        private string _Departamen;

        public string Departament
        {
            get => _Departamen;
            set => Set(ref _Departamen, value);
        }

    }
}
