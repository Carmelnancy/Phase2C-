var CourseName;
(function (CourseName) {
    CourseName["Angular"] = "Angular";
    CourseName["NodeJS"] = "Node.js";
    CourseName["FullStack"] = "FullStack";
})(CourseName || (CourseName = {}));
var CourseEnrollement = /** @class */ (function () {
    function CourseEnrollement() {
        this.students = [];
    }
    CourseEnrollement.prototype.addStudent = function (student) {
        this.students.push(student);
    };
    CourseEnrollement.prototype.displayEnrollements = function () {
        for (var _i = 0, _a = this.students; _i < _a.length; _i++) {
            var student_1 = _a[_i];
            console.log("Student Name: ".concat(student_1.name));
            console.log("Age: ".concat(student_1.age));
            console.log("Course: ".concat(student_1.courseName));
            console.log("Knows HTML: ".concat(student_1.knowsHTML));
            console.log("Course Category: " + this.getCourseCategory(student_1));
            console.log("Enrollment Status: " + this.getEnrollmentStatus(student_1));
        }
    };
    CourseEnrollement.prototype.getCourseCategory = function (s) {
        if (s.courseName == "Angular")
            return "Front-End";
        else if (s.courseName == "Node.js")
            return "Back-End";
        else if (s.courseName == "FullStack")
            return "Full-Stack";
        else
            return "Unknown";
    };
    CourseEnrollement.prototype.getEnrollmentStatus = function (s) {
        if (s.age < 18) {
            return "Not Eligible";
        }
        if (s.courseName === CourseName.Angular && !s.knowsHTML) {
            return "Not Eligible";
        }
        return "Eligible";
    };
    return CourseEnrollement;
}());
var system = new CourseEnrollement();
var student = [
    { name: "Sneha", age: 20, courseName: CourseName.Angular, knowsHTML: true },
    { name: "Riya", age: 22, courseName: CourseName.Angular, knowsHTML: false },
    { name: "Karan", age: 17, courseName: CourseName.NodeJS, knowsHTML: false },
    { name: "Aman", age: 25, courseName: CourseName.FullStack, knowsHTML: true }
];
system.addStudent(student[0]);
system.addStudent(student[1]);
system.addStudent(student[2]);
system.addStudent(student[3]);
system.displayEnrollements();
