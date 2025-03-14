# Use MVVM pattern

## Context and Problem Statement

The MVVM pattern separates the user interface from the business logic. There are three roles defined:

* `Model`: All the application and business logic. It is not a single class but describes layers below.
* `View`: The visual representation for the user which represents data but does not anythig about the dta itself.
* `ViewModel`: The backend logic which knows how to collect and prepare the data.

As Windows Forms is using form, the pattern shoud be renamed to MFFM.

## Considered Options

* Implement MVVM for Windows Forms and call it MFFM.

## Decision Outcome

Choosen option is: "Implement MVVM for Windows Forms and call it MFFM"

* MFFM indicates that this is related to windows forms
* Supports the "zero business logic in UI"
* Supports the "form model does nit know UI"

### Consequences

* The implementation will be a framework with classes to implement some of the design principles.

==marp==
## Design Pattern: MVVM

- Model, View, ViewModel
- Model: Geschäftslogik
- View: Benutzeroberfläche
- ViewModel: Transformation Model -> View

---
## Vorteile

- UI kann ausgetauscht werden z.B. Accessibility
- UI kennt die Geschäftslogik nicht
- Geschäftslogik unabhängig von Oberfläche
- Datentransformation (VM) spazialisiert für UI
