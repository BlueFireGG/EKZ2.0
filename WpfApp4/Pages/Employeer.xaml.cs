using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp4.Class;
using WpfApp4.DB;

namespace WpfApp4.Pages
{
    /// <summary>
    /// Логика взаимодействия для Employeer.xaml
    /// </summary>
    public partial class Employeer : Page
    {
        private EkzEntities EkzEntities;
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Position> Positions { get; set; }
        private Employee selectedEmployee = null;
        private string SearchText;
        public Employeer()
        {
            InitializeComponent();
            EkzEntities= new EkzEntities();
            Employees = new ObservableCollection<Employee>(EkzEntities.Employees.ToList());
            Positions = new ObservableCollection<Position>(EkzEntities.Positions.ToList());
            Departments = new ObservableCollection<Department>(EkzEntities.Departments.ToList());
            ListViewName.ItemsSource = Employees;
            DepartmentAdd.ItemsSource = Departments;
            PositionAdd.ItemsSource = Positions;
            DepartmentAdd.SelectedIndex= 0;
            PositionAdd.SelectedIndex= 0;
            DataContext = this;
        }
        private void CreateNewEmploy(object sender, RoutedEventArgs e)
        {
            selectedEmployee = new Employee();
            spEmployee.DataContext = selectedEmployee;
            EkzEntities.Employees.Add(selectedEmployee);
            Employees.Add(selectedEmployee);
            ListViewName.SelectedItem = selectedEmployee;
            SaveBut.IsEnabled = true;
        }
        private void EmployeeChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = ListViewName.SelectedItem as Employee;
            spEmployee.DataContext = selectedEmployee;
            if (selectedEmployee != null)
            {
                SaveBut.IsEnabled = true;
            }
            else
            {
                SaveBut.IsEnabled = false;
            }
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            try
            {
                EkzEntities.SaveChanges();
                MessageBox.Show("Сохранен");
            }
            catch(Exception exception)
            { MessageBox.Show("Упс..."+exception.Message); }
        }

        private void DelEmployee(object sender, RoutedEventArgs e)
        {
            Employee employee =ListViewName.SelectedItem as Employee;
            EkzEntities.Employees.Remove(selectedEmployee);
            Employees.Remove(selectedEmployee);
            EkzEntities.SaveChanges();
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            Search(SearchBox.Text.Trim());
        }
        private void Search(string substring)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ListViewName.ItemsSource);
            if (view == null) return;
            view.Filter = new Predicate<object>(obj =>
            {
                bool IsView = ((Employee)obj).FirstName.ToLower().Contains(substring.ToLower());
                return IsView;
            });

        }
    }
}
