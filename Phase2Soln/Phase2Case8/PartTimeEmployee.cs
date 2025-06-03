using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase2Case8
{
    public class PartTimeEmployee : Employee,IPartTimeEmployee
    {
        public double Basic { get; set; }
        public double Da { get; set; }
        public double NetSalary { get; set; }
        public PartTimeEmployee() { }

        public double CalculateSalary()
        {
            Da = 0.05 * Basic;
            NetSalary = Basic + Da;
            return NetSalary;
        }

        public string PrintEmployeeDetails(Employee employee)
        {
            return $"PTE => Code: {employee.EmpCode}, Name: {employee.EmpName}, Net Salary: {NetSalary}";
        }
    }

}
