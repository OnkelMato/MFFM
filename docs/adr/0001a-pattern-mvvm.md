# Use MVVM pattern for separation of concerns

## Context and Problem Statement

The MVVM pattern separates the user interface from the business logic. There are three roles defined

* `model`: All the application and business logic. It is not a single class but describes layers below.
* `view`: The visual representation for the user which represents data but does not anythig about the dta itself.
* `viewmodel`: The backend logic which knows how to collect and prepare the data.

As Windows Forms is using form, the pattern shoud be renamed to MFFM.

## Considered Options

Implement MFFM

## Decision Outcome



### Consequences

