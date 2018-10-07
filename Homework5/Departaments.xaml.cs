using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Homework5
{
    /// <summary>
    /// Управление отделами
    /// </summary>
    public partial class Departaments : Window
    {
        private MainWindow main_window;
        private List<Departament> dep;
        private BinaryFormatter formatter = new BinaryFormatter();
        private FileStream fsDepartament = new FileStream("departaments.dat", FileMode.OpenOrCreate);

        public Departaments(List<Departament> departaments, MainWindow form)
        {
            main_window = form;
            dep = departaments;
            InitializeComponent();
            foreach (var item in dep)
            {
                DepartamentsList.Items.Add(item.NameDep);
            }
        }

        public void AddDep_Click(object sender, RoutedEventArgs e)
        {
            if (AddDepartamentsTextBox.Text == "")
                MessageBox.Show("Введите название отдела");
            else
            {
                dep.Add(new Departament(AddDepartamentsTextBox.Text));
                DepartamentsList.Items.Add(AddDepartamentsTextBox.Text);
                AddDepartamentsTextBox.Text = "";
                formatter.Serialize(fsDepartament, dep);
            }
                
        }

        private void DelDepartament_Click(object sender, RoutedEventArgs e)
        {
            if (DepartamentsList.SelectedItem != null)
            {
                int i = 0;
                foreach (var item in dep)
                {
                    if (item.NameDep == DepartamentsList.SelectedItem.ToString())
                    {
                        dep.Remove(item);
                        DepartamentsList.Items.RemoveAt(i);
                        break;
                    }
                    i++;
                }
                formatter.Serialize(fsDepartament, dep);
            }
        }

        private void CloseDepartaments_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        private void Window_Closing(object sender, EventArgs e)
        {
            fsDepartament.Close();
            //main_window.UpdateDepartamentsComboBox();
            //main_window.Update();
            
            //я никак не пойму, почему при апдейте, если раскоментить, SelectedValue возвращает null.
            //из-за этого не могу обновить отделы после закрытия формы управления ими
        }
    }

    
}
