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
  selector: 'app-department',
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
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {
  departments: any[] = [];
  department: any = { id: 0, name: '', code: '', description: '', countryId: 0, state: false };
  countries: any[] = [];

  dtOptions: Config = {};

  private apiUrl = 'http://localhost:9191/api/Department';
  private countriesApiUrl = 'http://localhost:9191/api/Country';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getDepartments();
    this.getCountries();
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
  }

  getDepartments(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (departments) => {
        this.departments = departments;
        this.cdr.detectChanges();
        console.log('Departments fetched:', this.departments);
      },
      (error) => {
        console.error('Error fetching departments:', error);
      }
    );
  }

  getCountries(): void {
    this.http.get<any[]>(this.countriesApiUrl).subscribe(
      (countries) => {
        this.countries = countries;
        console.log('Countries fetched:', this.countries);
      },
      (error) => {
        console.error('Error fetching countries:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    if (this.department.id === 0) {
      // Add a new department
      this.http.post(this.apiUrl, this.department).subscribe(() => {
        this.getDepartments();  // Refresh the list of departments
        form.resetForm();  // Reset the form visually
        this.resetForm();  // Reset the department object
        
        Swal.fire('Success', 'Department created successfully!', 'success');
      });
    } else {
      // Update an existing department
      this.http.put(`${this.apiUrl}/${this.department.id}`, this.department).subscribe(() => {
        this.getDepartments();
        form.resetForm();
        this.resetForm();
        
        Swal.fire('Success', 'Department updated successfully!', 'success');
      });
    }
  }

  editDepartment(department: any): void {
    this.department = { ...department };
  }

  deleteDepartment(id: number): void {
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
          this.getDepartments();
          Swal.fire(
            'Deleted!',
            'Your department has been deleted.',
            'success'
          );
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your department is safe :)',
          'error'
        );
      }
    });
  }

  resetForm(): void {
    this.department = { id: 0, name: '', code: '', description: '', countryId: 0, state: false };
  }

  // Method to get the country name based on countryId
  getCountryName(countryId: number): string {
    const country = this.countries.find(c => c.id === countryId);
    return country ? country.name : 'Unknown';
  }
}
