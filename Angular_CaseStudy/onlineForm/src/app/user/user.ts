import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { User } from './models/user.model';

@Component({
  selector: 'app-user',
  imports: [FormsModule,CommonModule],
  templateUrl: './user.html',
  styleUrl: './user.css'
})
export class UserComponent {
user:User={
  name:'',
  email:'',
  gender:'',
  country:'',
  agree:false
};
submitted=false;
onSubmit(form:any):void{
  if(form.valid){
    this.submitted=true;
    console.log('Form Submitted',this.user);
  }
  else{
    console.log('Form Not Valid');
  }
}
}
