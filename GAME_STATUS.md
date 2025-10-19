# Arrgh Pirates Project Status

## Project Overview
- Unity project targeting a pirate-themed experience with multiple scenes (`MenuScene`, `SinglePlayBoard`, `InventoryScene`).
- Core gameplay scripts sit under `Assets/Scripts`, covering battle board targeting, ship and pirate inventory management, and persistence helpers.
- Assets include reusable prefabs in `Assets/Resources/Prefabs` and name lists in `Assets/Names` for procedural generation.

## Implemented Systems
### Menu & Scene Flow
- `MenuScript` provides a simple scene switcher via `SceneManager.LoadScene`, allowing navigation from the main menu to other scenes.【F:Assets/Scripts/Menu/MenuScript.cs†L5-L12】

### Battle Board Targeting
- `GameManager` boots the targeting loop by launching `SelectTarget.Shoot()` when the board scene loads.【F:Assets/Scripts/GameManager.cs†L5-L15】
- `SelectTarget` wires UI buttons to move a targeting cursor across a 20×12 tile grid, toggling materials on `TileScript` tiles and firing a bullet animation coroutine when the Shoot button is pressed.【F:Assets/Scripts/SelectTarget.cs†L6-L130】
- `TileScript` maintains material references for water states and a dictionary tracking shot history for each tile.【F:Assets/Scripts/TileScript.cs†L5-L47】

### Inventory & Ship Management
- `Inventory` drives dock setup, ship bag population, pirate rosters, and saving back to disk. It spawns prefabs based on `PlayerStatModel` data and maintains lists of instantiated ships/pirates for persistence.【F:Assets/Scripts/Inventory/Inventory.cs†L11-L167】
- `ShipScript` handles docking selected ships, instantiating replacements, and exchanging ships between the dock and bag.【F:Assets/Scripts/ShipScript.cs†L5-L43】
- Drag-and-drop interactions (`DragAndDrop`, `ChangeShipScript`) allow moving pirates between containers and opening the ship selection bag.【F:Assets/Scripts/Inventory/DragAndDrop.cs†L1-L37】【F:Assets/Scripts/Inventory/ChangeShipScript.cs†L1-L23】
- `GetNewItemScript` supports spawning new ships and pirates with randomly generated names sourced from the text lists in `Assets/Names`.【F:Assets/Scripts/Inventory/GetNewItemScript.cs†L1-L45】【F:Assets/Names/ShipNames.txt†L1-L20】

### Data & Models
- `DataSaver` persists `PlayerStatModel` instances as JSON under `Application.persistentDataPath/data`, using `Newtonsoft.Json` with type metadata enabled.【F:Assets/Scripts/DataSaver.cs†L10-L134】
- `PlayerStatModel`, ship subclasses, and pirate models define the saved state for docks, ship stats, and pirate roles.【F:Assets/Scripts/PlayerStatModel.cs†L5-L12】【F:Assets/Scripts/Models/Ships/ShipModel.cs†L1-L32】【F:Assets/Scripts/Models/Pirates/PirateModel.cs†L1-L23】

## Current Issues & Risks
- `Inventory.SetupDocks` declares `newShip` inside the `if` branch but uses it afterwards, so the `else` path will not compile and the shared assignment will fail. The loop also assumes `data` is non-null.【F:Assets/Scripts/Inventory/Inventory.cs†L46-L69】
- `LoadData` can return `null` when no save file exists; the subsequent `Setup*` calls dereference `data`, leading to a runtime `NullReferenceException` on first launch.【F:Assets/Scripts/Inventory/Inventory.cs†L38-L44】【F:Assets/Scripts/DataSaver.cs†L43-L88】
- `TileScript` never seeds its `Board` dictionary because `SetupStartBoard` is commented out, so features like "BlackWater" detection rely on dynamic key creation and lose initial ship placement data.【F:Assets/Scripts/TileScript.cs†L10-L46】
- `SelectTarget.Shoot` registers button listeners every time it runs and never removes them, so re-entering the coroutine or reloading the scene would stack duplicate callbacks.【F:Assets/Scripts/SelectTarget.cs†L33-L55】
- `GetNewItemScript.GetShip` always spawns a three-masted ship prefab, with no logic for other ship classes; `GetRandomName` performs synchronous file I/O from the `Assets` directory, which will break in builds without the raw text assets present.【F:Assets/Scripts/Inventory/GetNewItemScript.cs†L9-L45】
- Persistence writes hard-coded values (`DockSize = 2`, `Lvl = 1`) when saving, ignoring actual in-game configuration, and the `Inventory` scene lacks implementation for placing pirates already assigned to ships.【F:Assets/Scripts/Inventory/Inventory.cs†L137-L154】【F:Assets/Scripts/Inventory/Inventory.cs†L109-L112】
- `DragAndDrop.OnEndDrag` assumes `pointerCurrentRaycast` is valid and `ItemSlotScript` exists, so dropping in empty space can throw; the project currently lacks the referenced `ItemSlotScript` implementation in the repository.【F:Assets/Scripts/Inventory/DragAndDrop.cs†L25-L37】

## Missing or Incomplete Features
- No enemy AI, ship placement phase, or hit detection beyond marking tiles as "H2"; the board lacks ship spawning logic, so combat is currently target practice without win/loss conditions.【F:Assets/Scripts/SelectTarget.cs†L98-L108】【F:Assets/Scripts/TileScript.cs†L10-L46】
- Inventory saving does not persist dock size, player level, or ship/pirate attributes beyond what exists in memory; there is no UI for editing stats or assigning crews beyond drag-and-drop placeholders.【F:Assets/Scripts/Inventory/Inventory.cs†L137-L154】
- `SetupOnBoardPirates` and other stubs are empty, so pirates assigned to ships never appear in the docked ship UI.【F:Assets/Scripts/Inventory/Inventory.cs†L109-L112】
- The game lacks a bootstrap for initial `PlayerStatModel` data; without a pre-generated `stats.txt`, most inventory features cannot initialize.【F:Assets/Scripts/Inventory/Inventory.cs†L38-L44】【F:Assets/Scripts/DataSaver.cs†L43-L88】

## Suggested Next Steps
1. Provide default player data or guard `Inventory.Awake` against missing saves to prevent startup crashes.
2. Fix `SetupDocks` variable scoping and flesh out dock population for empty slots.
3. Implement ship placement and enemy ship logic on the battle board, alongside victory/defeat states.
4. Add proper event listener cleanup and lifecycle management for UI buttons and drag targets.
5. Build editor scripts or ScriptableObjects to replace direct file reads for names and stats, ensuring compatibility in builds.
