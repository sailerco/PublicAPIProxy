import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public apiURL = "http://localhost:5078/api/";

  constructor(private http: HttpClient) { }

  /**
   * Retrieves the list of magic schools from the API.
   * @returns An Observable containing the list of magic schools.
   */
  getMagicSchools(): Observable<any> {
    return this.http.get<any>(this.apiURL + "magic-schools");
  }
  /**
   * Retrieves the list of classes from the API.
   * @returns An Observable containing the list of dnd classes.
   */
  getClasses(): Observable<any> {
    return this.http.get<any>(this.apiURL + "classes");
  }

  /**
   * Retrieves a list of spells based on the provided filters.
   * @param filters An object containing the filter criteria for fetching spells.
   * @returns An Observable containing the list of spells.
   */
  getSpellList(filters: { school: string, level: string, class: string }): Observable<any>{
    const params = new HttpParams()
    .set('magicSchool', filters.school)
    .set('level', filters.level)
    .set('classType', filters.class);
    console.log("getting spell list")
    console.log(filters.school, filters.class, filters.level)
    return this.http.get<any>(this.apiURL + "spellList", {params})
  }
  /**
   * Retrieves detailed information about a specific spell.
   * @param spellName - The name of the spell to retrieve.
   * @returns An Observable containing details of the specific spell.
   */
  getSpellDetails(spellName: string): Observable<any> {
    const url = `${this.apiURL}spells/${spellName}`;
    return this.http.get<any>(url);
  }
}
