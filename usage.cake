#r "dist/build/Cake.EventLog/Cake.EventLog.dll"

var target = Argument<string>("target", "Usage");

Task("Usage")
    .Does(() => {
        EnsureLogExists("Application");
        EnsureSourceExists("Cake Build");
        WriteToEventLog("Event logging from Cake script");
    });

Task("Settings")
    .Does(() => {
        EnsureLogExists(s => s.LogTo("Application"));
        EnsureSourceExists(s => s.WithSourceName("Cake Build"));
        WriteToEventLog("Event logging from Cake script", s => {
            s.LogTo("Application")
             .WithSourceName("Cake Build")
             .ForMachine(Environment.MachineName);
        });
    });

RunTarget(target);