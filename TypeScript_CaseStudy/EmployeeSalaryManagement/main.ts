enum Department{
    HR="HR",
    IT="IT",
    Sales="Sales"
}

interface Employee{
    id: number;
    name: string;
    age: number;
    department: Department ;
    salary: number;
}



class System{
    

    constructor(private employee : Employee){

    }
    CalculateNetSalary(department:Department,salary:number):number{
        if(department === Department.HR){
        return salary+(10/100)*salary;
        }
        else if(department === Department.IT){
            return salary+(15/100)*salary;
        }
        else {
            return salary+(12/100)*salary; 
         }
        
    }

    CategorizeEmployee():string{
        if(this.employee.salary>=80000){
            return "High Earner";
        }
        else if(this.employee.salary>=50000 && this.employee.salary<80000){
            return "Medium Earner";
        }
        else{
            return "Low Earner";
        }
    }

    EmployeeReport():void{
        
            console.log("Employee Name: "+this.employee.name);
            console.log("Age: "+this.employee.age);
            console.log("Department: "+this.employee.department);
            console.log("Base Salary: "+this.employee.salary);
            console.log("Net Salary(with bonus): "+this.CalculateNetSalary(this.employee.department,this.employee.salary));
            console.log("Category: "+this.CategorizeEmployee());
      
    }

}


let emp1:Employee={
    id:1,
    name:'Ravi',
    age:28,
    department:Department.IT,
    salary:60000
}

let emp2:Employee={
    id:2,
    name:'Priya',
    age:32,
    department:Department.HR,
    salary:48000
}

let emp3:Employee={
    id:3,
    name:'Arjun',
    age:26,
    department:Department.Sales,
    salary:85000
}

var sys1=new System(emp1);
sys1.EmployeeReport();

var sys2=new System(emp2);
sys2.EmployeeReport();

var sys3=new System(emp3);
sys3.EmployeeReport();


