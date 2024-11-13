# UnrealReplayReader

---

```
                       var settings = new ReplayReaderSettings
                        {
                            IsDebug = true,
                            Logger = null,
                            UseCheckpoints = false,
                            ExportConfiguration = ReplayExportConfiguration.FromAssembly(typeof(GameStateExport))
                        };

                        var replay = FortniteReplayReader.FromFile(filePath, settings);```

## Build

`dotnet build`

## Test

`dotnet test`
