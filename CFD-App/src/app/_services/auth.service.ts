import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseURL = 'http://localhost:5000/api/user/login';
  jwtHelper = new JwtHelperService;
  decodificadorToken: any;

  constructor(
    private http: HttpClient
  ) { }

  login(model: any) {
    return this.http.post(`${this.baseURL}`, model).pipe(
      map((resp: any) => {
        const user = resp;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodificadorToken = this.jwtHelper.decodeToken(user.token);
          sessionStorage.setItem('nome', this.decodificadorToken.unique_name);
          sessionStorage.setItem('id', this.decodificadorToken.email);
          sessionStorage.setItem('nivelUsuario', this.decodificadorToken.role);
        }
      })
    );
  }

  logado() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
