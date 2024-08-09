import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { SpellComponent } from './spell/spell.component';
import { SpellDetailComponent } from './spell-detail/spell-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    SpellComponent,
    SpellDetailComponent
  ],
  imports: [
    BrowserModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
