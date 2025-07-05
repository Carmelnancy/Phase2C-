import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection, Component } from '@angular/core';
import { provideRouter,Routes } from '@angular/router';
import { HomeComponent } from './home/home';
import { BooklistComponent } from './booklist/booklist';
import { BookdetailsComponent } from './bookdetails/bookdetails';

import { routes } from './app.routes';

const myroute:Routes=[
  {path:'',component:HomeComponent},
  {path:'books',component:BooklistComponent},
  {path:'books/:id',component:BookdetailsComponent}
]

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(myroute)
  ]
};
