import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../models/movie.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class MovieService {
  private baseUrl = `${environment.apiUrl}/movies`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Movie[]> {
    return this.http.get<Movie[]>(this.baseUrl);
  }

  create(movie: Partial<Movie>): Observable<Movie> {
    return this.http.post<Movie>(this.baseUrl, movie);
  }

  update(id: number, movie: Partial<Movie>): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, movie);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}