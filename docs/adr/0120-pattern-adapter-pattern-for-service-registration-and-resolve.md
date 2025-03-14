# <!-- short title, representative of solved problem and found solution -->

## Context and Problem Statement



## Considered Options



## Decision Outcome



### Consequences

==marp==
## DI Adapter

- Registrierung ist Framework abhängig
- Adapter Framework zu `IServiceRegistrationAdapter`
- Resolve is Framework unabhängig
- Nur Resolve nutzt den `System.IServiceProvider`
- Eigenes Projekt pro Framework
