using StudentGroupSystem_1.Models;
using StudentGroupSystem_1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace StudentGroupSystem_1.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly StudentService _studentService;
        private Student _newStudent;
        private string _errorMessage;
        private Student _selectedStudent;

        public StudentViewModel()
        {
            _studentService = new StudentService();
            _newStudent = new Student();

            // Тестовые данные
            _studentService.AddStudent(new Student
            {
                FullName = "Иванов Иван Иванович",
                Group = "ИСП-301",
                Phone = "+7 900 123-45-67",
                AverageGrade = 4.2
            });
            _studentService.AddStudent(new Student
            {
                FullName = "Петрова Анна Сергеевна",
                Group = "ИСП-301",
                Phone = "+7 900 234-56-78",
                AverageGrade = 4.8
            });
            _studentService.AddStudent(new Student
            {
                FullName = "Сидоров Пётр Алексеевич",
                Group = "ИСП-302",
                Phone = "+7 900 345-67-89",
                AverageGrade = 3.5
            });
        }

        // --- Свойства ---

        // Возвращаем коллекцию напрямую из сервиса — без сеттера
        public ObservableCollection<Student> Students
        {
            get { return _studentService.Students; }
        }

        public Student NewStudent
        {
            get { return _newStudent; }
            set
            {
                _newStudent = value;
                OnPropertyChanged("NewStudent");
            }
        }

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        // --- Команды ---

        private RelayCommand _addStudentCommand;
        public RelayCommand AddStudentCommand
        {
            get
            {
                if (_addStudentCommand == null)
                    _addStudentCommand = new RelayCommand(ExecuteAddStudent, CanExecuteAddStudent);
                return _addStudentCommand;
            }
        }

        private RelayCommand _removeStudentCommand;
        public RelayCommand RemoveStudentCommand
        {
            get
            {
                if (_removeStudentCommand == null)
                    _removeStudentCommand = new RelayCommand(ExecuteRemoveStudent, CanExecuteRemoveStudent);
                return _removeStudentCommand;
            }
        }

        // --- Логика команд ---

        private bool CanExecuteAddStudent(object parameter)
        {
            return true;
        }

        private void ExecuteAddStudent(object parameter)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(_newStudent.FullName))
            {
                ErrorMessage = "Ошибка! ФИО не может быть пустым.";
                return;
            }
            if (string.IsNullOrWhiteSpace(_newStudent.Group))
            {
                ErrorMessage = "Ошибка! Группа не может быть пустой.";
                return;
            }
            if (_newStudent.AverageGrade < 2 || _newStudent.AverageGrade > 5)
            {
                ErrorMessage = "Ошибка! Средний балл должен быть в диапазоне от 2 до 5.";
                return;
            }
            if (!IsValidPhone(_newStudent.Phone))
            {
                ErrorMessage = "Ошибка! Неверный формат телефона.";
                return;
            }

            // Добавление через сервис
            _studentService.AddStudent(_newStudent);
            ErrorMessage = "";

            // Очистка формы
            _newStudent = new Student();
            OnPropertyChanged("NewStudent");
        }

        private bool CanExecuteRemoveStudent(object parameter)
        {
            return _selectedStudent != null;
        }

        private void ExecuteRemoveStudent(object parameter)
        {
            if (_selectedStudent != null)
            {
                _studentService.RemoveStudent(_selectedStudent);
                ErrorMessage = "";
            }
        }

        // --- Вспомогательные методы ---

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;
            return Regex.IsMatch(phone, @"^\+?[0-9\s\-]{7,}$");
        }

        // --- INotifyPropertyChanged ---

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}