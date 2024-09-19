import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';

@Component({
  selector: 'app-usle',
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
  templateUrl: './usle.component.html',
  styleUrls: ['./usle.component.css'],
})
export class UsleComponent implements OnInit {
  userRoles: any[] = [];
  users: any[] = [];
  roles: any[] = [];
  userRole: any = { id: 0, roleId: 0, userId: 0, state: false };
  
  private apiUrlUserRole = 'http://localhost:9191/api/UserRole';
  private apiUrlUser = 'http://localhost:9191/api/User';
  private apiUrlRole = 'http://localhost:9191/api/Role';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getUserRoles();
    this.getUsers();
    this.getRoles();
    
  }

  getUserRoles(): void {
    this.http.get<any[]>(this.apiUrlUserRole).subscribe(
      (userRoles) => {
        this.userRoles = userRoles;
        this.cdr.detectChanges();
      },
      (error) => {
        console.error('Error fetching user roles:', error);
      }
    );
  }

  getUsers(): void {
    this.http.get<any[]>(this.apiUrlUser).subscribe(
      (users) => {
        this.users = users;
        this.cdr.detectChanges();
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

  getRoles(): void {
    this.http.get<any[]>(this.apiUrlRole).subscribe(
      (roles) => {
        this.roles = roles;
        this.cdr.detectChanges();
      },
      (error) => {
        console.error('Error fetching roles:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    if (this.userRole.id === 0) {
      this.http.post(this.apiUrlUserRole, this.userRole).subscribe(() => {
        this.getUserRoles();
        form.resetForm();
        this.resetForm();
        Swal.fire('Success', 'User role created successfully!', 'success');
      });
    } else {
      this.http.put(`${this.apiUrlUserRole}/${this.userRole.id}`, this.userRole).subscribe(() => {
        this.getUserRoles();
        form.resetForm();
        this.resetForm();
        Swal.fire('Success', 'User role updated successfully!', 'success');
      });
    }
  }

  editUserRole(userRole: any): void {
    this.userRole = { ...userRole };
  }

  deleteUserRole(id: number): void {
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
        this.http.delete(`${this.apiUrlUserRole}/${id}`).subscribe(() => {
          this.getUserRoles();
          Swal.fire(
            'Deleted!',
            'Your user role has been deleted.',
            'success'
          );
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your user role is safe :)',
          'error'
        );
      }
    });
  }

  resetForm(): void {
    this.userRole = { id: 0, roleId: 0, userId: 0, state: false };
  }

  getUserName(userId: number): string {
    const user = this.users.find((u) => u.id === userId);
    return user ? user.userName : 'Unknown User';
  }

  getRoleName(roleId: number): string {
    const role = this.roles.find((r) => r.id === roleId);
    return role ? role.name : 'Unknown Role';
  }
}
