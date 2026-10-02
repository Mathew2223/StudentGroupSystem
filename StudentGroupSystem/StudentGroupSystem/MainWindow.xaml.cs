using StudentGroupSystem_1.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudentGroupSystem_1.Services;

namespace StudentGroupSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StudentService _service = new StudentService();
        public MainWindow()
        {
            InitializeComponent();
            dgStudents.ItemsSource = _service.Students;

            _service.AddStudent(new Student
            {
                FullName = "Иванов Иван",
                Group = "ИСП-301",
                Phone = "+7 900 123-45-67",
                AverageGrade = 4.2
            });
            _service.AddStudent(new Student
            {
                FullName = "Петрова Анна",
                Group = "ИСП-301",
                Phone = "+7 900 234-56-78",
                AverageGrade = 4.8
            });
            _service.AddStudent(new Student
            {
                FullName = "Сидоров Пётр",
                Group = "ИСП-302",
                Phone = "+7 900 345-67-89",
                AverageGrade = 3.5
            });
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            double grade;
            if (!double.TryParse(txtGrade.Text, out grade))
            {
                lblStatus.Text = "Ошибка: балл должен быть числом";
                return;
            }

            Student student = new Student
            {
                FullName = txtFullName.Text.Trim(),
                Group = txtGroup.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                AverageGrade = grade
            };

            string error = _service.Validate(student);
            if (error != null)
            {
                lblStatus.Text = "Ошибка: " + error;
                return;
            }

            _service.AddStudent(student);
            lblStatus.Text = "";

            txtFullName.Text = "";
            txtGroup.Text = "";
            txtPhone.Text = "";
            txtGrade.Text = "";
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Student selected = dgStudents.SelectedItem as Student;
            if (selected == null)
            {
                lblStatus.Text = "Выберите студента для удаления";
                return;
            }

            _service.RemoveStudent(selected);
            lblStatus.Text = "";
        }
    }
}

