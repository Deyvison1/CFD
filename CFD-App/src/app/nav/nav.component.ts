import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(
    public router: Router,
    private authService: AuthService,
    private toastr: ToastrService
  ) {  }

  ngOnInit() {
  }

  logado() {
    return !this.authService.logado();
  }

  sair() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
  abrirMenu() {
    document.getElementById('mySidenav').style.width = '173px';
  }
  fecharMenu() {
    document.getElementById('mySidenav').style.width = '0';
  }

}
