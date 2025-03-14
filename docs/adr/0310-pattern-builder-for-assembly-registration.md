# Use the builder pattern for form/formmodel mapping

## Context and Problem Statement

One component in the MFFM framework needs to collect all available forms and provide a mapping between them. As we use convention over configuration ADR-0001a, some logic needs to be implemented.

## Considered Options

* use a dictionary like class that can change during runtime
* use a builder pattern to configure the dictionary and create an immutable class

## Decision Outcome

A builder pattern is used so a mutable builder is created which can be changed during bootstrapping. After finishing bootstrapping, the form mapper is returned and immutable.

### Consequences

* Pro: A form cannot be changed during runtime
* Pro: During bootstrapping, a form can be overwritten. E.g. Default implementation with plugin implementation

==marp==
## Builder Pattern

- Registrieren von Form/FormModel aus mehreren Assemblies
- 2-Stufiges erstellen
    - Schritt 1: Configuration
    - Schritt 2: Erzeugen des Ziels
- DefaultFormMapperBuilder -> DefaultFormMapper
- Überschreiben und erweitern der Forms und FormModels
