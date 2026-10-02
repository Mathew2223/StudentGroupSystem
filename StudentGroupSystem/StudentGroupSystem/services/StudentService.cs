using StudentGroupSystem_1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentGroupSystem_1.Services
{
    public class StudentService
    {
     
            private ObservableCollection<Student> _students = new ObservableCollection<Student>();

            public ObservableCollection<Student> Students
            {
                get { return _students; }
            }

            public string Validate(Student student)
            {
                if (string.IsNullOrWhiteSpace(student.FullName))
                    return "ФИО не может быть пустым";
                if (string.IsNullOrWhiteSpace(student.Group))
                    return "Группа не может быть пустой";
                if (student.AverageGrade < 2 || student.AverageGrade > 5)
                    return "Средний балл должен быть от 2 до 5";
                if (!Regex.IsMatch(student.Phone ?? "", @"^\+?[0-9\s\-]{7,}$"))
                    return "Неверный формат телефона";
                return null;
            }

            public bool AddStudent(Student student)
            {
                string error = Validate(student);
                if (error != null)
                    return false;
                _students.Add(student);
                return true;
            }

            public bool RemoveStudent(Student student)
            {
                if (student != null && _students.Contains(student))
                {
                    _students.Remove(student);
                    return true;
                }
                return false;
            }
        }
    }
