using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EmployesDatabase.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployesDatabase
{
    class MainWindowViewModel : ViewModel
    {
        private const string str_conection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EmployesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ObservableCollection<Employee> Employes { get; } = new ObservableCollection<Employee>();

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand EditCommand { get; }

        public MainWindowViewModel()
        {
            AddCommand = new LambdaCommand(OnAddCommandExecuted);
            RemoveCommand = new LambdaCommand(OnRemoveCommandExecuted);
            EditCommand = new LambdaCommand(OnEditCommandExecuted);

            string sql = "SELECT Employes.ID, Employes.Name, Employes.Birthday, Employes.Phone, Departaments.Name " +
                "FROM Employes, Departaments WHERE Departaments.Id = Employes.ID_Departament";
            using (var connection = new SqlConnection(str_conection))
            {
                connection.Open();

                var command = new SqlCommand(sql, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employes.Add(new Employee
                        {
                            Id = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            Birthday = (string)reader["Birthday"],
                            Phone = (string)reader["Phone"],
                            Departament = (string)reader.GetValue(4)
                        });
                    }
                }
            }
        }

        private void OnClickAddWindow()
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.Show();
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
            
        }
    }
}
