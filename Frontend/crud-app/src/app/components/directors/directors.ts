import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Director } from '../../models/director.model';
import { DirectorService } from '../../services/director';

@Component({
  selector: 'app-directors',
  imports: [FormsModule, CommonModule],
  templateUrl: './directors.html'
})
export class Directors implements OnInit {
  directors = signal<Director[]>([]);
  editing: Director | null = null;
  form: Partial<Director> = { name: '', age: null, active: false };

  constructor(private directorService: DirectorService) { }

  ngOnInit(): void {
    this.loadDirectors();
  }

  loadDirectors(): void {
    this.directorService.getAll().subscribe(data => this.directors.set(data));
  }

  submit(): void {
    if (this.editing) {
      this.directorService.update(this.editing.pkDirector, this.form).subscribe(() => {
        this.cancelEdit();
        this.loadDirectors();
      });
    } else {
      this.directorService.create(this.form).subscribe(() => {
        this.resetForm();
        this.loadDirectors();
      });
    }
  }

  edit(director: Director): void {
    this.editing = director;
    this.form = { name: director.name, age: director.age, active: director.active };
  }

  cancelEdit(): void {
    this.editing = null;
    this.resetForm();
  }

  resetForm(): void {
    this.form = { name: '', age: null, active: false };
  }

  remove(id: number): void {
    if (confirm('¿Eliminar este director?')) {
      this.directorService.delete(id).subscribe(() => this.loadDirectors());
    }
  }
}