import { Component, OnInit } from '@angular/core';
import { EventoService } from '../_services/Evento.service';

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

  constructor(private eventoService: EventoService) { }

  ngOnInit(): void { 
    this.getEventos();
  }

  getEventos() {
    this.eventoService.getEvento().subscribe(
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
