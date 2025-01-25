# <!-- short title, representative of solved problem and found solution -->

## Context and Problem Statement

The binding of the form model to the form needs to be done with code -there is no magic in coding. At a certain place in code, a binding need to be created `b = new Binding(..)` and the binding added to the form or control `.DataBindings.Add(b)`.

## Considered Options

* use bindingsource and ui for data binding
* use code for data binding

## Decision Outcome

Use code for data binding as it is more flexible.

* pro: can use the model itself as command parameter in command binding
* pro: can bind all properties and not the default suggedted by the system

### Consequences

It simplify binding, a deparate component should be written to handle the data binding. Data binding in code behind would break the separation of concerns ADR-0007 and no code behind ADR-0001.
