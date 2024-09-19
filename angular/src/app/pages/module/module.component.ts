import { Component, OnInit, ChangeDetectorRef} from '@angular/core';


import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';

@Component({
  selector: 'app-module',
  standalone: true,
  imports: [HttpClientModule, FormsModule, CommonModule, RouterModule, DataTablesModule],  // Asegúrate de que esté importado aquí
  templateUrl: './module.component.html',
  styleUrls: ['./module.component.css']
})
export class ModuleComponent implements OnInit {
  modules: any[] = [];
  module: any = { id: 0, name: '', description: '', state: false };

  dtOptions: Config = {};

  

  private apiUrl = 'http://localhost:9191/api/Module';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getModules();
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
  }

 

  getModules(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (modules) => {
        this.modules = modules;
        this.cdr.detectChanges();
        console.log('Modules fetched:', this.modules);
      },
      (error) => {
        console.error('Error fetching modules:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    if (this.module.id === 0) {
      // Agregar un nuevo registro
      this.http.post(this.apiUrl, this.module).subscribe(() => {
        this.getModules();  // Refresca la lista de roles
        form.resetForm();  // Resetea el formulario visualmente
        this.resetForm();  // Resetea el objeto role
        Swal.fire('Success', 'Modulo created successfully!', 'success');
      });
    } else {
      // Actualizar un registro existente
      this.http.put(`${this.apiUrl}/${this.module.id}`, this.module).subscribe(() => {
        this.getModules();
        form.resetForm();
        this.resetForm();
        Swal.fire('Success', 'Modulo update successfully!', 'success');
        
      });
    }
  }
  

  editModule(module: any): void {
    this.module = { ...module };
  }

  deleteModule(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You won\'t be able to revert this!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
          this.getModules();
          Swal.fire(
            'Deleted!',
            'Your role has been deleted.',
            'success'
          );
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your Module is safe :)',
          'error'
        );
      }
    });
  }

  resetForm(): void {
    this.module = { id: 0, name: '', description: '', state: false };
  }
  
}
