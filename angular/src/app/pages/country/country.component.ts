import { Component, OnInit, ChangeDetectorRef} from '@angular/core';


import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';

@Component({
  selector: 'app-country',
  standalone: true,
  imports: [HttpClientModule, FormsModule, CommonModule, RouterModule, DataTablesModule],  // Asegúrate de que esté importado aquí
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.css']
})
export class CountryComponent implements OnInit {
  countrys: any[] = [];
  country: any = { id: 0, name: '', state: false };

  dtOptions: Config = {};

  

  private apiUrl = 'http://localhost:9191/api/Country';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getCountrys();
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
  }

 

  getCountrys(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (countrys) => {
        this.countrys = countrys;
        this.cdr.detectChanges();
        console.log('countrys fetched:', this.countrys);
      },
      (error) => {
        console.error('Error fetching countrys:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    if (this.country.id === 0) {
      // Agregar un nuevo registro
      this.http.post(this.apiUrl, this.country).subscribe(() => {
        this.getCountrys();  // Refresca la lista de roles
        form.resetForm();  // Resetea el formulario visualmente
        this.resetForm();  // Resetea el objeto role
        Swal.fire('Success', 'Country created successfully!', 'success');
      });
    } else {
      // Actualizar un registro existente
      this.http.put(`${this.apiUrl}/${this.country.id}`, this.country).subscribe(() => {
        this.getCountrys();
        form.resetForm();
        this.resetForm();
        Swal.fire('Success', 'Country update successfully!', 'success');
        
      });
    }
  }
  

  editCountry(country: any): void {
    this.country = { ...country };
  }

  deleteCountry(id: number): void {
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
          this.getCountrys();
          Swal.fire(
            'Deleted!',
            'Your Country has been deleted.',
            'success'
          );
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your Country is safe :)',
          'error'
        );
      }
    });
  }

  resetForm(): void {
    this.country = { id: 0, name: '', state: false };
  }
  
}
