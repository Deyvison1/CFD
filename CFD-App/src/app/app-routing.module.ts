import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RendaComponent } from './renda/renda.component';
import { DividaComponent } from './divida/divida.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth/auth/auth.guard';

const routes: Routes = [
  { path: 'user', component: UserComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'renda', component: RendaComponent, canActivate: [AuthGuard] },
  { path: 'divida', component: DividaComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'divida', pathMatch: 'full' },
  { path: '**', redirectTo: 'divida', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
