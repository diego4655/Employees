import { Injectable } from '@angular/core';
import { Experience } from '../../domain/experiences';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ExperienceService {

  
  constructor(private http: HttpClient) { }


  getExperience(id: number) {
    return this.http.get<Experience[]>(`${environment.apiUrl}${environment.query}/${id}${environment.queryExperience}`);
  }

  createExperience(id: number, experience: Experience) {
    return this.http.post<Experience>(`${environment.apiUrl}${environment.query}/${id}${environment.queryExperience}`, experience);
  }

  updateExperience(id: number, experience: Experience) {
    return this.http.patch<Experience>(`${environment.apiUrl}${environment.query}/${id}${environment.queryExperience}`, experience);
  }

  deleteExperience(id: number) {
    return this.http.delete<Experience>(`${environment.apiUrl}${environment.query}/${id}${environment.queryExperience}`);
  }
}