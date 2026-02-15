# Testing

## Scope in this repository
This repository is a Unity 2021.3 project. It does not contain Avalonia/.NET application projects (`*.sln`, `*.csproj`) in the current snapshot.

The test setup below focuses on Unity smoke coverage and save-load stabilization checks.

## PlayMode smoke tests
Added PlayMode smoke tests in `Assets/Tests/PlayMode/SceneSmokeTests.cs` covering:
- `MenuScene` load
- `SinglePlayBoard` load
- `InventoryScene` load after deleting `stats` save file

## How to run tests

### Unity Editor
1. Open the project in Unity 2021.3 LTS.
2. Open **Window → General → Test Runner**.
3. Select **PlayMode**.
4. Run `SceneSmokeTests` or all PlayMode tests.

### Unity CLI (headless CI-style run)
Use your local Unity executable path:

```bash
"<UNITY_EDITOR_PATH>" \
  -batchmode -nographics -quit \
  -projectPath "$(pwd)" \
  -runTests -testPlatform PlayMode \
  -testResults "TestResults.xml" \
  -logFile "unity-test.log"
```

## Notes
- Tests are deterministic and avoid `Thread.Sleep`; they wait on async scene load completion.
- Save cleanup for the inventory smoke test uses `DataSaver.deleteData("stats")`.
