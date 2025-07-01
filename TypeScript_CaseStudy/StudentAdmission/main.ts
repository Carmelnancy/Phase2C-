//Enum for Status
enum AdmissionStatus{
    Eligible,
    NotEligible
}

//Interface for Student Model
interface Student{
    name:string;
    age:number;
    course:string;
    score:number;
}

//Class to handle logic
class Admission{

    private status:AdmissionStatus;
    constructor(private student:Student){

    }

    checkEligibility():void{
        if(this.student.age>=18 && (this.student.course=="Angular" ||this.student.course=="React")){
            this.status=AdmissionStatus.Eligible;
        }else{
            this.status=AdmissionStatus.NotEligible;
        }
    }

    getGrade():string{
        this.checkEligibility();
        let score=this.student.score;
        if(score>=90) return "A";
        else if(score>=75) return "B";
        else if(score>=60) return "C";
        else return "D";
    }

    printProfile():void{
        console.log(`Student Name:${this.student.name}`);
         console.log(`Student Age:${this.student.age}`);
          console.log(`Student Course:${this.student.course}`);
           console.log(`Student Score:${this.student.score}`);
            console.log(`Student Grade:${this.getGrade()}`);
             console.log(`Admission Status:${AdmissionStatus[this.status]}`);
    }

}

//To consume class
let student1:Student={
    name:"Neha",
    age:19,
    course:"Angular",
    score:82
}

let student2:Student={
    name:"Sachin",
    age:20,
    course:"Vuejs",
    score:90
}

let admission1=new Admission(student1);
admission1.printProfile();

let admission2=new Admission(student2);
admission2.printProfile();