import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { SpellDetailsResponse } from '../api-response';


/**
 * The `SpellDetailComponent` is responsible for displaying detailed information about a spell.
 * It receives a spell object as input from its parent component and renders the details
 * in its template.
 */

@Component({
  selector: 'app-spell-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './spell-detail.component.html',
  styleUrls: ['./spell-detail.component.css']
})
export class SpellDetailComponent {
  //The spell property is passed down from a parent component.
  @Input() spellDetails: SpellDetailsResponse | undefined;
}
