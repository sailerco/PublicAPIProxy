# DndApi

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 18.1.3.

## The project structure
`App Component` contains the `Spell Component` which contains a `Spell Detail Component`.
The App Component contains a form, through which we can filter a dungeon and dragons spell. The spell can be filtered by the DnD class, magic school and spell level.
When confirming the search, a list of fitting spells is fetched from the Backend/API. 
When clicking on a spell, we will get more information about spell - this information is also fetched from the backend.

The project uses Angular and the EsLinter.

## Running the project

Execute `cd .\dnd-api\` and then `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.


## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).


