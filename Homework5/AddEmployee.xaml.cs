using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Homework5
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private bool flag; //false - вносим нового сотрудника, true - редактируем существующего
        private Dictionary<int, Employee> employees;
        private BinaryFormatter formatter = new BinaryFormatter();
        private FileStream fsEmployees = new FileStream("persons.dat", FileMode.OpenOrCreate);
        private MainWindow main_form;

        public AddEmployee(Dictionary<int, Employee> employees, List<Departament> departaments, MainWindow form)
        {
            InitializeComponent();
            this.employees = employees;
            foreach (var item in departaments)
            {
                DepartamentComboBox.Items.Add(item.NameDep);
            }
            flag = false;
            main_form = form;
        }

        public AddEmployee(Dictionary<int, Employee> employees, List<Departament> departaments,
            int id, MainWindow form)
        {
            InitializeComponent();
            this.employees = employees;
            foreach (var item in departaments)
            {
                DepartamentComboBox.Items.Add(item.NameDep);
            }

            IDTextBox.Text = id.ToString();
            LastNameTextBox.Text = employees[id].Last_name;
            NameTextBox.Text = employees[id].Name;
            MiddleNameTextBox.Text = employees[id].Middle_name;
            DR.Text = employees[id].DR;
            DoljnostTextBox.Text = employees[id].Doljnost;
            DepartamentComboBox.Text = employees[id].Department;
            flag = true;
            main_form = form;
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IDTextBox.Text, out int id))
                MessageBox.Show("Индвидуальный номер сотрудника не введен либо введен не верно!");
            else
            {
                if (employees.ContainsKey(id) & !flag)
                    MessageBox.Show("Сотрудник с данным номер уже существует!");
                else
                {
                    if (LastNameTextBox.Text == null ||
                        NameTextBox.Text == null ||
                        MiddleNameTextBox.Text == null ||
                        DR.Text == null ||
                        DoljnostTextBox.Text == null ||
                        DepartamentComboBox.Text == null)
                        MessageBox.Show("Введены не все данные!");
                    else
                    {
                        if (flag)
                            employees.Remove(id);
                        employees.Add(id, new Employee(id, LastNameTextBox.Text, NameTextBox.Text, MiddleNameTextBox.Text,
                                DR.Text, DoljnostTextBox.Text, DepartamentComboBox.Text));
                        formatter.Serialize(fsEmployees, employees);
                        fsEmployees.Close();

                        main_form.Load();
                        main_form.Update();
                        
                        Close();
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
