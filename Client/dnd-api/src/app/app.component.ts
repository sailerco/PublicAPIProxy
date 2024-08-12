import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ApiService } from './api.service';
import { CommonModule, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SpellComponent } from './spell/spell.component';
import { SpellDetailComponent } from './spell-detail/spell-detail.component';
import { DndResource} from './api-response';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, NgFor, FormsModule, SpellComponent, SpellDetailComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'dnd-api';

  magicSchools: DndResource[] = [];
  spellLevels: number[] = Array.from({ length: 10 }, (_, i) => i); // Spell levels from 0 to 9
  classes: DndResource[] = [];
  spells: DndResource[] = [];
  displayedSpells: DndResource[] = [];
  spellsToShow = 10;
  selectedSpell: DndResource | undefined;
  selectedSchool: string = '';
  selectedLevel: string = '';
  selectedClass: string = '';
  noSpellFound = false;
  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.fetchInitalData();
  }

  /**
   * Fetches the initial data for magic schools and classes from the API.
   * The data is used to populate dropdown menus in the form.
   */
  fetchInitalData() {
    this.apiService.getMagicSchools().subscribe(response => {
      this.magicSchools = this.buildDropdownOptions(response.results, 'All Magic Schools');
    });

    this.apiService.getClasses().subscribe(response => {
      this.classes = this.buildDropdownOptions(response.results, 'All Classes');
    });
  }

  /**
 * Builds an array of dropdown options with a default label at the top.
 * 
 * @param filter - Array of options retrieved from the API
 * @param defaultLabel - The label for the default option (e.g., 'All Magic Schools')
 * @returns Array of options including the default label
 */
  buildDropdownOptions(filter: DndResource[], defaultLabel: string) {
    return [{ index: '', name: defaultLabel }, ...filter];
  }

  /**
 * Initializes the list of spells to be displayed based on the current filters.
 */
  loadInitialSpells() {
    this.displayedSpells = this.spells.slice(0, this.spellsToShow);
    console.log(this.displayedSpells)
  }

  /**
   * Loads more spells to be displayed by increasing the number of spells shown.
   */
  loadMoreSpells() {
    const nextIndex = this.displayedSpells.length;
    this.displayedSpells = this.spells.slice(0, nextIndex + this.spellsToShow);
  }

  /**
   * Checks if there are more spells to load.
   * @returns {boolean} - True if there are more spells to load, otherwise false.
   */
  hasMoreSpells(): boolean {
    return this.displayedSpells.length < this.spells.length;
  }

  /**
   * Handles form submission, which filters the spell list based on selected criteria.
   * It calls the `api.service.ts` which fetches the filtered list of spells from the backend and updates the displayed spells.
   */
  submitForm() {
    console.log("Form submitted");

    const filter = {
      school: this.selectedSchool,
      level: this.selectedLevel,
      class: this.selectedClass
    };

    this.apiService.getSpellList(filter).subscribe(response => {
      console.log(response.results)
      this.spells = response.results;
      this.noSpellFound = this.spells.length === 0;
      if (!this.noSpellFound)
        this.loadInitialSpells();
    });
  }
}
