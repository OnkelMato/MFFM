# MFFM - A MVVM Pattern Implementation for WinForms

The MFFM project is an outtake from a Clean Code training with a question "Can I do MVVM with Windows Forms". The project is a prototype implementation of a binding and separation of concern framework for a MVVM/MFFM pattern.

The term MFFM describes the Model-Form-FormModel, which is similar to the MVVM Model-View-ViewModel term.

As this is a part of a Clean Code training, the documentation tries to explain the ideas and clean code concepts.

## How to use the framework

First of all, the framework uses the dependency inversion principle (DIP) and therefore it uses a dependency injection framework to simpleft dependency inversion. The main application uses the dependency injection extensions from Microsoft.

``` csharp
    // the example uses the Microsoft dependency injection framework

    // here is all the registration logic for the MFFM services and framework
    // this includes the user interface which is formModels and forms
    serviceCollection.ConfigureMffm(typeof(Program).Assembly);

    // Get window manager and run application
    var windowManager = serviceProvider.GetService<IWindowManager>() ??
                        throw new ServiceNotFoundException("cannot find window manager for MFFM pattern");
    windowManager.Run<MainFormModel>();
```

For the main form above a main form model has to be created. The connection between the Form and the FormModel is done by a naming convention.

``` csharp

    // Binding a command to a button with name "SendLogMessage"
    public ICommand SendLogMessage { get; private set; }

    // Binding a string to a textbox with name "SendLogMessage"
    public string LogMessages /* Property with PropertyChanged */

    // Binding a list to a listbox with name "People".
    // The "PeopleSelected" property can be bound to a textbox with name "PeopleSelected"
    public IList<string> People { get; } = new List<string> { "Alice", "Bob", "Charlie" };
    public string PeopleSelected /* Property with PropertyChanged */
```
