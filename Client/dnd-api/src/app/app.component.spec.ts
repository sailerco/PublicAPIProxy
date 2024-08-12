import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { of } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { SpellComponent } from './spell/spell.component';
import { SpellDetailComponent } from './spell-detail/spell-detail.component';
import { CommonModule } from '@angular/common';
import { ApiService } from './api.service';

// Mock ApiService
class MockApiService {
  getMagicSchools() {
    return of({ results: [{ index: 'school', name: 'School 1' }] });
  }
  getClasses() {
    return of({ results: [{ index: 'class', name: 'Class 1' }] });
  }
}


describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let apiService: ApiService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommonModule, FormsModule, RouterOutlet, SpellComponent, SpellDetailComponent],
      providers: [{ provide: ApiService, useClass: MockApiService }]
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    apiService = TestBed.inject(ApiService);
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should fetch initial data on initialization', () => {
    spyOn(apiService, 'getMagicSchools').and.callThrough();
    spyOn(apiService, 'getClasses').and.callThrough();

    component.ngOnInit();

    expect(apiService.getMagicSchools).toHaveBeenCalled();
    expect(apiService.getClasses).toHaveBeenCalled();

    fixture.detectChanges();

    expect(component.magicSchools).toEqual([{ index: '', name: 'All Magic Schools' }, { index: 'school', name: 'School 1' }]);
    expect(component.classes).toEqual([{ index: '', name: 'All Classes' }, { index: 'class', name: 'Class 1' }]);
  });
});
