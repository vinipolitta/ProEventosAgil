import { EventoService } from './../../services/evento.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Evento } from 'src/app/model/evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public eventos: Evento[] = [];
  public widthImg = 50;
  public margenImg = 2;
  public showImage = false;
  public eventsFilter: Evento[] = [];
  private _filterList;
  content = 'Vivamus sagittis lacus vel augue laoreet rutrum faucibus.';


  public get filterList() {
    return this._filterList;
  }

  public set filterList(value: string) {
    this._filterList = value;
    this.eventsFilter = this.filterList
      ? this.filterEvents(this.filterList)
      : this.eventos;
  }

  constructor(private eventoService: EventoService) {}

  API = 'https://localhost:5001/api/Eventos';

  ngOnInit(): void {
    this.getEventos();
  }

  public filterEvents(filterFor: string): Evento[] {
    filterFor = filterFor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento) =>
        evento.tema.toLocaleLowerCase().indexOf(filterFor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filterFor) !== -1
    );
  }

  public getEventos(): void {
    this.eventoService.getEvento().subscribe(
      (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventsFilter = this.eventos;
        console.log('aqui',this.eventsFilter);

      },
      (error) => console.log(error)
    );
  }

  public showImageHtml() {
    this.showImage = !this.showImage;
  }
}
