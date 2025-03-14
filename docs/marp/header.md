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
