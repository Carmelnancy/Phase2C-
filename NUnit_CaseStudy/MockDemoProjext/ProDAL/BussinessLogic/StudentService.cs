using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentDAL.Domain;
using StudentDAL.Repository;

namespace StudentDAL.BussinessLogic
{
    public class StudentService

    {

        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)

        {

            _repository = repository;

        }

        //Rest of code will go here 
        public void AddStudent(Student student)
        {
            if (student != null)
            {
                _repository.Add(student);
            }
        }

        public void DeleteStudent(int rollNo)
        {
            if (rollNo != 0)
            {
                _repository.Delete(rollNo);
            }
        }

        public List<Student> GetAllStudents()
        {
            return _repository.GetAll();
        }

        public Student GetByStudentName(string name)
        {
            if (name != null)
            {
                var student = _repository.GetByName(name);
                return student;
            }
            else
            {
                return null;
            }
        }

        public Student GetByStudentRollNo(int rollNo)
        {
            if (rollNo != 0)
            {
                var student=_repository.GetByRollNo(rollNo);
                return student;
            }
            else
            {
                return null;
            }
        }

        public void UpdateStudent(Student student)
        {
            _repository.Update(student);
        }


    }
}
