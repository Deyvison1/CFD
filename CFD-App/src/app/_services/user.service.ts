import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/User';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseURL = 'http://localhost:5000/api/user';

  constructor(
    private http: HttpClient
  ) { }

  // Gets
  // Todos
  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.baseURL);
  }
  // Lista por Id
  getById(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseURL}/${id}`);
  }
  // Lista por Nome ou E-mail
  getAllByNomeOrEmail(buscar: string): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseURL}/Buscar/${buscar}`);
  }

  // Adicionar
  post(user: User)  {
    return this.http.post(this.baseURL, user);
  }

  // Editar
  put(user: User) {
    return this.http.put(`${this.baseURL}/${user.id}`, user);
  }

  // Deletar
  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
