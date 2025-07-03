import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Course } from './course/models/course.model';
import { CourseComponent } from './course/course';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,CourseComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'myapp';
}
