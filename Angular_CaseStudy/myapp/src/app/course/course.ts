import { Component } from '@angular/core';
import { Course } from './models/course.model';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-course',
  imports: [CommonModule],
  templateUrl: './course.html',
  styleUrl: './course.css'
})
export class CourseComponent {
course:Course[]=[
  {id:1,title:"Angular fundamentals",instructor:"John Doe",startDate:new Date(2025,0o7,15),price:4999,description:"Learn the basics of Angular inclu..."},
  {id:2,title:"React basics",instructor:"Jane Smith",startDate:new Date(2025,0o7,20),price:3499,description:"Build interactive UIs with Reac..."}
];
}
