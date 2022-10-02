import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public eventos: any = [];
  public widthImg = 50;
  public margenImg = 2;
  public showImage = false;
  public eventsFilter: any = []
  private _filterList;

  public get filterList() {
    return this._filterList;
  }

  public set filterList(value: string) {
    this._filterList = value;
    this.eventsFilter = this.filterList
      ? this.filterEvents(this.filterList)
      : this.eventos;
  }

  constructor(private http: HttpClient) {}

  API = 'https://localhost:5001/api/Eventos';

  ngOnInit(): void {
    this.getEventos();
  }

  public filterEvents(filterFor: string): any {
    filterFor = filterFor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento) =>
        evento.tema.toLocaleLowerCase().indexOf(filterFor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filterFor) !== -1
    );
  }

  public getEventos(): void {
    this.http.get(this.API).subscribe(
      (response) => {
        this.eventos = response;
        this.eventsFilter = this.eventos
      },
      (error) => console.log(error)
    );
  }

  showImageHtml() {
    this.showImage = !this.showImage;
  }
}
