import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Director } from '../models/director.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class DirectorService {
  private baseUrl = `${environment.apiUrl}/directors`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Director[]> {
    return this.http.get<Director[]>(this.baseUrl);
  }

  create(director: Partial<Director>): Observable<Director> {
    return this.http.post<Director>(this.baseUrl, director);
  }

  update(id: number, director: Partial<Director>): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, director);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}