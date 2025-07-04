enum CourseName {
    Angular = "Angular",
    NodeJS = "Node.js",
    FullStack = "FullStack"
}

interface Student{
    name: string;
    age: number;
    courseName: CourseName;
    knowsHTML: boolean;
}

class CourseEnrollement{

    private students: Student[];

    constructor() {
        this.students = [];
    }

    addStudent(student:Student):void{
        this.students.push(student);
    }

    displayEnrollements():void{
        for (let student of this.students) {
            console.log(`Student Name: ${student.name}`);
        console.log(`Age: ${student.age}`);
        console.log(`Course: ${student.courseName}`);
        console.log(`Knows HTML: ${student.knowsHTML}`);
        console.log("Course Category: "+this.getCourseCategory(student));
        console.log("Enrollment Status: "+this.getEnrollmentStatus(student));
        }
    }

    getCourseCategory(s:Student):string{
        if(s.courseName=="Angular") return "Front-End";
        else if(s.courseName=="Node.js") return "Back-End";
        else if(s.courseName=="FullStack") return "Full-Stack";
        else return "Unknown";
    }

     getEnrollmentStatus(s:Student): string {
        if (s.age < 18) {
            return "Not Eligible";
        }
        if (s.courseName === CourseName.Angular && !s.knowsHTML) {
            return "Not Eligible";
        }
        return "Eligible";
    }

}

const system=new CourseEnrollement();

const student:Student[]=[
    {name:"Sneha",age:20,courseName:CourseName.Angular,knowsHTML:true},
    {name:"Riya",age:22,courseName:CourseName.Angular,knowsHTML:false},
    {name:"Karan",age:17,courseName:CourseName.NodeJS,knowsHTML:false},
    {name:"Aman",age:25,courseName:CourseName.FullStack,knowsHTML:true}
];

 system.addStudent(student[0]);
 system.addStudent(student[1]);
 system.addStudent(student[2]);
 system.addStudent(student[3]);

 system.displayEnrollements();