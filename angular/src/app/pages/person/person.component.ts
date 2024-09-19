import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';

@Component({
  selector: 'app-person',
  standalone: true,
  imports: [HttpClientModule, FormsModule, CommonModule, RouterModule, DataTablesModule],
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.css']
})
export class PersonComponent implements OnInit {
  persons: any[] = [];
  person: any = {
    id: 0,
    firtsName: '',
    secondName: '',
    firstLastName: '',
    secondLastName: '',
    email: '',
    dateOfBirth: '',
    gender: '',
    address: '',
    typeDocument: '',
    numberDocument: '',
    state: false
  };

  dtOptions: Config = {};

  private apiUrl = 'http://localhost:9191/api/Person';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getPersons();
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
  }

  getPersons(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (persons) => {
        this.persons = persons;
        this.cdr.detectChanges();
        console.log('Persons fetched:', this.persons);
      },
      (error) => {
        console.error('Error fetching persons:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    if (this.person.id === 0) {
      // Agregar un nuevo registro
      this.http.post(this.apiUrl, this.person).subscribe(() => {
        this.getPersons();
        form.resetForm();
        this.resetForm();

        Swal.fire('Success', 'Person created successfully!', 'success');
      });
    } else {
      // Actualizar un registro existente
      this.http.put(`${this.apiUrl}/${this.person.id}`, this.person).subscribe(() => {
        this.getPersons();
        form.resetForm();
        this.resetForm();

        Swal.fire('Success', 'Person updated successfully!', 'success');
      });
    }
  }

  editPerson(person: any): void {
    this.person = { ...person };
  }

  deletePerson(id: number): void {
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
          this.getPersons();
          Swal.fire('Deleted!', 'Your person has been deleted.', 'success');
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire('Cancelled', 'Your person is safe :)', 'error');
      }
    });
  }

  resetForm(): void {
    this.person = {
      id: 0,
      firtsName: '',
      secondName: '',
      firstLastName: '',
      secondLastName: '',
      email: '',
      dateOfBirth: '',
      gender: '',
      address: '',
      typeDocument: '',
      numberDocument: '',
      state: false
    };
  }
}
