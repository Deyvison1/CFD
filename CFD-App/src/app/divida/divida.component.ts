import { Component, OnInit } from '@angular/core';
import { DividaService } from '../_services/divida.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Divida } from '../_models/Divida';
import { ValoresDividaAndRenda } from '../_models/ValoresDividaAndRenda';
import { escapeHtml } from '@angular/platform-browser/src/browser/transfer_state';
import { ToastrService } from 'ngx-toastr';
import { getLocaleDayPeriods } from '@angular/common';

@Component({
  selector: 'app-divida',
  templateUrl: './divida.component.html',
  styleUrls: ['./divida.component.css']
})
export class DividaComponent implements OnInit {

  dividas: Divida[] = [];
  divida: Divida = new Divida();
  dividaLast: Divida[] = [];
  form: FormGroup;
  id = 1;
  dividasFiltradas: Divida[] = [];

  // Painel Valores
  valorespainel: ValoresDividaAndRenda = new ValoresDividaAndRenda();
  // Pages
  pageAtual = 1;
  itensPorPage = 7;
  public loading = false;


  // VARIAVEIS AUX
  metodoSalvar = '';
  tituloModal = '';

  _filtroLista = '';

  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.dividasFiltradas = this._filtroLista ? this.filtrarLista(this.filtroLista) : this.dividas;
  }

  constructor(
    private dividaService: DividaService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.validacao();
    this.getAll();
    this.getUltimasDividas();
    this.getValoresPainel();
  }

  filtrarLista(buscar: string): Divida[] {
    this.dividaService.getDividaByTituloOrDescricaoOrValor(buscar).subscribe(
      data => {
        this.dividasFiltradas = data;
        this.loading = false;
        return this.dividas;
      }, error => {
        console.log(`Erro no filtro. CODE: ${error}`);
      }
    );
    return [];
  }

  validacao() {
    this.form = this.fb.group({
      titulo: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(80)]],
      dataCompra: ['', Validators.required],
      parcela: ['', Validators.required],
      formaCompra: ['', [Validators.required, Validators.max(1), Validators.min(0)]],
      dataVencimento: ['', Validators.required],
      valorParcela: [''],
      descricaoProduto: ['', Validators.required],
      userId: [''],
      dataRegistro: [''],
      dataModificacao: [''],
      situacao: [''],
      valorTotal: ['', Validators.required]
    });
  }

  // MODAL
  abrirModal(dados: any) {
    this.form.reset();
    dados.show();
  }
  // DETALHES
  detalhes(dados: any, _divida: Divida) {
    this.tituloModal = 'Detalhes';
    this.abrirModal(dados);
    this.divida = _divida;
    this.form.patchValue(this.divida);
  }
  // POST
  inserir(dados: any, _divida: Divida) {
    this.tituloModal = 'Adicionar';
    this.metodoSalvar = 'post';
    this.abrirModal(dados);
  }
  // PUT
  editar(dados: any, _divida: Divida) {
    this.tituloModal = 'Editar';
    this.metodoSalvar = 'put';
    this.abrirModal(dados);
    this.divida = _divida;
    this.form.patchValue(this.divida);
  }
  // DELETE
  deletar(dados: any, _divida: Divida) {
    this.abrirModal(dados);
    this.divida = _divida;
  }
  confirmeDelete(dados: any) {
    this.loading = true;
    this.dividaService.delete(this.divida.id).subscribe(
      (data: Divida) => {
        dados.hide();
        this.toastr.success('Sucesso no Excluir');
        this.getAll();
        this.getUltimasDividas();
        this.getValoresPainel();
        this.loading = false;
      }, error => {
        this.toastr.error(`Erro ao Deletar. CODE: ${error}`);
      }
    );
  }
  // PAGAR
  confirmaPagamento(_divida: Divida) {
    this.loading = true;

    this.divida = _divida;
    this.divida = Object.assign({ id: this.divida.id }, this.divida);
    this.divida.situacao = 1;
    this.dividaService.put(this.divida).subscribe(
      (data: Divida) => {
        this.toastr.success('Sucesso no Pagamento');
        this.getAll();
        this.getUltimasDividas();
        this.getValoresPainel();
        this.loading = false;
      }, error => {
        this.toastr.error(`Erro no Pagamento. CODE: ${error}`);
      }
    );
  }
  // SALVAR ALTERACOES
  salvarAlteracao(dados: any) {
    this.loading = true;
    if (this.form.valid) {
      if (this.metodoSalvar === 'post') {
        this.divida = Object.assign({  }, this.form.value);

        this.divida.userId = 1;
        this.divida.situacao = 0;
        this.divida.valorParcela = this.divida.valorTotal / this.divida.parcela;

        this.dividaService.post(this.divida).subscribe(
          (data: Divida) => {
            dados.hide();
            this.divida = data;
            this.toastr.success('Sucesso no Cadastro.');
            this.getAll();
            this.getUltimasDividas();
            this.getValoresPainel();
            this.loading = false;
          }, error => {
            this.toastr.error(`Erro no Cadastro. CODE: ${error}`);
          }
        );
      } else if (this.metodoSalvar === 'put') {
        this.divida = Object.assign({ id: this.divida.id }, this.form.value);

        this.divida.valorParcela = this.divida.valorTotal / this.divida.parcela;

        this.dividaService.put(this.divida).subscribe(
          (data: Divida) => {
            dados.hide();
            this.divida = data;
            this.toastr.success('Sucesso no Atualizar.');
            this.getAll();
            this.getUltimasDividas();
            this.getValoresPainel();
            this.loading = false;
          }, error => {
            this.toastr.error(`Erro no Atualizar. CODE: ${error}`);
          }
        );
      } else {
        this.toastr.error('Erro, não e post nem put.');
      }
    }
  }
  getValoresPainel() {
    return this.dividaService.getValoresPainel(this.id).subscribe(
      (data: ValoresDividaAndRenda) => {
        this.valorespainel = data;
      }, error => {
        console.log(`Erro ao listar valores do painel. CODE: ${error}`);
      }
    );
  }

  getUltimasDividas() {
    return this.dividaService.getLastDivida().subscribe(
      (data: Divida[]) => {
        this.dividaLast = data;
      }, error => {
        console.log(`Erro no listar ultimos. CODE: ${error}`);
      }
    );
  }

  getAll() {
    return this.dividaService.getAll().subscribe(
      (data: Divida[]) => {
        this.dividas = data;
        this.dividasFiltradas = this.dividas;
      }, error => {
        console.log(`Erro no listar todos. CODE: ${error}`);
      }
    );
  }
}
