using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentDAL.BussinessLogic;
using StudentDAL.Domain;
using StudentDAL.Repository;
using Moq;

namespace StudentTests
{
    [TestFixture]
    public class StudentServiceTest
    {

        private Mock<IStudentRepository> _mock;
        private StudentService _service;

        [SetUp]
        public void setUp()
        {
            _mock = new Mock<IStudentRepository>();
            _service=new StudentService(_mock.Object);
        }

        [Test]
        public void AddStudent_ShouldCallAdd()
        {
            var student = new Student { RollNo = 3, Name = "Alice", Email = "alice@example.com" };

            _service.AddStudent(student);

            _mock.Verify(r => r.Add(It.Is<Student>(s => s.Name == "Alice")), Times.Once());

            //var students = _service.GetAllStudents();
            //Assert.AreEqual(1, students.Count);
            //Assert.AreEqual("Alice", students[0].Name);
        }

        [Test]
        public void GetStudentByRollNo_ShouldReturnCorrectStudent()
        {
            var student = new Student { RollNo = 2, Name = "Bob", Email = "bob@example.com" };
            _service.AddStudent(student);

            var result = _service.GetByStudentRollNo(2);

            Assert.IsNotNull(result);
            Assert.AreEqual("Bob", result.Name);
        }

        [Test]
        public void UpdateStudent_ShouldChangeName()
        {
            var student = new Student { RollNo = 3, Name = "Charlie", Email = "charlie@example.com" };
            _service.AddStudent(student);

            student.Name = "Charles";
            _service.UpdateStudent(student);

            var updated = _service.GetByStudentRollNo(3);
            Assert.AreEqual("Charles", updated.Name);
        }

        [Test]
        public void DeleteStudent_ShouldRemoveStudent()
        {
            var student = new Student { RollNo = 4, Name = "David", Email = "david@example.com" };
            _service.AddStudent(student);

            _service.DeleteStudent(4);

            var result = _service.GetByStudentRollNo(4);
            Assert.IsNull(result);
        }
    }
}
