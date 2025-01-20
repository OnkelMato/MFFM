# MFFM - A MVVM Pattern Implementation for WinForms

The MFFM project is an outtake from a Clean Code training with a question "Can I do MVVM with Windows Forms". The project is a prototype implementation of a binding and separation of concern framework for a MVVM/MFFM pattern.

The term MFFM describes the Model-Form-FormModel, which is similar to the MVVM Model-View-ViewModel term.

As this is a part of a Clean Code training, the documentation tries to explain the ideas and clean code concepts.

# How to use the framework

First of all, the framework uses the dependency inversion principle (DIP) and therefore it uses a dependency injection framework to simpleft dependency inversion. The main application uses the dependency injection extensions from Microsoft.

``` csharp
            // register services. Bining manager requires the ability to resolve instances
            services.AddSingleton<IBindingManager, BindingManager>(provider =>
            {
                return new BindingManager((type) => provider.GetService(type) ?? throw new ServiceNotFoundException($"Cannot resolve service for {type.FullName}"));
            });

            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<IEventAggregator, EventAggregator>();

            // windows manager can start 
            var windowManager = serviceProvider.GetService<IWindowManager>() ?? throw new ServiceNotFoundException("cannot find window manager for MFFM pattern");
            windowManager.Run<MainFormModel>();
```

# Window Manager

* Knows how to manage windows
* Can do "ShowDialog()"

# Binding Manager

* Know Form and FormModel
* Knows how to bind properties

# Event Aggregator

* Knows how to publish and subscribe events
* Allows to decouple components
* Allows to send messages between components

