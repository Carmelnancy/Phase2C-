import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StudentFeedback } from './student-feedback/models/studentFeedback.model';
import { StudentFeedbackComponent } from './student-feedback/student-feedback';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,StudentFeedbackComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'feedbackProject';
}
