import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-role',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    HttpClientModule
  ],
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {
  roles: any[] = [];
  role: any = { id: 0, name: '', description: '', state: false };

  private apiUrl = 'http://localhost:9191/api/Role';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getRoles();
  }

  getRoles(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (roles) => {
        this.roles = roles;
        this.cdr.detectChanges();
        console.log('Roles fetched:', this.roles);
      },
      (error) => {
        console.error('Error fetching roles:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    if (this.role.id === 0) {
      // Add a new role
      this.http.post(this.apiUrl, this.role).subscribe(() => {
        this.getRoles();  // Refresh the list of roles
        form.resetForm();  // Reset the form visually
        this.resetForm();  // Reset the role object
        
        Swal.fire('Success', 'Role created successfully!', 'success');
      });
    } else {
      // Update an existing role
      this.http.put(`${this.apiUrl}/${this.role.id}`, this.role).subscribe(() => {
        this.getRoles();
        form.resetForm();
        this.resetForm();
        
        Swal.fire('Success', 'Role updated successfully!', 'success');
      });
    }
  }

  editRole(role: any): void {
    this.role = { ...role };
  }

  deleteRole(id: number): void {
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
          this.getRoles();
          Swal.fire(
            'Deleted!',
            'Your role has been deleted.',
            'success'
          );
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your role is safe :)',
          'error'
        );
      }
    });
  }

  resetForm(): void {
    this.role = { id: 0, name: '', description: '', state: false };
  }
}
