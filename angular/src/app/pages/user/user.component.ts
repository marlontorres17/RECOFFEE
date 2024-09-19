import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    HttpClientModule,
    FormsModule,
    CommonModule,
    RouterModule,
    DataTablesModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule,
    MatOptionModule,
    MatSelectModule
  ],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  users: any[] = [];
  user: any = { id: 0, userName: '', password: '', personId: 0, state: false };
  persons: any[] = [];
  filteredPersons: any[] = [];

  dtOptions: Config = {};

  private apiUrl = 'http://localhost:9191/api/User';
  private personApiUrl = 'http://localhost:9191/api/Person';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getUsers();
    this.getPersons();
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
  }

  getUsers(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (users) => {
        this.users = users;
        this.cdr.detectChanges();
        console.log('Users fetched:', this.users);
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

  getPersons(): void {
    this.http.get<any[]>(this.personApiUrl).subscribe(
      (persons) => {
        this.persons = persons;
        this.filteredPersons = persons;
        this.cdr.detectChanges();
        console.log('Persons fetched:', this.persons);
      },
      (error) => {
        console.error('Error fetching persons:', error);
      }
    );
  }

  filterPersons(event: any): void {
    const query = event.target.value.toLowerCase();
    this.filteredPersons = this.persons.filter(person =>
      person.firtsName.toLowerCase().includes(query)
    );
  }
  
  getPersonName(personId: number): string {
    const person = this.persons.find(p => p.id === personId);
    return person ? person.firtsName : 'Unknown';
  }

  onSubmit(form: NgForm): void {
    if (this.user.id === 0) {
      this.http.post(this.apiUrl, this.user).subscribe(() => {
        this.getUsers();
        form.resetForm();
        this.resetForm();
        Swal.fire('Success', 'User created successfully!', 'success');
      });
    } else {
      this.http.put(`${this.apiUrl}/${this.user.id}`, this.user).subscribe(() => {
        this.getUsers();
        form.resetForm();
        this.resetForm();
        Swal.fire('Success', 'User updated successfully!', 'success');
      });
    }
  }

  editUser(user: any): void {
    this.user = { ...user };
  }

  deleteUser(id: number): void {
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
          this.getUsers();
          Swal.fire('Deleted!', 'User has been deleted.', 'success');
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire('Cancelled', 'Your user is safe :)', 'error');
      }
    });
  }

  resetForm(): void {
    this.user = { id: 0, userName: '', password: '', personId: 0, state: false };
  }
}
