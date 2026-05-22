import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tarefa } from '../models/tarefa.model';

@Injectable({
  providedIn: 'root'
})
export class TarefaService {
  private apiUrl = 'http://localhost:5071/api/Tarefas';

  constructor(private http: HttpClient) { }

  getTarefas(): Observable<Tarefa[]> {
    return this.http.get<Tarefa[]>(this.apiUrl);
  }

  getTarefaPorId(id: number): Observable<Tarefa> {
    return this.http.get<Tarefa>(`${this.apiUrl}/${id}`);
  }

  criarTarefa(tarefa: Tarefa): Observable<Tarefa> {
    return this.http.post<Tarefa>(this.apiUrl, tarefa);
  }

  atualizarTarefa(id: number, tarefa: Tarefa): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, tarefa);
  }

  excluirTarefa(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}