import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public apiURL = "http://localhost:5078/api/";

  constructor(private http: HttpClient) { }

  getMagicSchools(): Observable<any> {
    return this.http.get<any>(this.apiURL + "magicSchools");
  }

  getClasses(): Observable<any> {
    return this.http.get<any>(this.apiURL + "classes");
  }

  
  getSpellList(filters: { school: string, level: string, class: string }): Observable<any>{
    const params = new HttpParams()
    .set('school', filters.school)
    .set('level', filters.level)
    .set('classType', filters.class);
    console.log("button clicked")
    return this.http.get<any>(this.apiURL + "spellList", {params})
  }

  getSpellDetails(spellName: string): Observable<any> {
    const url = `${this.apiURL}spells/${spellName}`;
    return this.http.get<any>(url);
  }
}
