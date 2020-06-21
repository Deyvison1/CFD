import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { User } from '../_models/User';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Divida } from '../_models/Divida';
import { Renda } from '../_models/Renda';
import { ValoresDividaAndRenda } from '../_models/ValoresDividaAndRenda';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  
  valorePainel: ValoresDividaAndRenda = new ValoresDividaAndRenda();
  users: User[] = [];
  form: FormGroup;
  _filtroLista = '';
  usersFiltrados: User[] = [];
  user: User = new User();
  pageAtual: number = 1;
  qtdItensPorPage: number = 5;
  qtdPages: number;
  id = 1;

  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.usersFiltrados = this._filtroLista ? this.filtrarLista(this.filtroLista) : this.users;
  }


  constructor(
    private userService: UserService,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.validacao();
    this.getAllUser();
    this.getValoresPainel(this.id);
  }

  filtrarLista(buscar: string): User[] {
    this.userService.getAllByNomeOrEmail(buscar).subscribe(
      data => {
        this.usersFiltrados = data;
        return this.users;
      }, error => {
        console.log(`Erro no filtro. CODE: ${error}`);
      }
    );
    return [];
  }

  validacao() {
    this.form = this.fb.group({
      nome: [''],
      email: [''],
      senha: [''],
      valorTotalDividasPorId: [''],
      valorTotalDividasPagas: [''],

    });
  }
  qtdpagesUser() {
    this.userService.qtdPages().subscribe(
      data => {
        this.qtdPages = data;
        console.log('deu certo');
      }, error => {
        console.log('Error');
      }
    );
  }
  getValoresPainel(id: number) {
    this.userService.getValoresPainel(id).subscribe(
      (data: ValoresDividaAndRenda) => {
        this.valorePainel = data;
        console.log(data);
      }, error => {
        console.log(`Erro no listar valores do painel. CODE: ${error}`);
      }
    );
  }
  getAllUser() {
    return this.userService.getAll().subscribe(
      (data: User[]) => {
        this.users = data;
        this.usersFiltrados = this.users;
        console.log(data);
      }, error => {
        console.log(`Erro ao listar. CODE: ${error}`);
      }
    );
  }
  /*
  getAllUserPage(page: number) {
    this.userService.getAllUserPage(this.pageAtual).subscribe(
      (data: User[]) => {
        this.users = data;
        this.usersFiltrados = this.users;
        console.log(data);
      }, error => {
        console.log(`Error. CODE: ${error}`);
      }
    );
  }
  */
}
