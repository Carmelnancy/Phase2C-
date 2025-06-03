using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase2Case8
{
    public class Employee
    {
        public int EmpCode { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string DeptName { get; set; }
        public string Location { get; set; }

        public Employee() { }

        public Employee(int empCode, string empName, string email, string deptName, string location)
        {
            EmpCode = empCode;
            EmpName = empName;
            Email = email;
            DeptName = deptName;
            Location = location;
        }
    }
}
