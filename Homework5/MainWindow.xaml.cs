using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Homework5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Departament> departaments; //коллекция отделов
        private Dictionary<int, Employee> employeesDict; //коллекция сотрудников с доступом по индивидуальному номеру (id)
        private Dictionary<int, int> listbox_id = new Dictionary<int, int>(); //словарь для сопоставления позиции в листбоксе и id
        private BinaryFormatter formatter = new BinaryFormatter(); //для сериализации
        private FileStream fsEmployees;
        private FileStream fsDepartament;

        public MainWindow()
        {
            Load(); 

            InitializeComponent();

            UpdateDepartamentsComboBox();
            Update();
        }

        /// <summary>
        /// Загрузка данных по отделам и персоналу
        /// </summary>
        public void Load()
        {
            fsDepartament = new FileStream("departaments.dat", FileMode.OpenOrCreate);
            fsEmployees = new FileStream("persons.dat", FileMode.OpenOrCreate);
            departaments = null;
            employeesDict = null;

            try
            {
                departaments = (List<Departament>)formatter.Deserialize(fsDepartament);
            }
            catch (Exception)
            {

                departaments = new List<Departament>();
            }

            try
            {
                employeesDict = (Dictionary<int, Employee>)formatter.Deserialize(fsEmployees);
            }
            catch (Exception)
            {

                employeesDict = new Dictionary<int, Employee>();
            }

            fsEmployees.Close();
            fsDepartament.Close();
        }

        /// <summary>
        /// Обновление и отображение отделов
        /// </summary>
        public void UpdateDepartamentsComboBox()
        {
            DepartamentsComboBox.Items.Clear();
            DepartamentsComboBox.Items.Add("Все сотрудники");
            
            foreach (var item in departaments)
            {
                DepartamentsComboBox.Items.Add(item.NameDep);
            }
            
            DepartamentsComboBox.Text = "Все сотрудники";
            DepartamentsComboBox.SelectedValue = "Все сотрудники";
        }

        /// <summary>
        /// Обновление и отображение сотрудников в соответствии с выбраным отделом
        /// </summary>
        public void Update()
        {
            EmployeesList.Items.Clear();
            listbox_id.Clear();
            
            foreach (var item in employeesDict)
            {
                if (DepartamentsComboBox.SelectedValue.ToString() == "Все сотрудники" || 
                    item.Value.Department == DepartamentsComboBox.SelectedValue.ToString())
                {
                    EmployeesList.Items.Add($"{item.Value.Last_name} {item.Value.Name[0]}.{item.Value.Middle_name[0]}.");
                    listbox_id.Add(EmployeesList.Items.Count - 1, item.Value.ID);
                }
            }
        }

        /// <summary>
        /// Управление отделами
        /// </summary>
        private void MenuItemDep_Click(object sender, RoutedEventArgs e)
        {
            Departaments dep = new Departaments(departaments, this);
            dep.Show();
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee(employeesDict, departaments, this);
            addEmployee.AddButton.Content = "Добавить";
            addEmployee.Show();
        }

        /// <summary>
        /// Редактировать сотрудника
        /// </summary>
        private void ChangeEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesList.SelectedItem != null)
            {
                AddEmployee addEmployee = new AddEmployee(employeesDict, departaments, listbox_id[EmployeesList.SelectedIndex], this);
                addEmployee.AddButton.Content = "Изменить";
                addEmployee.Show();
            }
            else
                MessageBox.Show("Сперва выберите сотрудника");
            
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        private void DelEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesList.SelectedItem != null)
            {
                employeesDict.Remove(listbox_id[EmployeesList.SelectedIndex]);
                fsEmployees = new FileStream("persons.dat", FileMode.OpenOrCreate);
                formatter.Serialize(fsEmployees, employeesDict);
                fsEmployees.Close();
                Load();
                Update();
            }
            else
                MessageBox.Show("Сперва выберите сотрудника");
        }

        /// <summary>
        /// Отображение данных сотрудника
        /// </summary>
        private void EmployeesList_MouseDown(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Employee pers = employeesDict[listbox_id[EmployeesList.SelectedIndex]];
                LastNameText.Text = pers.Last_name;
                NameText.Text = pers.Name;
                MiddleNameText.Text = pers.Middle_name;
                DRText.Text = pers.DR;
                DoljnostText.Text = pers.Doljnost;
                DepartamentText.Text = pers.Department;
            }
            catch (Exception)
            {
                LastNameText.Text = "";
                NameText.Text = "";
                MiddleNameText.Text = "";
                DRText.Text = "";
                DoljnostText.Text = "";
                DepartamentText.Text = "";
            }
            
            
        }

        /// <summary>
        /// Обновление списка сотрудников сотрудника
        /// </summary>
        private void DepartamentsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        
    }
}
