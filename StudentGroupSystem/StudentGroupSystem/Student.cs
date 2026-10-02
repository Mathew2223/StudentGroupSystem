using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGroupSystem
{
    internal class Student
    {
        public string FullName { get; set; }
        public List<int> Grades { get; set; } = new List<int>();

        public double Average => Grades.Count > 0 ? Grades.Average() : 0;
    }
}
