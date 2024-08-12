import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpellComponent } from './spell.component';
import { of } from 'rxjs';
import { ApiService } from '../api.service';

class MockApiService {
  getSpellDetails() {
    return of({ index:'fireball', name: 'Fireball', desc: ['A powerful spell']}); // Mocked response
  }
}

describe('SpellComponent', () => {
  let component: SpellComponent;
  let fixture: ComponentFixture<SpellComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SpellComponent],
      providers: [{ provide: ApiService, useClass: MockApiService }]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SpellComponent);
    component = fixture.componentInstance;

    component.spell = { index: 'fireball', name:'fireball' }; // Provide initial input data as a mock
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('should toggle details and fetch spell details', () => {
    spyOn(component, 'fetchSpellDetails').and.callThrough();

    component.toggleDetails(); // Toggle to show details

    expect(component.showDetails).toBeTrue();
    expect(component.fetchSpellDetails).toHaveBeenCalled();

    fixture.detectChanges();

    // Test the spell details
    expect(component.spellDetails).toEqual({ index:'fireball', name: 'Fireball', desc: ['A powerful spell'] });
  });

});
