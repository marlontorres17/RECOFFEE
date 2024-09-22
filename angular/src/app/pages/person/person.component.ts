import { Component, OnInit, ViewChild, TemplateRef, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { NgbModal, ModalDismissReasons, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-person',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    HttpClientModule,
    NgbModule
  ],
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.css']
})
export class PersonComponent implements OnInit {
  persons: any[] = [];
  person: any = { id: 0, firstName: '', secondName: '', firstLastName: '', secondLastName: '', email: '', dateOfBirth: '', gender: '', cityId: 0, typeDocument: 'CC', numberDocument: '', state: true };
  cities: any[] = [];
  filteredCities: any[] = [];
  selectedCity = '';
  closeResult = '';



  private personApiUrl = 'http://localhost:9191/api/Person';
  private citiesApiUrl = 'http://localhost:9191/api/City';

  @ViewChild('personModal') personModal!: TemplateRef<any>;


  constructor(private http: HttpClient, private cdr: ChangeDetectorRef, private modalService: NgbModal) {}

  ngOnInit(): void {
    this.getPersons();
    this.getCities();
  }

  getPersons(): void {
    this.http.get<any[]>(this.personApiUrl).subscribe(
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

  getCities(): void {
    this.http.get<any[]>(this.citiesApiUrl).subscribe(
      (cities) => {
        this.cities = cities;
        this.filteredCities = [...this.cities];
        this.cdr.detectChanges();
        console.log('Cities fetched:', this.cities);
      },
      (error) => {
        console.error('Error fetching cities:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    const personDto = { ...this.person };

    if (this.person.id === 0) {
      // Add a new person
      this.http.post(this.personApiUrl, personDto).subscribe(() => {
        this.getPersons();
        this.resetForm();
        Swal.fire('Success', 'Person created successfully', 'success');
      });
    } else {
      // Update an existing person
      this.http.put(`${this.personApiUrl}/${this.person.id}`, personDto).subscribe(() => {
        this.getPersons();
        this.resetForm();
        Swal.fire('Success', 'Person updated successfully', 'success');
      });
    }
    this.modalService.dismissAll();
  }

  editPerson(person: any): void {
    this.person = { ...person };
    this.selectedCity = this.getCityName(person.cityId);
    this.modalService.open(this.personModal);
  }

  deletePerson(id: number, deleteType: 'logical' | 'physical'): void {
    Swal.fire({
      title: 'Are you sure?',
      text: `You are about to perform a ${deleteType} deletion. This action cannot be undone!`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        const deleteUrl = `${this.personApiUrl}/${deleteType}/${id}`;
        this.http.delete(deleteUrl).subscribe(() => {
          this.getPersons();
          Swal.fire('Deleted!', `Person has been ${deleteType} deleted.`, 'success');
        });
      } else {
        Swal.fire('Cancelled', 'The person was not deleted.', 'error');
      }
    });
  }

  openModal(): void {
    this.resetForm();
    this.modalService.open(this.personModal);
  }

  filterCities(): void {
    this.filteredCities = this.cities.filter(city =>
      city.name.toLowerCase().includes(this.selectedCity.toLowerCase())
    );
  }

  selectCity(city: any): void {
    this.person.cityId = city.id;
    this.selectedCity = city.name;
    this.filteredCities = [];
  }

  getCityName(cityId: number): string {
    const city = this.cities.find(c => c.id === cityId);
    return city ? city.name : 'Unknown';
  }

  resetForm(): void {
    this.person = { id: 0, firstName: '', secondName: '', firstLastName: '', secondLastName: '', email: '', dateOfBirth: '', gender: '', cityId: 0, typeDocument: 'CC', numberDocument: '', state: true };
    this.selectedCity = '';
  }
}
