import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { User } from '../_models/User';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Divida } from '../_models/Divida';
import { Renda } from '../_models/Renda';
import { ValoresDividaAndRenda } from '../_models/ValoresDividaAndRenda';
import { map } from 'rxjs/operators';
import { DividaService } from '../_services/divida.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  users: User[] = [];
  usersLast: User[] = [];
  usersFiltrados: User[] = [];
  user: User = new User();
  form: FormGroup;
  postOrPot = '';
  _filtroLista = '';
  // --> Pages
  pageAtual = 1;
  qtdItensPorPage = 5;
  qtdPages: number;
  // Dividas
  dividasUser: Divida[] = [];
  dividaUser: Divida = new Divida();
  // --> Painel
  id = 14;
  valorePainel: ValoresDividaAndRenda = new ValoresDividaAndRenda();
  // Loading
  public loading = false;


  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.usersFiltrados = this._filtroLista ? this.filtrarLista(this.filtroLista) : this.users;
  }


  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    private dividaService: DividaService
  ) { }

  ngOnInit() {
    this.validacao();
    this.getAllUser();
    this.getValoresPainel(this.id);
    this.getUltimosUsers();
  }

  filtrarLista(buscar: string): User[] {
    this.userService.getAllByNomeOrEmail(buscar).subscribe(
      data => {
        this.usersFiltrados = data;
        this.loading = false;
        return this.users;
      }, error => {
        console.log(`Erro no filtro. CODE: ${error}`);
      }
    );
    return [];
  }

  validacao() {
    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(60)]],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [ Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
      papel: ['', [Validators.required, Validators.max(2), Validators.min(1)]],
      dividas: this.fb.array([]),
      rendas: this.fb.array([])
    });
  }
  // POST AND PUT
  inserir(inserir: any) {
    this.postOrPot = 'post';
    this.form.reset();
    inserir.show();
  }
  editar(dados: any, _user: User) {
    this.postOrPot = 'put';
    this.form.reset();
    dados.show();
    this.user = _user;
    this.form.patchValue(this.user);
  }
  salvarAlteracao(dados: any) {
    if (this.form.valid) {
    this.loading = true;
      if (this.postOrPot === 'post') {
        this.user = Object.assign({  }, this.form.value);
        this.userService.post(this.user).subscribe(
          (newUser: User) => {
            dados.hide();
            this.getAllUser();
          }, error => {
            console.log(`Erro ao Adicionar. CODE: ${error}`);
          }
        );
      } else if (this.postOrPot === 'put') {
        this.user = Object.assign({ id: this.user.id }, this.form.value);
        this.userService.put(this.user).subscribe(
          (editUser: User) => {
            dados.hide();
            this.getAllUser();
          this.loading = false;
          }, error => {
            console.log(`Erro ao Atualizar. CODE: ${error}`);
          this.loading = false;
          }
        );
      } else {
        console.log(`Não entrou em nenhum if`);
        this.loading = false;
      }
    }
  }
  // DETAILS
  detalhes(dados: any, _user: User) {
    this.user = _user;
    dados.show();
    if (this.user) {
      this.dividaService.getAllDividaByUserId(this.user.id).subscribe(
        (data: Divida[]) => {
          this.dividasUser = data;
          console.log(data);
        }, error => {
          console.log('aqui parou');
        }
      );
    } else {
      console.log('deu errado');
    }
  }
  // DELETE
  delete(userDelete: User) {
    this.loading = true;
    this.user = userDelete;
    if (this.user.id !== null) {
      this.userService.delete(this.user.id).subscribe(
        (data) => {
          console.log('Deletado');
          this.getAllUser();
          this.loading = false;
        }, error => {
          console.log(`Erro ao deletar. CODE: ${error}`);
          this.loading = false;
        }
      );
    }
  }
  // GET
  qtdpagesUser() {
    this.userService.qtdPages().subscribe(
      data => {
        this.qtdPages = data;
      }, error => {
        console.log('Error');
      }
    );
  }
  getValoresPainel(id: number) {
    this.userService.getValoresPainel(id).subscribe(
      (data: ValoresDividaAndRenda) => {
        this.valorePainel = data;
      }, error => {
        console.log(`Erro no listar valores do painel. CODE: ${error}`);
      }
    );
  }
  getUltimosUsers() {
    return this.userService.getUltimosUsers().subscribe(
      (data: User[]) => {
        this.usersLast = data;
      } , error => {
        console.log(error);
      }
    );
  }
  getAllUser() {
    this.loading = true;
    return this.userService.getAll().subscribe(
      (data: User[]) => {
        this.users = data;
        this.usersFiltrados = this.users;
        this.loading = false;
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
