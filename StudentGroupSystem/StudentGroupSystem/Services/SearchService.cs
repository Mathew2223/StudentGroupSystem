using System;
using System.Collections.Generic;
using System.Linq;
using StudentGroupSystem.Models;

namespace StudentGroupSystem.Services
{
    public class SearchService
    {
        // Поиск студентов по ФИО и по группе
        public List<Student> SearchStudents(List<Student> students, string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return students; // Если запрос пустой, возвращаем всех
            }

            return students
                .Where(s => (s.FullName != null && s.FullName.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (s.Group != null && s.Group.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();
        }

        // Проверка: найден ли кто-нибудь
        public bool IsStudentFound(List<Student> searchResults)
        {
            return searchResults != null && searchResults.Count > 0;
        }



        // Очистка / сброс результатов поиска """третий комит"""


        public List<Student> ClearSearch(List<Student> originalList)
        {
            return originalList;
        }
    }
}