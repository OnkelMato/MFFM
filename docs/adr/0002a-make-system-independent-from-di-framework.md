# <!-- short title, representative of solved problem and found solution -->

## Context and Problem Statement



## Considered Options

* Create custom (default) DI framework
* Use adapter pattern to attach 3rd party DI frameworks
* Create DI framework specific libs and register services manually

## Decision Outcome

Create DI framework specific libs and register services manually.

### Consequences

* Con: Manually register all required services (no information hiding)
* 
