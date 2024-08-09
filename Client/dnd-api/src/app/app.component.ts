import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';
import { CommonModule, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SpellComponent } from './spell/spell.component';
import { SpellDetailComponent } from './spell-detail/spell-detail.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, NgFor, FormsModule, SpellComponent, SpellDetailComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'project';

  magicSchools: any[] = [];
  spellLevels: number[] = Array.from({ length: 10 }, (_, i) => i); // Spell levels from 0 to 9
  classes: any[] = [];
  spells: any[] = [];
  selectedSpell: any;
  selectedSchool: string = '';
  selectedLevel: string = '';
  selectedClass: string = '';
  noSpellFound = false;
  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.fetchMagicSchools();
    this.fetchClasses();
  }

  fetchMagicSchools() {
    this.apiService.getMagicSchools().subscribe(response => {
      this.magicSchools = [{ index: '', name: 'All Magic Schools' }, ...response.results];
    });
  }

  fetchClasses() {
    this.apiService.getClasses().subscribe(response => {
      this.classes = [{ index: '', name: 'All Classes' }, ...response.results];;
    });
  }

  submitForm() {
    console.log("Form submitted");

    const data = {
      school: this.selectedSchool,
      level: this.selectedLevel,
      class: this.selectedClass
    };

    this.apiService.getSpellList(data).subscribe(response => {
      this.spells = response.results;
      console.log('Spells from backend:', this.spells);
      if(this.spells.length == 0){
        this.noSpellFound = true;
      }else this.noSpellFound = false
    });
  }
}
