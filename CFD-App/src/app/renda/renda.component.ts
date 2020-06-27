import { Component, OnInit } from '@angular/core';
import { RendaService } from '../_services/renda.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Renda } from '../_models/Renda';
import { ValoresDividaAndRenda } from '../_models/ValoresDividaAndRenda';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-renda',
  templateUrl: './renda.component.html',
  styleUrls: ['./renda.component.css']
})
export class RendaComponent implements OnInit {

  renda: Renda = new Renda();
  rendas: Renda[] = [];
  rendasLast: Renda[] = [];
  rendasFiltradas: Renda[] = [];
  id = 1;
  form: FormGroup;


  // Variaveis Aux
  tituloModal = '';
  metodoSalvar = '';

  // Pages
  pageAtual = 1;
  itensPorPage = 7;
  public loading = false;

  valorespainel: ValoresDividaAndRenda = new ValoresDividaAndRenda();
  _filtroLista = '';
  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.rendasFiltradas = this._filtroLista ? this.filtrarLista(this.filtroLista) : this.rendas;
  }


  constructor(
    private rendaService: RendaService,
    private fb: FormBuilder,
    private toastr: ToastrService
    ) { }

  ngOnInit() {
    this.validacao();
    this.getAll();
    this.getValoresPainel();
    this.getUltimasRendasAdd();
  }
  filtrarLista(buscar: string): Renda[] {
    this.rendaService.GetRendaByTituloOrValorOrDescricao(buscar).subscribe(
      data => {
        this.rendasFiltradas = data;
        this.loading = false;
        return this.rendas;
      }, error => {
        console.log(`Erro no filtro. CODE: ${error}`);
      }
    );
    return [];
  }

  validacao() {
    this.form = this.fb.group({
      titulo: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(80)]],
      tipo: ['', [Validators.required, Validators.min(0), Validators.max(1)]],
      dataRenda: ['', [Validators.required]],
      valor: ['', Validators.required],
      descricao: [''],
      userId: ['']
    });
  }
  abrirModal(dados: any) {
    this.form.reset();
    dados.show();
  }
  // DELETAR
  confirmeDelete(dados: any, _renda: Renda) {
    dados.show();
    this.renda = _renda;
  }
  deletar(dados: any) {
    this.loading = true;
    if (this.renda.id !== null) {
      this.rendaService.delete(this.renda.id).subscribe(
        (data) => {
          dados.hide();
          this.toastr.success('Sucesso no Excluir');
          this.getAll();
          this.getUltimasRendasAdd();
          this.getValoresPainel();
          this.loading = false;
        }, error => {
          this.toastr.error(`Erro no Excluir. CODE: ${error}`);
        }
      );
    } else {
      this.toastr.error('Id Null');
    }
  }
  // DETALHES
  detalhes(dados: any, _renda: Renda) {
    this.abrirModal(dados);
    this.renda = _renda;
    this.form.patchValue(this.renda);
  }
  // POST
  inserir(dados: any) {
    this.metodoSalvar = 'post';
    this.tituloModal = 'Adicionar';
    this.abrirModal(dados);
  }
  // PUT
  editar(dados: any, _renda: Renda) {
    this.metodoSalvar = 'put';
    this.tituloModal = 'Editar';
    this.abrirModal(dados);
    this.renda = _renda;
    this.form.patchValue(this.renda);
  }
  salvarAlteracao(dados: any) {
    if (this.form.valid) {
      this.loading = true;
      if (this.metodoSalvar === 'post') {
        this.renda = Object.assign({ }, this.form.value);

        this.renda.userId = 1;
        this.rendaService.post(this.renda).subscribe(
          (data: Renda) => {
            dados.hide();
            this.toastr.success('Sucesso no Cadastro');
            this.getAll();
            this.getValoresPainel();
            this.getUltimasRendasAdd();
            this.loading = false;
          }, error => {
            this.toastr.error(`Erro no Cadastro. CODE: ${error}`);
          }
        );
      } else if (this.metodoSalvar === 'put') {
        this.renda = Object.assign({ id: this.renda.id }, this.form.value);
        // this.renda.userId = 2;
        this.rendaService.put(this.renda).subscribe(
          (data: Renda) => {
            dados.hide();
            this.toastr.success('Sucesso na Atualizacao');
            this.getAll();
            this.getUltimasRendasAdd();
            this.getValoresPainel();
            this.loading = false;
          }, error => {
            this.toastr.error(`Erro no Atualizar. CODE: ${error}`);
          }
        );
      } else {
        this.toastr.error('Não e put nem post. Erro');
      }
    }
  }

  getUltimasRendasAdd() {
    this.loading = true;
    return this.rendaService.getLastRendaAdd().subscribe(
      (data: Renda[]) => {
        this.rendasLast = data;
        console.log(data);
        this.loading = false;
      }, error => {
        console.log(`Erro no listar ultimas.CODE: ${error}`);
      }
    );
  }
  getValoresPainel() {
    return this.rendaService.getPainelValores(this.id).subscribe(
      (data: ValoresDividaAndRenda) => {
        this.valorespainel = data;
        console.log(data);
      }, error => {
        console.log(`Erro ao listar valores do painel. CODE: ${error}`);
      }
    );
  }
  getAll() {
    this.loading = true;
    return this.rendaService.getAllRenda().subscribe(
      (data: Renda[]) => {
        this.rendas = data;
        this.rendasFiltradas = this.rendas;
        console.log(data);
        this.loading = false;
      }, error => {
        console.log(`Erro ao listar todas. CODE: ${error}`);
      }
    );
  }

}
