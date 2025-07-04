import { Component } from '@angular/core';
import { Student } from './models/student.model';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-student',
  imports: [CommonModule],
  templateUrl: './student.html',
  styleUrl: './student.css'
})
export class StudentComponent {

  students:Student[]=[
    {name:"John",marks:90},
    {name:"Peter",marks:41},
    {name:"Rex",marks:89}
  ]

}
