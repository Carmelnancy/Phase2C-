using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StudentDAL.Domain;

namespace StudentDAL.Repository
{
    public class InMemoryStudentRepository : IStudentRepository

    {

        private readonly List<Student> _students = new List<Student>
        {
            new Student{ RollNo=1,Name="Abi",Email="abi@gmail" },
            new Student{RollNo=2,Name="Balu",Email="balu@gmail" }
        };

        public void Add(Student student)
        {
            _students.Add(student);
        }

        public void Delete(int rollNo)
        {
            var student = _students.Where(x => x.RollNo == rollNo).FirstOrDefault();
            _students.Remove(student);
        }

        public List<Student> GetAll()
        {
            return _students;
        }

        public Student GetByName(string name)
        {
            var student= _students.Where(s=>s.Name.Equals(name)).FirstOrDefault();
            return student;
        }

        public Student GetByRollNo(int rollNo)
        {
            var student = _students.Where(s => s.RollNo.Equals(rollNo)).FirstOrDefault();
            return student;
        }

        public void Update(Student student)
        {
            var existing = GetByRollNo(student.RollNo);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.Email = student.Email;
            }
        }

    }
}
