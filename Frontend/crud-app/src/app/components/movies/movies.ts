import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Movie } from '../../models/movie.model';
import { Director } from '../../models/director.model';
import { MovieService } from '../../services/movie';
import { DirectorService } from '../../services/director';

@Component({
  selector: 'app-movies',
  imports: [FormsModule, CommonModule],
  templateUrl: './movies.html'
})
export class Movies implements OnInit {
  movies = signal<Movie[]>([]);
  directors = signal<Director[]>([]);
  editing: Movie | null = null;
  form: Partial<Movie> = { name: '', gender: '', duration: '', fkDirector: undefined };

  constructor(private movieService: MovieService, private directorService: DirectorService) { }

  ngOnInit(): void {
    this.loadMovies();
    this.directorService.getAll().subscribe(data => this.directors.set(data));
  }

  loadMovies(): void {
    this.movieService.getAll().subscribe(data => this.movies.set(data));
  }

  submit(): void {
    if (this.editing) {
      this.movieService.update(this.editing.pkMovies, this.form).subscribe(() => {
        this.cancelEdit();
        this.loadMovies();
      });
    } else {
      this.movieService.create(this.form).subscribe(() => {
        this.resetForm();
        this.loadMovies();
      });
    }
  }

  edit(movie: Movie): void {
    this.editing = movie;
    this.form = { name: movie.name, gender: movie.gender, duration: movie.duration, fkDirector: movie.fkDirector };
  }

  cancelEdit(): void {
    this.editing = null;
    this.resetForm();
  }

  resetForm(): void {
    this.form = { name: '', gender: '', duration: '', fkDirector: undefined };
  }

  remove(id: number): void {
    if (confirm('¿Eliminar esta película?')) {
      this.movieService.delete(id).subscribe(() => this.loadMovies());
    }
  }
}