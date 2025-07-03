import { Component } from '@angular/core';
import { StudentFeedback } from './models/studentFeedback.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-student-feedback',
  imports: [CommonModule,FormsModule],
  templateUrl: './student-feedback.html',
  styleUrl: './student-feedback.css'
})
export class StudentFeedbackComponent {
feedback:StudentFeedback={
  name:"John Doe",
  course:"Angular",
  rating:4,
  comments:"Very helpful and beginner-friendly course."
}
}
