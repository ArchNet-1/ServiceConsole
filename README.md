<p align="center">
  <a href="https://archnet-plateform.fr"><img src="https://archnet-plateform.fr/ArchNetLogoWithBck.png" alt="ArchNet" width="240"></a>
</p>

# ArchNet · Console Service

A second console for your own messages, sorted by priority and shown in its own window, each with its call stack.

- Unity **6000.0** or newer · No dependency.
- Part of [ArchNet](https://archnet-plateform.fr), free and open-source Unity packages

## Install

*Window > Package Manager > + > Install package from git URL…*

```
https://github.com/ArchNet-1/ServiceConsole.git
```

## Use

```csharp
using ArchNet.Service;

ConsoleService.Log("Level loaded", eLogPriority.FEATURE);
```

Open *ArchNet > Services > Console*.

## License

See [LICENSE.md](LICENSE.md).
