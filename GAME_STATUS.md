# Arrgh Pirates Project Status

## Project Overview
- Unity 2021.3 prototype that mixes a battleship-style cannon board (`SinglePlayBoard`) with WIP inventory and menu scenes (`MenuScene`, `InventoryScene`).
- Core gameplay scripts sit under `Assets/Scripts`, covering battle board targeting, ship and pirate inventory management, and persistence helpers.
- Assets include reusable prefabs in `Assets/Resources/Prefabs` and name lists in `Assets/Names` for procedural generation.

## Playability snapshot
- **Playable loop** – `SinglePlayBoard.unity` runs in the editor: targeting buttons move the reticle, shots animate, hits/misses update the water material, and the HUD text reflects remaining ammo.【F:Assets/Scripts/SelectTarget.cs†L34-L132】【F:Assets/Scripts/TileScript.cs†L26-L73】【F:Assets/Scripts/TopTextScript.cs†L11-L39】
- **Victory/defeat** – The board tracks hits and ends the round once all ships are sunk or ammo runs out, updating the top text with a win/lose message.【F:Assets/Scripts/SelectTarget.cs†L152-L207】
- **Menu flow** – `MenuScene.unity` has functional buttons that load the board and inventory scenes through `MenuScript`.【F:Assets/Scripts/Menu/MenuScript.cs†L1-L11】
- **Inventory scene** – Cannot be played: it expects a saved `PlayerStatModel` file and contains compile errors (`SetupDocks` uses `newShip` outside of scope).【F:Assets/Scripts/Inventory/Inventory.cs†L40-L83】

## How to run
1. Install **Unity 2021.3.16f1** or a compatible 2021.3 LTS patch.【F:ProjectSettings/ProjectVersion.txt†L1-L2】
2. Open the project with Unity Hub and load `Assets/Scenes/SinglePlayBoard.unity`.
3. Press **Play** in the editor; use the on-screen arrow buttons to move and **Tűz** to shoot.
4. For menu navigation tests, start from `Assets/Scenes/MenuScene.unity` and select the desired destination.

> A standalone build configuration is not present. Run the project directly in the editor.

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
- `Inventory.SetupDocks` declares `newShip` inside the `if` branch but uses it afterwards, so the `else` path fails to compile; the loop also assumes `data` is non-null.【F:Assets/Scripts/Inventory/Inventory.cs†L46-L83】
- `LoadData` can return `null` when no save file exists; the subsequent `Setup*` calls dereference `data`, causing a `NullReferenceException` on first launch.【F:Assets/Scripts/Inventory/Inventory.cs†L38-L44】【F:Assets/Scripts/DataSaver.cs†L43-L88】
- `TileScript.SetupStartBoard` seeds ship positions, but no ship placement UI exists; once a tile is hit the game cannot respawn a new layout without reloading the scene.【F:Assets/Scripts/TileScript.cs†L34-L67】【F:Assets/Scripts/SelectTarget.cs†L98-L148】
- `SelectTarget` never unwires button listeners, so reloading the scene stacks callbacks and double-fires actions.【F:Assets/Scripts/SelectTarget.cs†L62-L90】
- `GetNewItemScript.GetShip` always spawns the three-masted prefab, and `GetRandomName` reads from `Assets/Names` at runtime, which fails in a player build without including the raw text assets.【F:Assets/Scripts/Inventory/GetNewItemScript.cs†L9-L45】
- Persistence writes hard-coded values (`DockSize = 2`, `Lvl = 1`) and omits crew assignments; the inventory scene does not restore pirates already linked to ships.【F:Assets/Scripts/Inventory/Inventory.cs†L109-L154】
- `DragAndDrop.OnEndDrag` assumes `pointerCurrentRaycast` is valid and references a missing `ItemSlotScript`, causing drops onto empty space to throw at runtime.【F:Assets/Scripts/Inventory/DragAndDrop.cs†L25-L37】

## Missing or Incomplete Features
- No enemy AI, ship placement phase, or hit detection beyond marking tiles as "H2"; the board lacks ship spawning logic, so combat is currently target practice without win/loss conditions.【F:Assets/Scripts/SelectTarget.cs†L98-L108】【F:Assets/Scripts/TileScript.cs†L10-L46】
- Inventory saving does not persist dock size, player level, or ship/pirate attributes beyond what exists in memory; there is no UI for editing stats or assigning crews beyond drag-and-drop placeholders.【F:Assets/Scripts/Inventory/Inventory.cs†L137-L154】
- `SetupOnBoardPirates` and other stubs are empty, so pirates assigned to ships never appear in the docked ship UI.【F:Assets/Scripts/Inventory/Inventory.cs†L109-L112】
- The game lacks a bootstrap for initial `PlayerStatModel` data; without a pre-generated `stats.txt`, most inventory features cannot initialize.【F:Assets/Scripts/Inventory/Inventory.cs†L38-L44】【F:Assets/Scripts/DataSaver.cs†L43-L88】

## Suggested Next Steps
1. Provide default player data or guard `Inventory.Awake` against missing saves to prevent startup crashes.
2. Fix `SetupDocks` scoping, guard against `null` data, and finish the empty-dock fallback to make the scene compile again.
3. Introduce ship placement and enemy logic so the board becomes a full battleship experience instead of a fixed shooting gallery.
4. Add lifecycle management for UI listeners and drag targets to avoid duplicate callbacks and null pointer crashes.
5. Replace runtime file IO for names/stats with ScriptableObjects or resources bundled into the build for platform compatibility.
6. Capture proper error states in the inventory UI (e.g., missing prefabs) and add editor tools to validate required references.
