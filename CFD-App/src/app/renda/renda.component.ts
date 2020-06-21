import { Component, OnInit } from '@angular/core';
import { RendaService } from '../_services/renda.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Renda } from '../_models/Renda';
import { ValoresDividaAndRenda } from '../_models/ValoresDividaAndRenda';

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
    private fb: FormBuilder
    ) { }

  ngOnInit() {
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
      titulo: [''],
      valor: [''],
      descricao: ['']
    });
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
