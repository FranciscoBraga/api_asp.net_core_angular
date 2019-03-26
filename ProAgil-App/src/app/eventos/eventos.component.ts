
import { Component, OnInit, TemplateRef, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import {defineLocale, BsLocaleService, ptBrLocale} from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  @Input() titulo = 'Eventos';

  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  modoSalvar = 'post';
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  bodyDeletarEvento = '';

  _filtroLista = '';

  constructor(
     private eventoService: EventoService
   , private modalService: BsModalService
   , private fb: FormBuilder
   , private localService: BsLocaleService
   , private toastrService: ToastrService
    ) {
      this.localService.use('pt-br');
     }

  get filtroLista() {
     return this._filtroLista;
  }

  set filtroLista(values: string) {
    this._filtroLista = values;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  editarEvento(evento: Evento, template: any){
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }

  novoEvento(template: any){
    this.modoSalvar = 'post';
    this.openModal(template); 
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.getEventos();
          this.toastrService.success('Deletado com sucesso');
        }, error => {
          this.toastrService.error(`Erro ao Inserir: ${error}`);
          console.log(error);
        }
    );
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImage() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
      this.eventoService.getAllEvento().subscribe((_evento: Evento[]) =>{
      this.eventos = _evento;
      this.eventosFiltrados = this.eventos;
      console.log(_evento);
    }, error => {
      this.toastrService.error(`Erro ao Carregar: ${error}`);
    }
    );
  }

  validation(){
    this.registerForm = this.fb.group({
      tema: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['',Validators.required],
      dataEvento: ['',Validators.required],
      imagemUrl: ['',Validators.required],
      qtaPessoas: ['',[Validators.required, Validators.max(120000)]],
      telefone: ['',Validators.required],
      email: ['',[Validators.required, Validators.email]]
    });
  }

  salvarAlteracao(template: any){
    if(this.registerForm.valid){
      if(this.modoSalvar === 'post'){
        this.evento = Object.assign({}, this.registerForm.value);
        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            console.log(novoEvento);
            template.hide();
            this.getEventos();
            this.toastrService.success('Inserido com sucesso');
          }, error =>{ 
            this.toastrService.error(`Erro ao Inserir: ${error}`);
            console.log(error);
          }
        );
      }
       else {
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
        this.eventoService.putEvento(this.evento).subscribe(
          (editar: Evento) => {
            console.log(editar);
            template.hide();
            this.getEventos();
            this.toastrService.success('Atualizado com sucesso');
          }, error =>{ 
            this.toastrService.error(`Erro ao atualizar: ${error}`);
            console.log(error);
          }
      );
      }
    }
  }

}
