import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor() { }


  abrirMenu() {
    document.getElementById('mySidenav').style.width = '173px';
  }
  fecharMenu() {
    document.getElementById('mySidenav').style.width = '0';
  }

  ngOnInit() {
  }

}
