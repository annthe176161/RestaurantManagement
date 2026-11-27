import { Routes } from '@angular/router';
import { CustomerLayoutComponent } from './layout/customer-layout/customer-layout.component';
import { HomeComponent } from './features/home/home.component';
import { NewsfeedPageComponent } from './features/newsfeed/newsfeed-page.component';

export const routes: Routes = [
  {
    path: '',
    component: CustomerLayoutComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'newsfeed', component: NewsfeedPageComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];
