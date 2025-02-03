# MFFM - A MVVM Pattern Implementation for WinForms

The MFFM project is an extraction from a Clean Code training with a question "Can I do MVVM with Windows Forms?". The project is an implementation of clean code concepts, design pattern, and architectural decisions to implement a MVVM framework for Windows Forms.

The term MFFM describes the Model-Form-FormModel, which is similar to the MVVM Model-View-ViewModel term. The binding manager uses `form model` property names to bind it to the corresponding control in the `form`. As a conclusion of the dynamic aproach, extensibility, and dependency inversion principle, a highly customizable and extensible framework is created.

The [ADRs](/docs/adr/README.md) describe the design decisions, design pattern, and clean code principles. As this is a part of a Clean Code training, the documentation tries to explain the ideas and clean code concepts.

## How to use the framework

First of all, the framework uses the dependency inversion principle (DIP) and therefore it uses a dependency injection framework to simplify dependency inversion. The default application uses the dependency injection extensions framework from Microsoft. Another example shows an [autofac](https://autofac.org/) minimal version which orchestrates the `form`/`form model` and the extensions project.

``` csharp
    // Initialize Dependency Injection (Microsoft Container Builder)
    var serviceCollection = new ServiceCollection();

    // register the services for the demo application that are injected into the form models
    serviceCollection.ConfigureDemoAppServices();

    // here is all the registration logic for the MFFM services and framework
    // this includes the user interface which is formModels and forms from the main assembly
    serviceCollection.ConfigureMffm(typeof(Program).Assembly);

    // create the service provider aka container
    var serviceProvider = serviceCollection.BuildServiceProvider();

    // Run the application directly on service provider
    serviceProvider.Run<MainFormModel>();
```

For the main form above a main form model has to be created. The connection between the Form and the FormModel is done by a naming convention.

``` csharp
    // Binding a command to a button with name "SendLogMessage"
    public ICommand SendLogMessage { get; private set; }

    // Binding a string to a textbox with name "LogMessages"
    public string LogMessages /* Property with PropertyChanged */

    // Binding a custom DTO to a custom control with name "Coordinates" (see extensions sample)
    public Coordinate Coordinate /* Property with PropertyChanged */

    // Binding a list to a listbox with name "People".
    // The "PeopleSelected" property can be bound to a textbox with name "PeopleSelected"
    public IList<string> People { get; } = new List<string> { "Alice", "Bob", "Charlie" };
    public string PeopleSelected /* Property with PropertyChanged */
```

## Projects

* Mffm => Main project with the framework
* Mffm.Contracts => Contracts for the framework used by the application and the framework
* Mffm.DependencyInjection.Autofac => Autofac implementation for the framework
* Mffm.DependencyInjection.Microsoft.Extensions => Microsoft DI implementation for the framework
* Mffm.Samples => Sample application using the framework
* Mffm.Samples.Autofac => Sample application using the framework with Autofac
* Mffm.Samples.Extensions => Sample of extensions

## Extensibility

The project `Mffm.Samples.Extensions` demonstrates the following extensibility points:

### Override a default form

The default person edit form is overwritten with a custom form. The custom form is registered during the "assembly registration" for the MFFM framework. This is important for the IFormMapper default implementation. Otherwise a custom form mapper has to be implemented.

### Map a custom control

The custom control `GeolocaitonControl` represents a custom control which is used (in the person edit form). The binding is registered during the "assembly registration" for the MFFM framework but can be done directly in the service registration.

