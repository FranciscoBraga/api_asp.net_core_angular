import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {
baseUrl = 'http://Localhost:5000/api/evento';

constructor( private http: HttpClient) { }

getAllEvento(): Observable<Evento[]> {
  return this.http.get<Evento[]>(this.baseUrl);
}

getByTema(tema: string): Observable<Evento[]> {
  return this.http.get<Evento[]>(`${this.baseUrl}/getByTema/${tema}`);
}

getById(id: number): Observable<Evento> {
  return this.http.get<Evento>(`${this.baseUrl}/${id}`);
}

}
