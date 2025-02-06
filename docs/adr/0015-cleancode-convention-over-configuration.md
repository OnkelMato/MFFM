# Use convention over configuration

## Context and Problem Statement

In a framework, a lot of things need to be defined. For exanple which form belongs to a formmodel or which property is bound to which control.

Cf: [Wikipedia - Convention over Configuration](https://en.wikipedia.org/wiki/Convention_over_configuration)

## Considered Options

* Explicit configuration for example during startup, configuration file or attributes.
* Implicit convention for example XyzFormModel uses the XyzForm through naming convention.

## Decision Outcome

Choosen option is: "Implicit convention"

* A naming convention does the magic to define things
* The following namign conventions are used
  * XyzFormModel uses XyzForm (namespace invariant)
  * AbcProperty property from formmodel is mapped to AbcProperty control in form
  * AbcPropertyFoo postfix is used to map to specific field control e.g. ListBox.SelectedItem (form) --> ListBoxSelected (formmodel)
* The following implicit behaviour is defined
  * The latest registration is used first

### Consequences

* A control `FirstnameList` and control `FirstnameListSelected` result in conflicts. `*Selected` is bount to the `SelectedItem` property of the control `FirstnameList` by naming convention.
