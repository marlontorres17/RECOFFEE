import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { RouterModule } from '@angular/router';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatOptionModule } from '@angular/material/core';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';

@Component({
  selector: 'app-rovi',
  standalone: true,
  imports: [HttpClientModule, FormsModule, CommonModule, RouterModule, MatFormFieldModule, MatSelectModule, MatOptionModule, DataTablesModule],
  templateUrl: './rovi.component.html',
  styleUrls: ['./rovi.component.css']
})
export class RoviComponent implements OnInit {
  roleViews: any[] = [];
  roleView: any = { id: 0, roleId: 0, viewId: 0, state: false };
  roles: any[] = [];
  views: any[] = [];
  
  dtOptions: Config = {};

  private apiUrl = 'http://localhost:9191/api/RoleView';
  private roleApiUrl = 'http://localhost:9191/api/Role';
  private viewApiUrl = 'http://localhost:9191/api/View';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getRoleViews();
    this.getRoles();
    this.getViews();
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
  }

  getRoleViews(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (roleViews) => {
        this.roleViews = roleViews;
        this.cdr.detectChanges();
        console.log('RoleViews fetched:', this.roleViews);
      },
      (error) => {
        console.error('Error fetching roleViews:', error);
      }
    );
  }

  getRoles(): void {
    this.http.get<any[]>(this.roleApiUrl).subscribe(
      (roles) => {
        this.roles = roles;
        console.log('Roles fetched:', this.roles);
      },
      (error) => {
        console.error('Error fetching roles:', error);
      }
    );
  }

  getViews(): void {
    this.http.get<any[]>(this.viewApiUrl).subscribe(
      (views) => {
        this.views = views;
        console.log('Views fetched:', this.views);
      },
      (error) => {
        console.error('Error fetching views:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    if (this.roleView.id === 0) {
      this.http.post(this.apiUrl, this.roleView).subscribe(() => {
        this.getRoleViews();  // Refresca la lista de roleViews
        form.resetForm();  // Resetea el formulario visualmente
        this.resetForm();  // Resetea el objeto roleView
        
        Swal.fire('Success', 'RoleView created successfully!', 'success');
      });
    } else {
      this.http.put(`${this.apiUrl}/${this.roleView.id}`, this.roleView).subscribe(() => {
        this.getRoleViews();
        form.resetForm();
        this.resetForm();
        
        Swal.fire('Success', 'RoleView updated successfully!', 'success');
      });
    }
  }
  
  editRoleView(roleView: any): void {
    this.roleView = { ...roleView };
  }

  deleteRoleView(id: number): void {
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
          this.getRoleViews();
          Swal.fire(
            'Deleted!',
            'Your roleView has been deleted.',
            'success'
          );
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your roleView is safe :)',
          'error'
        );
      }
    });
  }

  resetForm(): void {
    this.roleView = { id: 0, roleId: 0, viewId: 0, state: false };
  }

  getRoleName(roleId: number): string {
    const role = this.roles.find(r => r.id === roleId);
    return role ? role.name : 'Unknown';
  }

  getViewName(viewId: number): string {
    const view = this.views.find(v => v.id === viewId);
    return view ? view.name : 'Unknown';
  }
}
