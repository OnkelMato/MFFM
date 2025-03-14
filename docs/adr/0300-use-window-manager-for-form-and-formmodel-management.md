# Use window manager for form and formmodel management

## Context and Problem Statement

As stated in [ADR-0005](0005-create-mffm-framework-for-winforms.md), the form and formmodel do not know each other. Theoretically, any formmodel can be bound to any form, independent if this makes sense from a business perspective.

A "man in the middle" needs to connect these two classes.

## Considered Options

* Window Manager to 

## Decision Outcome



### Consequences

==marp==
## Window Manager

- **Kann Formulare erzeugen, binden und verwalten**
- Kennt die Forms
- Kennt die FormModels
- Kennt den Binding Manager
