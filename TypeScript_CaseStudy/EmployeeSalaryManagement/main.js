var Department;
(function (Department) {
    Department["HR"] = "HR";
    Department["IT"] = "IT";
    Department["Sales"] = "Sales";
})(Department || (Department = {}));
var System = /** @class */ (function () {
    function System(employee) {
        this.employee = employee;
    }
    System.prototype.CalculateNetSalary = function (department, salary) {
        if (department === Department.HR) {
            return salary + (10 / 100) * salary;
        }
        else if (department === Department.IT) {
            return salary + (15 / 100) * salary;
        }
        else {
            return salary + (12 / 100) * salary;
        }
    };
    System.prototype.CategorizeEmployee = function () {
        if (this.employee.salary >= 80000) {
            return "High Earner";
        }
        else if (this.employee.salary >= 50000 && this.employee.salary < 80000) {
            return "Medium Earner";
        }
        else {
            return "Low Earner";
        }
    };
    System.prototype.EmployeeReport = function () {
        console.log("Employee Name: " + this.employee.name);
        console.log("Age: " + this.employee.age);
        console.log("Department: " + this.employee.department);
        console.log("Base Salary: " + this.employee.salary);
        console.log("Net Salary(with bonus): " + this.CalculateNetSalary(this.employee.department, this.employee.salary));
        console.log("Category: " + this.CategorizeEmployee());
    };
    return System;
}());
var emp1 = {
    id: 1,
    name: 'Ravi',
    age: 28,
    department: Department.IT,
    salary: 60000
};
var emp2 = {
    id: 2,
    name: 'Priya',
    age: 32,
    department: Department.HR,
    salary: 48000
};
var emp3 = {
    id: 3,
    name: 'Arjun',
    age: 26,
    department: Department.Sales,
    salary: 85000
};
var sys1 = new System(emp1);
sys1.EmployeeReport();
var sys2 = new System(emp2);
sys2.EmployeeReport();
var sys3 = new System(emp3);
sys3.EmployeeReport();
