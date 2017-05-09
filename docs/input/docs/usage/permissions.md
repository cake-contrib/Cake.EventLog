---
Order: 40
Title: Permissions
---

## Permissions

Note that access to the event log is not available for all users and in particular creating logs or sources may only be possible for administrators. The addin will attempt to warn you if you may be attempting to do something that may require evaulation.

Since Cake is an automation-centric and often runs unattended, the addin will attempt to continue for some permissions problems, such as creating an event source even if it is unable to read all existing sources. As such, it is recommended to test your build scripts and check for any warnings before using this addin in a production-like environment. In particular, you will see a warning message reading *"It appears the script is not running with privileges"* whenever the addin encounters a permissions issue (recoverable or not). 

> [!NOTE]
> Finer-grained control over error handling is planned for a future release.