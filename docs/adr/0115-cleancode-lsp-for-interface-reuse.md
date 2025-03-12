# Liskov Substitution Principle (LSP)

## Context and Problem Statement

While reusing interfaces, it could happen, that interfaces are misused for another purpose. This happens when interfaces have methods which match the functionality used in another context. Or this happens when an interface has a good name but does not completly match the needs.

## Decision Outcome

Use the LSP and make sure the implementation of the reused interfaces does not violate the LSP.

### Consequences
