import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(
    public router: Router,
    private authService: AuthService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    if (localStorage.getItem('token') !== null) {
      this.router.navigate(['/divida']);
    }
  }

  login() {
    this.authService.login(this.model).subscribe(
      () => {
        this.router.navigate(['/divida']);
        this.toastr.success('Sucesso no Login');
      }, error => {
        this.toastr.error(`Usuario ou Senha Invalidos!`);
      }
    );
  }


}
