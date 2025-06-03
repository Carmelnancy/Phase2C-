using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase2Case5
{
    internal class Student
    {
        public int id { get; set; }
        public string Name { get; set; }

        public Student() { }
        public Student(int id, string name)
        {
            this.id = id;
            this.Name = name;
        }
    }
}
