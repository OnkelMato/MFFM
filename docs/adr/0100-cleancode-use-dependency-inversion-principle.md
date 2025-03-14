# Use dependency inversion principle (DIP)

## Context and Problem Statement

The dependency inversion principle is crucial for extensible applications. It decouples the usage from implementation and and uses abstractions for communication.

A different implementation can change the behaviour of specific party without changing the entire behaviour.

## Considered Options

* Use dependency inversion principle and abstractions
* Use dependencies with "new" in a class directly

## Decision Outcome

Choosen option is: "Use dependency inversion principle and abstractions"

* Dependency to an abstraction supports different implementations for different purpose
* Makes the system unaware of the concrete implementation
* the system is more testable as the dependency can be mocked

### Consequences

==marp==
# Dependency Injection

## Abhängigkeiten, aber wovon?

---
## DIP (Dependency Inversion Principle)

- Unabhängig von Implementierung 
- Austauschbarkeit (neue Implementierung)
- Erweiterbarkeit (zusätzliche Implementierung)
- Achtung: Eigenheiten des DI Frameworks
