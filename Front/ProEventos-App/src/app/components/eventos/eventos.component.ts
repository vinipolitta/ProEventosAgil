import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public eventos: any;
  constructor(private http: HttpClient) {}

   API = "https://localhost:5001/api/Eventos"

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get(this.API).subscribe(
      response => this.eventos = response,
      error => console.log(error)

    )
  }
}
