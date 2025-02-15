# Favour buy over make

## Context and Problem Statement

The market provides a lot of ready-to-use products but also interfaces in several libraries. It needs to be decided if the predefined interfaces shall be used or a custom interface shall be created.

## Considered Options

* Only rely on self-defined interface and do not reuse existing interfaces
* Reuse predefined interfaces from dotnet core framework
* Reuse as many interfaces as possible from dotnet core framework and extensions

## Decision Outcome

Choosen option is: "Reuse predefined interfaces from dotnet core framework".

* Easier to be reused by 3rd party frameworks
* No "adapter pattern" is required to connect a self-defined interface to a framework
* Interfaces from dotnet core framework are always available in other applications
* Use self-defined interfaces for non-dotnet-core reduces dependencies
* Use self-defined interfaces for non-dotnet-core increases extensibility
* Adapter from 3rd party libries to dotnet core framework libraries are usually already available

### Consequences
