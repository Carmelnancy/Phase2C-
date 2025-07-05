import { Component } from '@angular/core';
import { Book } from '../book.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-booklist',
  imports: [CommonModule,RouterModule],
  templateUrl: './booklist.html',
  styleUrl: './booklist.css'
})
export class BooklistComponent {
books:Book[]=[
   { id: 1, title: 'Angular Basics', author: 'John', price: 299, description: 'Intro to Angular' },
    { id: 2, title: 'TypeScript Mastery', author: 'Bob', price: 499, description: 'Into to TypeScript' },
    { id: 3, title: 'RxJS Deep Dive', author: 'Max', price: 399, description: 'Intro to RxJS' }
];
}
