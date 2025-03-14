# Use gitversion for automatic versioning

## Context and Problem Statement

As semantic versioning was decided in [ADR-0200](0200-use-semantic-versioning.md), it is the next step to combine this with git.

## Considered Options

* use `gitversion`

## Decision Outcome

Choosen option: "gitversion"

* Tracking of a version to the source code
* git is the config management tool

### Consequences

==marp==
## gitversion

- Tags für Versionen
- Direkter Rückschluss von Version auf Code
- Build abhängig von Tag oder PR odr Push
- git als Konfigurationsmanagementtool
