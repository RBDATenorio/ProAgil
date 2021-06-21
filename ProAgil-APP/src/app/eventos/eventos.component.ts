import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: any = [];
  imagemLargura = 50;
  imagemMargem = 2;
  _filtroLista = '';
  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }  
  
  eventosFiltrados: any = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void { 
    this.getEventos();
    console.log(this.eventos);
  }

  getEventos() {
    this.http.get('http://localhost:5000/api/values').subscribe(
      response => { 
                    this.eventos = response;
                    this.eventosFiltrados = this.eventos;},
      (error) => {
        console.log(error);
      }
    );
  }

  filtrarEvento(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1

    );
  }

}
