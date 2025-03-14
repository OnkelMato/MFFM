# <!-- short title, representative of solved problem and found solution -->

## Context and Problem Statement



## Considered Options



## Decision Outcome



### Consequences

==marp==
## Control Datenbindung

- Kennt das Control und weiß, was gebunden wird
    - List, ListItems
    - TreeView, TreeViewSelected
    - Command + CommandParameter
- if-else Anti-Pattern

---
## IControlBinding
- Strategy Pattern + Chain of Responsibility
- Strategie je nach Control
- Strategie je nach Eigenschaft
- Erweiterbarkeit durch zusätzliche Registrierung

---
## IControlBinding v2
- Prüfen ob Binding schon existiert
- Möglichkeit, Eigenschaften anders zu binden
    - Implementieren eines `BindToControlAttribute`
    - Binden an andere Property Namen (neue Konvention)
