using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EmployesDatabase.Data;

namespace EmployesDatabase
{
    class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<Employee> Employes { get; } = new ObservableCollection<Employee>
        {
            //new Employee{ Id = 0, Name = "Ivanov", Birthday = DateTime.Today, Email = "qwe@asd.ru" },
            //new Employee{ Id = 1, Name = "Petrov", Birthday = DateTime.Today, Email = "123@asd.ru" },
            //new Employee{ Id = 2, Name = "Sidorov", Birthday = DateTime.Today, Email = "ZXC@asd.ru" },
        };

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand EditCommand { get; }

        public MainWindowViewModel()
        {
            AddCommand = new LambdaCommand(OnAddCommandExecuted);
            RemoveCommand = new LambdaCommand(OnRemoveCommandExecuted);
            EditCommand = new LambdaCommand(OnEditCommandExecuted);
        }

        private void OnEditCommandExecuted(object obj)
        {
            MessageBox.Show("Команда редактирования");
        }

        private void OnRemoveCommandExecuted(object obj)
        {
            MessageBox.Show("Команда удаления");
        }

        private void OnAddCommandExecuted(object obj)
        {
            MessageBox.Show("Команда добавления");
        }
    }
}
