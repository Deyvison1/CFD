import { Component, OnInit } from '@angular/core';
import { DividaService } from '../_services/divida.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Divida } from '../_models/Divida';
import { ValoresDividaAndRenda } from '../_models/ValoresDividaAndRenda';

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
    private fb: FormBuilder
  ) { }

  ngOnInit() {
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
      titulo: [''],
      valor: [''],
      descricao: ['']
    });
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
