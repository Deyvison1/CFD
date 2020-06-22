import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RendaComponent } from './renda/renda.component';
import { DividaComponent } from './divida/divida.component';

const routes: Routes = [
  { path: 'user', component: UserComponent },
  { path: 'renda', component: RendaComponent },
  { path: 'divida', component: DividaComponent },
  { path: '', redirectTo: 'user', pathMatch: 'full' },
  { path: '**', redirectTo: 'user' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
