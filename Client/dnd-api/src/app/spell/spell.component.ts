import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ApiService } from '../api.service';
import { CommonModule } from '@angular/common';
import { SpellDetailComponent } from '../spell-detail/spell-detail.component';

@Component({
  selector: 'app-spell',
  standalone: true,
  imports: [CommonModule, SpellDetailComponent],
  templateUrl: './spell.component.html',
  styleUrls: ['./spell.component.css']
})
export class SpellComponent {
  @Input() spell: any;
  showDetails: boolean = false;
  spellDetails: any;

  constructor(private apiService: ApiService) {}



  toggleDetails() {
    this.showDetails = !this.showDetails;
    if (this.showDetails) {
      this.fetchSpellDetails();
    }
  }

  fetchSpellDetails() {
    if (this.spell) {
      this.apiService.getSpellDetails(this.spell.index).subscribe(response => {
        this.spellDetails = response;
        console.log(this.spellDetails)
      });
    }
  }
}
