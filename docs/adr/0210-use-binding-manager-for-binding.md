# <!-- short title, representative of solved problem and found solution -->

## Context and Problem Statement

* Glue form and formmodel
* 

## Considered Options



## Decision Outcome



### Consequences

==marp==
## Problem der Datenbindung

- Form kennt FormModel
- oder FormModel kennt Form
- Anhängigkeit auf gleicher "Ebene" in der Architektur
- Probleme: Z.B. bei `new()`
- Lösung: Unabhängigkeit Form und FormModel

---
## Binding Manager

- **Stellt die Verbindung von Form und FormModel her**
- Logik um die Controls and das FormModel zu binden
- Kennt beide Klassen aus einer "Ebene"
- Fundamental theorem of software engineering (FTSE)
- "We can solve any [dependency] problem by introducing an extra level of indirection."
