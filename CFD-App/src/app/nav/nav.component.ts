import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(
    public router: Router
  ) {  }

  ngOnInit() {
  }


  abrirMenu() {
    document.getElementById('mySidenav').style.width = '173px';
  }
  fecharMenu() {
    document.getElementById('mySidenav').style.width = '0';
  }

}
