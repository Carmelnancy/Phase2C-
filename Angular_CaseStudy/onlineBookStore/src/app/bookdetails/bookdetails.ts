import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Book } from '../book.model';

@Component({
  selector: 'app-bookdetails',
  imports: [CommonModule],
  templateUrl: './bookdetails.html',
  styleUrl: './bookdetails.css'
})
export class BookdetailsComponent {
book:Book| undefined;

books:Book[]=[
   { id: 1, title: 'Angular Basics', author: 'John', price: 299, description: 'Intro to Angular' },
    { id: 2, title: 'TypeScript Mastery', author: 'Bob', price: 499, description: 'Into to TypeScript' },
    { id: 3, title: 'RxJS Deep Dive', author: 'Max', price: 399, description: 'Intro to RxJS' }
];

constructor(private route: ActivatedRoute) {
  const id = Number(this.route.snapshot.paramMap.get('id'));
  this.book = this.books.find(b => b.id === id);
  // this.route.params.subscribe(params =>{
  //   this.bookid=params['id'];

  // })
}
}
