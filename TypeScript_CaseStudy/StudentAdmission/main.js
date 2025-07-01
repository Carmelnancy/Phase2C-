//Enum for Status
var AdmissionStatus;
(function (AdmissionStatus) {
    AdmissionStatus[AdmissionStatus["Eligible"] = 0] = "Eligible";
    AdmissionStatus[AdmissionStatus["NotEligible"] = 1] = "NotEligible";
})(AdmissionStatus || (AdmissionStatus = {}));
//Class to handle logic
var Admission = /** @class */ (function () {
    function Admission(student) {
        this.student = student;
    }
    Admission.prototype.checkEligibility = function () {
        if (this.student.age >= 18 && (this.student.course == "Angular" || this.student.course == "React")) {
            this.status = AdmissionStatus.Eligible;
        }
        else {
            this.status = AdmissionStatus.NotEligible;
        }
    };
    Admission.prototype.getGrade = function () {
        this.checkEligibility();
        var score = this.student.score;
        if (score >= 90)
            return "A";
        else if (score >= 75)
            return "B";
        else if (score >= 60)
            return "C";
        else
            return "D";
    };
    Admission.prototype.printProfile = function () {
        console.log("Student Name:".concat(this.student.name));
        console.log("Student Age:".concat(this.student.age));
        console.log("Student Course:".concat(this.student.course));
        console.log("Student Score:".concat(this.student.score));
        console.log("Student Grade:".concat(this.getGrade()));
        console.log("Admission Status:".concat(AdmissionStatus[this.status]));
    };
    return Admission;
}());
//To consume class
var student1 = {
    name: "Neha",
    age: 19,
    course: "Angular",
    score: 82
};
var student2 = {
    name: "Sachin",
    age: 20,
    course: "Vuejs",
    score: 90
};
var admission1 = new Admission(student1);
admission1.printProfile();
var admission2 = new Admission(student2);
admission2.printProfile();
