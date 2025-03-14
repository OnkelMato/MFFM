---
marp: true
theme: default
author: Onkel Mato (Thomas Ley)
---

# MFFM

## Ein MVVM Framework für Windows Forms

---
## Windows Forms

- Seit Februar 2002, .NET v1.0
- Basiert auf Windows API
- In .Net Core 3 re-implementiert
- Neuerungen bis dato (.NET 9)
    - Async
    - Runtime Designer
    - Skalierung, DPI, GDI+ Effekte

---
## Windows Forms Binding

- Binding Source für RAD/Designer
- Datenbindung DataSource -> Controls
- ICommand seit .NET 8.0 (.NET 7.0 Preview)
- `[Bindable(true)]`-Attribut

---
## Prototyp

- Form1 kennt Form1Model
- Binding in Form1 definiert
- Window Manager für Abstraktion von FormX

=> "Refactoring from scratch"

---
# Die Grundlagen

## und das Ziel des Frameworks

---
## MADR für Entscheidungen

- Entscheidungen werden notiert
- Business- und Architekturentscheidungen
- MADR für eine einfache Notation
- Entscheidungen werden mit dem Code versioniert
---
## MARP für Präsentationen

- Präsentation liegt im git
- Folien gemeinsam mit MADR
- Dynamisches erstellen der Präsentation
- Design zentral austauschbar
---
## MFFM Framework

- MVVM bei WPF
- Trennung von UI und Model
- Build-In Datenbindung nutzen
- Erweiterbarer Core

---
## MFFM Erweiterungen

- Benutzerdefinierte Controls (LoB, Xceed et.al.)
- Austauschbarkeit der Form
- Erweiterbarkeit der Datenbindung (Icons, Font)
- Attribute-Binding oder Internationalisierung
---
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
---
## Convention over Configuration

- Implizit durch Namenskonvention
- Beispiel: Textbox `Foo`/Eigenschaft `Text` => FormModel Eigenschaft `Foo`
- "Magie, die funktioniert"
- Weniger "Boilerplate-Code"
---
## Favour Buy over Make

- .NET Features werden verwendet z.B. DataBinding
- Interfaces aus .NET "Core" werden wiederverwendet
- Eigenes DataBinding wenn nicht unterstützt
- Eigene Interfaces wenn nötig
---
# Dependency Injection

## Abhängigkeiten, aber wovon?

---
## DIP (Dependency Inversion Principle)

- Unabhängig von Implementierung 
- Austauschbarkeit (neue Implementierung)
- Erweiterbarkeit (zusätzliche Implementierung)
- Achtung: Eigenheiten des DI Frameworks
---
## Information Hiding Principle

- Möglichst viel auf `internal`
- Registrierung mit korrekter Verwendung
- "Works by Default"
---
## Dependency Injection Adapter

- Wiederverwenden  `System.IServiceProvider`
- Eigene Implementierung `IServiceRegistrationAdapter`
- Klasse in MFFM Core zum Registrieren der `internals`
---
## DI Adapter

- Registrierung ist Framework abhängig
- Adapter Framework zu `IServiceRegistrationAdapter`
- Resolve is Framework unabhängig
- Nur Resolve nutzt den `System.IServiceProvider`
- Eigenes Projekt pro Framework
---
## Starten der Applikation

- Information Hiding durch Erweiterungsmethode
- `TheContainer`.Run<MainForm>()
- Window Manager etc. werden verborgen
- Nutzer kann eigene Erweiterungsmethode schreiben
---
# Datenbindung

## Oder wie bekomme ich die Daten in die UI

---

## .NET 9.0 DataBinding
- .NET Framework 1.1
- Datenbindung wiederverwenden
- `Control.DataBindings`
- new Binding("Control Property", DataSource, "DataSource Property")
---
## Code-Based Binding
- Designer vs. Code
- BindableButton als "Workaround" (.NET Framework 4.8.1)
- Code-based ist flexibler
- Form hat code-behind wenn nicht OOTB Binding
---
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
---
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
---
## Window Manager

- **Kann Formulare erzeugen, binden und verwalten**
- Kennt die Forms
- Kennt die FormModels
- Kennt den Binding Manager
---
## Builder Pattern

- Registrieren von Form/FormModel aus mehreren Assemblies
- 2-Stufiges erstellen
    - Schritt 1: Configuration
    - Schritt 2: Erzeugen des Ziels
- DefaultFormMapperBuilder -> DefaultFormMapper
- Überschreiben und erweitern der Forms und FormModels
---
## FormModel und Form Mapping

- Ein FormModel braucht eine Form
- Eine Form kann überschrieben/ersetzt werden
- Namespace wird nicht verwendet
- `GetType().Name` als identifizierendes Merkmal
- `FormModel` = `Form` + "Model"
---
## Versionierung

- Semantic Versioning statt Calendar Versioning
- X.0.0 => Breaking Changes
- 0.X.0 => Neue Features
- 0.0.X => Bugfixes
- Major vor Minor vor Patch
---
## gitversion

- Tags für Versionen
- Direkter Rückschluss von Version auf Code
- Build abhängig von Tag oder PR odr Push
- git als Konfigurationsmanagementtool
---
## Nuget Packages

- nuget Packages
- Push auf nuget.org
---
## Build Pipeline: github Actions

- Konfiguration im Source Code
- Versioniert
- Alte Version weiterhin "baubar"
---
# so long and thanks for all the fish

## Macht’s gut, und danke für den Fisch

