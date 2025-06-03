namespace Phase2Case8
{
    public class Program
    {
        static void Main(string[] args)
        {
            Employee e = new Employee(5, "Abi", "abi@mail", "math", "Mumbai");

            FullTimeEmployee fte = new FullTimeEmployee
            {
                EmpCode = 1,
                EmpName = "John",
                Email = "john@example.com",
                DeptName = "Development",
                Location = "New York",
                Basic = 30000
                //Hra=120,
                //Da=299,
                //NetSalary=2000
            };
            Console.WriteLine(fte.CalculateSalary());
            Console.WriteLine(fte.PrintEmployeeDetails(fte));

            // Part-time employee
            PartTimeEmployee pte = new PartTimeEmployee
            {
                EmpCode = 2,
                EmpName = "Anna",
                Email = "anna@example.com",
                DeptName = "Support",
                Location = "Chicago",
                Basic = 15000
            };
            pte.CalculateSalary();
            Console.WriteLine(pte.PrintEmployeeDetails(pte));

            Console.ReadKey();
        }
    }
}
