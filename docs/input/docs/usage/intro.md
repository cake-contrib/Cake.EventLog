---
Order: 10
Title: Introduction
---

# Getting Started

> This addin is supported **on Windows only!**

## Including the addin

At the top of your script, just add the following to install the addin:

```
#addin nuget:?package=Cake.EventLog
```

## Usage

The addin exposes a number of top-level aliases, generally with one simple `string`-only overload, and another that accepts arbitrary settings.

You can see the full details of each alias and it's arguments from the *Reference* tab above.

The general pattern is for simple messages:

`WriteToEventLog("Simple Message");`

or for more complex scenarios:

```
WriteToEventLog("Complex message", s => {
    s.WithLogLevel(LogLevel.Information)
     .LogTo("Application")
     .ForMachine("Hostname")
    });
});
```

Note that this also applies for the utility methods:

```
EnsureLogExists("Application");
EnsureLogExists(s => s.LogTo("Application"));
```

## Settings

Log settings are generally included as an `Action` object, so you can use the familiar lambda syntax from the `MSBuild` etc aliases. So a more complex example might be:

```
WriteToEventLog("Event logging from Cake script", s => {
            s.LogTo("Application")
             .WithSourceName("Cake Build")
             .ForMachine(Environment.MachineName);
        });
```

The `EventLogSettings` settings class has [full documentation](xref:Cake.EventLog.EventLogSettings) full documentation as well as extension methods for complete control with the fluent API.

## Permissions

See [permissions](permissions.md).

## Compatibility

Since Mono does not currently support the `System.Diagnostics.EventLog` types, this addin is compatible with Windows only. Note that we will throw a `NotSupportedException` exception if not running on Windows, so it is recommended to use `WithCriteria` on any event logging tasks:

```csharp
Task("WriteLogEntry")
.WithCriteria(IsRunningOnWindows)
.Does(() => {
    // your build code here
});
```

