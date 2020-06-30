import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Divida } from '../_models/Divida';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DividaService {

  baseURL = 'http://localhost:5000/api/divida';
  baseURLValores = ' http://localhost:5000/api/divida/valorTotal/';

  constructor(private http: HttpClient) { }


  // Listar Divida Por UserId
  getAllDividaByUserId(id: number): Observable<Divida[]> {
    return this.http.get<Divida[]>(`${this.baseURL}/dividaByUser/${id}`);
  }
  // Valores Painel
  getValoresPainel(id: number) {
    return this.http.get(`${this.baseURLValores}${id}`);
  }
  // Listar Ultimos Por Id
  getLastDividaById(id: number): Observable<Divida[]> {
    return this.http.get<Divida[]>(`${this.baseURL}/dividaLastById/${id}`);
  }
  // Listar Ultimos
  getLastDivida(): Observable<Divida[]> {
    return this.http.get<Divida[]>(`${this.baseURL}/dividaLast`);
  }
  // Listar Todos
  getAll(): Observable<Divida[]> {
    return this.http.get<Divida[]>(this.baseURL);
  }
  // Listar Por Id
  getDividaById(id: number): Observable<Divida> {
    return this.http.get<Divida>(`${this.baseURL}/${id}`);
  }
  // Listar Por Titulo ou Descricao ou Valor
  getDividaByTituloOrDescricaoOrValor(buscar: string): Observable<Divida[]> {
    return this.http.get<Divida[]>(`${this.baseURL}/Buscar/${buscar}`);
  }
  // Adicionar
  post(divida: Divida) {
    return this.http.post(this.baseURL, divida);
  }
  // Atualizar
  put(divida: Divida) {
    return this.http.put(`${this.baseURL}/${divida.id}`, divida);
  }
  // Deletar
  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
