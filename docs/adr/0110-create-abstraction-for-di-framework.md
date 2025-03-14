# Create an abstraction for the used DI framework

## Context and Problem Statement

The framework must be able to register and resolve implementations. But the MFFM framework should be independent from a DI framework. Therefor a layer of abstaction need to be defined. As stated in [ADR-0020](0020-favour-buy-over-make.md), we should reuse existing interfaces but not abuse existing interfaces (LSP).

## Considered Options

* Create custom interfaces
* Reuse default dotnet framework interfaces

## Decision Outcome

The dotnet framework provides a class `System.IServiceProvider` to resolve classes or implementations for interfaces. This interface shall be reused.

A similar interface for service registration does not exist. Therefore a custom interface `IServiceRegistrationAdapter` needs to be defined and used by MFFM core.

### Consequences

==marp==
## Dependency Injection Adapter

- Wiederverwenden  `System.IServiceProvider`
- Eigene Implementierung `IServiceRegistrationAdapter`
- Klasse in MFFM Core zum Registrieren der `internals`
