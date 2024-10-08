import { Component, OnInit, ViewChild, TemplateRef, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { NgbModal, ModalDismissReasons, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-rovi',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    HttpClientModule,
    NgbModule
  ],
  templateUrl: './rovi.component.html',
  styleUrls: ['./rovi.component.css']
})
export class RoviComponent implements OnInit {
  rovis: any[] = [];
  roles: any[] = [];
  views: any[] = [];
  rovi: any = { id: 0, state: true, roleId: 0, viewId: 0 };
  closeResult = '';
  private apiUrl = 'http://localhost:9191/api/RoleView';
  private roleApiUrl = 'http://localhost:9191/api/Role';
  private viewApiUrl = 'http://localhost:9191/api/View';

  @ViewChild('roviModal') roviModal!: TemplateRef<any>;

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef, private modalService: NgbModal) {}

  ngOnInit(): void {
    this.getRovis();
    this.getRoles();
    this.getViews();
  }

  getRovis(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (rovis) => {
        this.rovis = rovis;
        this.cdr.detectChanges();
      },
      (error) => {
        console.error('Error fetching role views:', error);
      }
    );
  }

  getRoles(): void {
    this.http.get<any[]>(this.roleApiUrl).subscribe(
      (roles) => {
        this.roles = roles;
        this.cdr.detectChanges();
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
        this.cdr.detectChanges();
      },
      (error) => {
        console.error('Error fetching views:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    const roviDto = { ...this.rovi };

    if (this.rovi.id === 0) {
      this.http.post(this.apiUrl, roviDto).subscribe(() => {
        this.getRovis();
        form.resetForm();
        this.resetForm();
        Swal.fire('Success', 'Role view created successfully', 'success');
      });
    } else {
      this.http.put(`${this.apiUrl}/${this.rovi.id}`, roviDto).subscribe(() => {
        this.getRovis();
        form.resetForm();
        this.resetForm();
        Swal.fire('Success', 'Role view updated successfully', 'success');
      });
    }
  }

  openModal(rovi?: any): void {
    if (rovi) {
      this.rovi = { ...rovi };
    } else {
      this.resetForm();
    }
    this.modalService.open(this.roviModal, { ariaLabelledBy: 'modal-basic-title' }).result.then(
      (result) => {
        this.closeResult = `Closed with: ${result}`;
      },
      (reason) => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
  }

  editRovi(rovi: any): void {
    this.openModal(rovi);
  }

  deleteRovi(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You won\'t be able to revert this!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
          this.getRovis();
          Swal.fire('Deleted!', 'Role view has been deleted.', 'success');
        });
      }
    });
  }

  resetForm(): void {
    this.rovi = { id: 0, state: true, roleId: 0, viewId: 0 };
  }

  getRoleName(roleId: number): string {
    const role = this.roles.find(r => r.id === roleId);
    return role ? role.name : 'N/A';
  }

  getViewName(viewId: number): string {
    const view = this.views.find(v => v.id === viewId);
    return view ? view.name : 'N/A';
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
