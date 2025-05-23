import { Component } from '@angular/core';
import { Employee } from '../domain/employee';
import { EmployeeService } from '../services/employee/employee.service';
import { Experience } from '../domain/experiences';
import { ExperienceService } from '../services/experience/experience.service';

@Component({
  selector: 'app-principalview',
  templateUrl: './principalview.component.html',
  styleUrl: './principalview.component.css'
})
export class PrincipalviewComponent {

  employees: Employee[] = [];
  experiences: Experience[] = [];
  employeeSearch: string = '';
  displayModal: boolean = false;
  displayModalUpdate: boolean = false;
  displayModalExperience: boolean = false;
  displayModalExperienceUpdate: boolean = false;
  idCandidateTemp: number = 0;
  expandedRows = {};
  newEmployee: Employee = {
    idCandidate: 0,
    name: '',
    surname: '',
    birthdate: new Date(),
    email: '',    
    insertDate: new Date(),
    modifyDate: new Date()
  };
  newExperience: Experience = {
    idCandidateExperience: 0,
    company: '',
    job: '',
    description: '',
    salary: 0,
    beginDate: new Date(),
    endDate: new Date(),
    insertDate: new Date(),
    modifyDate: new Date()
  }
  constructor(private employeeService: EmployeeService, private experienceService: ExperienceService) { }

  ngOnInit() {
    this.GetAllEmployees();
  }


  GetAllEmployees(){
    this.employeeService.getEmployees().subscribe(
      (employees: Employee[]) => {        
        this.employees = employees;
      }
    );
  }

  expandAll() {
    this.expandedRows = this.employees.reduce((acc, p) => {
      acc[p.idCandidate] = true;
      return acc;
    }, {} as { [key: number]: boolean });
  }

  collapseAll() {
    this.expandedRows = {};
  }

  onRowExpand(event: any) { 
    this.idCandidateTemp = event.data.idCandidate;
    this.onRowCollapse(event);    
    this.experienceService.getExperience(event.data.idCandidate).subscribe(
      (experience: Experience[]) => {
        this.experiences = experience;
        console.log(this.experiences);
      }
    );
  }
  searchEmployee(){
    if(this.employeeSearch.length > 0){
      this.employeeService.getEmployee(this.employeeSearch).subscribe(
        (employee: Employee) => {
          this.employees = [employee];
        }
      );
      return;
    }
    alert("diligencie el campo de busqueda");
  }


  saveEmployee(){
    this.employeeService.createEmployee(this.newEmployee).subscribe(
      (employee: any) => {
        this.employees.push(employee);
        this.hideModalDialog();
        this.GetAllEmployees();
      }
    );
  }



  saveExperience(){
    this.experienceService.createExperience(this.idCandidateTemp, this.newExperience).subscribe(
      (experience: any) => {
        this.experiences.push(experience);
        this.hideModalDialogExperience();
        this.collapseAll();
        this.GetAllEmployees();
      }
    );
  }

  onRowCollapse(event: any) {         
    this.experiences = [];    
  }

  createEmployee(employee: Employee) {
    
  }
  showModalDialog() {
    this.displayModal = true;
  }
  showModalDialogUpdate() {
    this.displayModalUpdate = true;
  }
  showModalDialogExperience() {
    this.displayModalExperience = true;
  }
  showModalDialogExperienceUpdate() {
    this.displayModalExperienceUpdate = true;
  }



  hideModalDialog() {
    this.displayModal = false;
    this.resetForm();
  }
  hideModalDialogUpdate() {
    this.displayModalUpdate = false;
    this.resetForm();
  }
  hideModalDialogExperience() {
    this.displayModalExperience = false;
    this.resetFormExperience();
  }

  hideModalDialogExperienceUpdate() {
    this.displayModalExperienceUpdate = false;
    this.resetFormExperience();
  }


  editEmployee(employee: Employee){
    this.newEmployee = employee;    
    this.showModalDialogUpdate();    
  }

  editExperience(experience: Experience){
    this.newExperience = experience;
    this.showModalDialogExperienceUpdate();
  }


  updateEmployee(){
    this.employeeService.updateEmployee(this.newEmployee.idCandidate, this.newEmployee).subscribe(
      (response: any) => {
        this.employees = this.employees.map(e => e.idCandidate === this.newEmployee.idCandidate ? response : e);
        this.hideModalDialogUpdate();
        this.GetAllEmployees();
      }
    );
  }

  updateExperience(){
    console.log(this.newExperience);
    this.experienceService.updateExperience(this.newExperience.idCandidateExperience, this.newExperience).subscribe(
      (response: any) => {
        this.experiences = this.experiences.map(e => e.idCandidateExperience === this.newExperience.idCandidateExperience ? response : e);
        this.hideModalDialogExperienceUpdate();
        this.collapseAll();
        this.GetAllEmployees();
      }
    );
  }


  deleteEmployee(employee: Employee){
    this.employeeService.deleteEmployee(employee.idCandidate).subscribe(
      (response: any) => {
        this.employees = this.employees.filter(e => e.idCandidate !== employee.idCandidate);
      }
    );
  }

  deleteExperience(experience: Experience){
    this.experienceService.deleteExperience(experience.idCandidateExperience).subscribe(
      (response: any) => {
        this.experiences = this.experiences.filter(e => e.idCandidateExperience !== experience.idCandidateExperience);
        this.collapseAll();
        this.GetAllEmployees();
      }
    );
  }


  resetForm() {
    this.newEmployee = {
      idCandidate: 0,
      name: '',
      surname: '',
      birthdate: new Date(),
      email: '',      
      insertDate: new Date(),
      modifyDate: new Date()
    };
  }

  resetFormExperience(){
    this.newExperience = {
      idCandidateExperience: 0,
      company: '',
      job: '',
      description: '',
      salary: 0,
      beginDate: new Date(),
      endDate: new Date(),
      insertDate: new Date(),
      modifyDate: new Date()
    }
  }
}
