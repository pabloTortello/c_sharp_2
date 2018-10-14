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

        private DateTime _Birthday;

        public DateTime Birthday
        {
            get => _Birthday;
            set => Set(ref _Birthday, value);
        }

        private string _Email;

        public string Email
        {
            get => _Email;
            set => Set(ref _Email, value);
        }

    }
}
