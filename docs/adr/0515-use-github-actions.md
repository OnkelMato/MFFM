# Use github actions for CI/CD pipeline

## Context and Problem Statement

A CI/CD pipeline needs to be implemented to publish the version automatically.

## Considered Options

* Use github actions
* Use Husky
* Use Jenkins

## Decision Outcome

Choosen option: "Use github actions".

* Available OOTB
* Automatic test and push to nuget.org

### Consequences

==marp==
## Build Pipeline: github Actions

- Konfiguration im Source Code
- Versioniert
- Alte Version weiterhin "baubar"
