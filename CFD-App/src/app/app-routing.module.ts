import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RendaComponent } from './renda/renda.component';
import { DividaComponent } from './divida/divida.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: 'user', component: UserComponent },
  { path: 'login', component: LoginComponent },
  { path: 'renda', component: RendaComponent },
  { path: 'divida', component: DividaComponent },
  { path: '', redirectTo: 'divida', pathMatch: 'full' },
  { path: '**', redirectTo: 'divida' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
