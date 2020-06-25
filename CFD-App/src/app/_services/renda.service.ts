import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Renda } from '../_models/Renda';

@Injectable({
  providedIn: 'root'
})
export class RendaService {

  baseURL = 'http://localhost:5000/api/renda';
  baseURLValores = ' http://localhost:5000/api/divida/valorTotal/';

  constructor(private http: HttpClient) { }

  // Listar Por UserId
  getAllRendaByUserId(id: number): Observable<Renda[]> {
    return this.http.get<Renda[]>(`${this.baseURL}/detalhes/${id}`);
  }
  // Valores do Painel
  getPainelValores(id: number) {
    return this.http.get(`${this.baseURLValores}${id}`);
  }
  // Listar Ultimas Rendas Adicionadas
  getLastRendaAdd(): Observable<Renda[]> {
    return this.http.get<Renda[]>(`${this.baseURL}/rendaLast`);
  }
  // Listar Todos
  getAllRenda(): Observable<Renda[]> {
    return this.http.get<Renda[]>(this.baseURL);
  }
  // Listar Por Id
  getRendaById(id: number): Observable<Renda> {
    return this.http.get<Renda>(`${this.baseURL}/${id}`);
  }
  // Listar Por Titulo ou Valor ou Descricao
  GetRendaByTituloOrValorOrDescricao(buscar: string): Observable<Renda[]> {
    return this.http.get<Renda[]>(`${this.baseURL}/Buscar/${buscar}`);
  }
  // Adicionar
  post(renda: Renda) {
    return this.http.post(this.baseURL, renda);
  }
  // Atualizar
  put(renda: Renda) {
    return this.http.put(`${this.baseURL}/${renda.id}`, renda);
  }
  // Deletar
  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
