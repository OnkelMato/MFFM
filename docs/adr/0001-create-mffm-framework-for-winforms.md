# Create a MFFM framework for WinForms like MVVM for MPF

## Context and Problem Statement

It is state of the art for a WPF application to use the MVVM pattern. As WinForms uses forms instead of views, the approach is called MFFM.

As binding is supported by default in WinForms, a MFFM approach shall be evaluated.

## Considered Options

* Concepts similar to Caliburn.Micro for WPF

## Decision Outcome

Choosen option is: "Concepts similar to Caliburn.Micro" with the following design principles

* Use data binding for a view model to view binding
* Use a window manager for form management outside form model
* No code-behind in form-class
