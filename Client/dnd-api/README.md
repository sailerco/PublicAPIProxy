# DndApi

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 18.1.3.

## Development server

Run `cd .\dnd-api\ dnd` and then `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## The project structure
`App Component` contains the `Spell Component` which contains a `Spell Detail Component`.
The App Component contains a form, through which we can filter a dungeon and dragons spell. The spell can be filtered by the DnD class, magic school and spell level.
When confirming the search a list of fitting spells is fetched from the Backend/API. 
When clicking on a spell we will get more information about the casting and spell - this information is also fetched from the backend.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.


