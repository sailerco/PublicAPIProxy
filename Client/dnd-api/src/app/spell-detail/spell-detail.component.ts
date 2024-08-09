import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-spell-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './spell-detail.component.html',
  styleUrls: ['./spell-detail.component.css']
})
export class SpellDetailComponent {
  @Input() spell: any;
}
