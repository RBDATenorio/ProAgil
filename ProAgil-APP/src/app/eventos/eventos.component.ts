import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/Evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
defineLocale('pt-br', ptBrLocale);
@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos?: Evento[];
  evento!: Evento;
  imagemLargura = 50;
  imagemMargem = 2;
  _filtroLista = '';
  modoSalvar = 'post';
  registerForm!: FormGroup;

  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }  

  eventosFiltrados: any = [];

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private localeService: BsLocaleService,
              private fb: FormBuilder) 
              { 
                this.localeService.use('pt-br');
              }

  ngOnInit(): void { 
    this.getEventos();
    this.validation();
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  novoEvento(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  editarEvento(evento: Evento, template: any) {
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);

  }

  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      (_eventos: Evento[]) => { 
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;},
      (error) => {
        console.log(error);
      }
    );
  }

  filtrarEvento(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos?.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1

    );
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50)
      ]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', Validators.required],
      imagemUrl: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [
        Validators.required,
        Validators.email
      ]]
    });
  }

  salvarAlteracao(template: any) {
    if(this.registerForm.valid)
      if(this.modoSalvar === 'post') { 
        this.evento = Object.assign({}, this.registerForm.value);
        this.eventoService.postEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
          },
          () => {}
        );
      } else {
          this.evento = Object.assign({eventoId: this.evento.eventoId}, this.registerForm.value);
          this.eventoService.putEvento(this.evento).subscribe(
            () => {
              template.hide();
              this.getEventos();
            },
            () => {}
          );
      }
  }
}
