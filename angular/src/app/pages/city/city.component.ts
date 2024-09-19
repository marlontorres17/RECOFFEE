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
  selector: 'app-city',
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
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {
  cities: any[] = [];
  city: any = { id: 0, name: '', code: '', description: '', departmentId: 0, state: false };
  departments: any[] = [];

  dtOptions: Config = {};

  private apiUrl = 'http://localhost:9191/api/City';
  private departmentsApiUrl = 'http://localhost:9191/api/Department';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getCities();
    this.getDepartments();
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
  }

  getCities(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (cities) => {
        this.cities = cities;
        this.cdr.detectChanges();
        console.log('Cities fetched:', this.cities);
      },
      (error) => {
        console.error('Error fetching cities:', error);
      }
    );
  }

  getDepartments(): void {
    this.http.get<any[]>(this.departmentsApiUrl).subscribe(
      (departments) => {
        this.departments = departments;
        console.log('Departments fetched:', this.departments);
      },
      (error) => {
        console.error('Error fetching departments:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    if (this.city.id === 0) {
      // Add a new city
      this.http.post(this.apiUrl, this.city).subscribe(() => {
        this.getCities();
        form.resetForm();
        this.resetForm();
        
        Swal.fire('Success', 'City created successfully!', 'success');
      });
    } else {
      // Update an existing city
      this.http.put(`${this.apiUrl}/${this.city.id}`, this.city).subscribe(() => {
        this.getCities();
        form.resetForm();
        this.resetForm();
        
        Swal.fire('Success', 'City updated successfully!', 'success');
      });
    }
  }

  editCity(city: any): void {
    this.city = { ...city };
  }

  deleteCity(id: number): void {
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
          this.getCities();
          Swal.fire(
            'Deleted!',
            'Your city has been deleted.',
            'success'
          );
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your city is safe :)',
          'error'
        );
      }
    });
  }

  resetForm(): void {
    this.city = { id: 0, name: '', code: '', description: '', departmentId: 0, state: false };
  }

  // Method to get the department name based on departmentId
  getDepartmentName(departmentId: number): string {
    const department = this.departments.find(dep => dep.id === departmentId);
    return department ? department.name : 'Unknown';
  }
}
