import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { User } from '../_models/User';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  users: User[];
  form: FormGroup;
  _filtroLista = '';
  usersFiltrados: User[] = [];

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
      senha: ['']
    });
  }
  getAllUser() {
    this.userService.getAll().subscribe(
      (data: User[]) => {
        this.users = data;
        this.usersFiltrados = this.users;
        console.log(data);
      }, error => {
        console.log(`Error. CODE: ${error}`);
      }
    );
  }

}
