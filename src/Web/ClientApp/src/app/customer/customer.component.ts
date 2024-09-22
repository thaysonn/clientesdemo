import { Component, TemplateRef, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import {
  CreateCustomerCommand,
  CustomerDto,
  CustomersClient,
  LookupDto,
  UpdateCustomerCommand
} from '../web-api-client';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent implements OnInit {
  customers: CustomerDto[];
  selectedCustomer: any;
  portesEmpresa: LookupDto[] = [];
  newCustomerModalRef: BsModalRef;
  customerModalRef: BsModalRef;
  deleteModalRef: BsModalRef;
  customError: string;

  constructor(
    private client: CustomersClient,
    private modalService: BsModalService
  ) { }

  ngOnInit(): void {
    this.client.get().subscribe({ 
      error: (error) => { console.error(error) },
      next: (result) => {
        this.customers = result.lists;
        this.portesEmpresa = result.portesEmpresa;
      },
    }); 
  }

  showNewCustomerModal(template: TemplateRef<any>): void {
    this.customError = null;
    this.selectedCustomer = { id: 0, nomeEmpresa: '', porteEmpresa: 0 };
    this.newCustomerModalRef = this.modalService.show(template);
    setTimeout(() => document.getElementById('nomeEmpresa').focus(), 250);
  }

  newCustomerCancelled(): void {
    this.newCustomerModalRef.hide();
    this.selectedCustomer = null;
  }

  addCustomer(): void {
    const customer = {
      id: 0,
      nomeEmpresa: this.selectedCustomer.nomeEmpresa,
      porteEmpresa: this.selectedCustomer.porteEmpresa
    } as CustomerDto;


    this.client.get().subscribe({
      error: (error) => { console.error(error) },
      next: (result) => {
        this.customers = result.lists;
        this.portesEmpresa = result.portesEmpresa;
      },
    });


    this.client.create(customer as CreateCustomerCommand).subscribe({
      next: (result) => {
        customer.id = result;
        this.customers.push(customer);
        this.newCustomerModalRef.hide();
        this.selectedCustomer = null;
        this.customError = null;
      },
      error: (error) => {
        const errors = JSON.parse(error.response).errors;
        if (errors && errors.NomeEmpresa)
          this.customError = errors.NomeEmpresa[0];
        setTimeout(() => document.getElementById('nomeEmpresa').focus(), 250);
      }
    });
  }

  showUpdateModal(obj: CustomerDto, template: TemplateRef<any>) {
    this.customError = null;
    this.selectedCustomer = obj;
    this.customerModalRef = this.modalService.show(template);
  }

  updateCustomer() {
    const customer = this.selectedCustomer as UpdateCustomerCommand;

    this.client.update(this.selectedCustomer.id, customer).subscribe({
      next: () => {
        this.customerModalRef.hide();
        this.selectedCustomer = null;
        this.customError = null;
      },
      error: (error) => {
        const errors = JSON.parse(error.response).errors;
        if (errors && errors.NomeEmpresa) {
          this.customError = errors.NomeEmpresa[0];
        }
        setTimeout(() => document.getElementById('nomeEmpresa').focus(), 250);
      }
    });
  }

  confirmDeleteCustomer(obj: CustomerDto, template: TemplateRef<any>) {
    this.selectedCustomer = obj;
    this.deleteModalRef = this.modalService.show(template);
  }

  deleteCustomerConfirmed(): void {
    this.client.delete(this.selectedCustomer.id).subscribe({
      next: () => {
        this.deleteModalRef.hide();
        this.customers = this.customers.filter(t => t.id !== this.selectedCustomer.id);
        this.selectedCustomer = this.customers.length ? this.customers[0] : null;
      },
      error: (error) => { console.error(error) }
    });
  }

  parsePorteEmpresa(idPorteEmpresa: number): string {
    return this.portesEmpresa.find(({ id }) => id === idPorteEmpresa).title;
  }
}
