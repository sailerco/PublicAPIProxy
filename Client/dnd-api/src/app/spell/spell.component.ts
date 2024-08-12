import { Component, Input } from '@angular/core';
import { ApiService } from '../api.service';
import { CommonModule } from '@angular/common';
import { SpellDetailComponent } from '../spell-detail/spell-detail.component';
import { DndResource, SpellDetailsResponse } from '../api-response';

@Component({
  selector: 'app-spell',
  standalone: true,
  imports: [CommonModule, SpellDetailComponent],
  templateUrl: './spell.component.html',
  styleUrls: ['./spell.component.css']
})
export class SpellComponent {
  @Input() spell: DndResource | undefined;
  showDetails: boolean = false;
  spellDetails: SpellDetailsResponse | undefined;

  constructor(private apiService: ApiService) { }

  /**
   * Toggles the visibility of the spell details.
   * If details are to be shown, it triggers the fetch of detailed spell information from the API.
   * This method is called when the user interacts with the spell component.
   */
  toggleDetails() {
    this.showDetails = !this.showDetails;
    if (this.showDetails) {
      this.fetchSpellDetails();
    }
  }

  /**
 * Fetches detailed information about the spell from the backend.
 * The fetched data is stored in the `spellDetails` property.
 */
  fetchSpellDetails() {
    if (this.spell) {
      this.apiService.getSpellDetails(this.spell.index).subscribe(response => {
        this.spellDetails = response;
        console.log(this.spellDetails)
      });
    }
  }
}
