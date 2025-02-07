# Hide as many thing as possible from the user

## Context and Problem Statement

If a framework has too many options and public properties, functions, and classes, a framework seems complex for a user and the user can produce an instable state. Information hiding shall be used to reduce the options for a user and make the system easy to use.

The information hiding principle at its origin only describes that the state of a class should be private to avoid inconsistent state. This concept can be applied to dependency injection as well.

## Considered Options

* Use information hiding principle

## Decision Outcome

Choosen option is: "Use information hiding principle"

* Only the "right way" is provided to the user
* The system is simple and easy to use
* No "paradox of choice", only one way to go
* User cannot create an invalid configuration by default, but override defaults

### Consequences

* The user has less options for example to reuse existing code
