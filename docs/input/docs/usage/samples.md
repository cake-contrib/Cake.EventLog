---
Order: 50
Title: Samples
---

# Sample snippets

Write to event log using defaults:

```csharp
WriteToEventLog("Simple Message");
```

Write to event log, including log details:

```csharp
WriteToEventLog("Complex message", s => {
    s.WithLogLevel(LogLevel.Information)
     .LogTo("Application")
     .ForMachine("Hostname")
    });
});
```

Ensure a named log exists:

```csharp
EnsureLogExists("Application");
```

Ensure a log exists on a target machine:

```csharp
EnsureLogExists(s =>
    s.LogTo("Application")
        .ForMachine("Remote-Hostname"));
```

Ensure a source exists:
```csharp
ctx.EnsureSourceExists("Cake Build");
```