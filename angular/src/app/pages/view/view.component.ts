import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-view',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    DataTablesModule,
    HttpClientModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {
  views: any[] = [];
  view: any = { id: 0, name: '', description: '', route: '', moduleId: 0, state: false };
  modules: any[] = [];

  dtOptions: Config = {};

  private apiUrl = 'http://localhost:9191/api/View';
  private modulesApiUrl = 'http://localhost:9191/api/Module';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getViews();
    this.getModules();
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
  }

  getViews(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (views) => {
        this.views = views;
        this.cdr.detectChanges();
        console.log('Views fetched:', this.views);
      },
      (error) => {
        console.error('Error fetching views:', error);
      }
    );
  }

  getModules(): void {
    this.http.get<any[]>(this.modulesApiUrl).subscribe(
      (modules) => {
        this.modules = modules;
        console.log('Modules fetched:', this.modules);
      },
      (error) => {
        console.error('Error fetching modules:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    if (this.view.id === 0) {
      // Add a new view
      this.http.post(this.apiUrl, this.view).subscribe(() => {
        this.getViews();  // Refresh the list of views
        form.resetForm();  // Reset the form visually
        this.resetForm();  // Reset the view object
        
        Swal.fire('Success', 'View created successfully!', 'success');
      });
    } else {
      // Update an existing view
      this.http.put(`${this.apiUrl}/${this.view.id}`, this.view).subscribe(() => {
        this.getViews();
        form.resetForm();
        this.resetForm();
        
        Swal.fire('Success', 'View updated successfully!', 'success');
      });
    }
  }

  editView(view: any): void {
    this.view = { ...view };
  }

  deleteView(id: number): void {
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
          this.getViews();
          Swal.fire(
            'Deleted!',
            'Your view has been deleted.',
            'success'
          );
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your view is safe :)',
          'error'
        );
      }
    });
  }

  resetForm(): void {
    this.view = { id: 0, name: '', description: '', route: '', moduleId: 0, state: false };
  }

  // Method to get the module name based on moduleId
  getModuleName(moduleId: number): string {
    const module = this.modules.find(mod => mod.id === moduleId);
    return module ? module.name : 'Unknown';
  }
}
