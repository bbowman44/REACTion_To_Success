import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Endpoints } from './pages/endpoints/endpoints';
import { Login } from './pages/login/login';

export const routes: Routes = [
    {path: '', component: Home},
    {path: 'endpoints', component: Endpoints},
    {path: 'login', component: Login}
];
