# Arrgh Pirates

This repository contains a Unity 2021.3 project for a pirate-themed prototype that mixes a battleship-style firing range with inventory management.

## How to open and run
1. Install **Unity 2021.3.16f1** (or a later 2021.3 LTS patch).
2. From the Unity Hub select **Add project from disk** and point it at the repository root.
3. The primary playable scene is `Assets/Scenes/SinglePlayBoard.unity`.
   - Press **Play** in the editor to try the cannon-targeting loop.
4. The `Assets/Scenes/MenuScene.unity` scene exposes buttons to switch to the inventory and board scenes.

A standalone build is not configured yet; run the project directly inside the Unity editor.

## Project structure
- `Assets/Scripts` – gameplay logic for the board, targeting, inventory, and persistence systems.
- `Assets/Scenes` – Unity scenes (menu, single-player board, inventory prototype).
- `Assets/Resources` – prefabs for ships, pirates, and UI elements.
- `Assets/Names` – text files that feed random name generation.


## Testing
- Unity PlayMode smoke tests are available under `Assets/Tests/PlayMode/SceneSmokeTests.cs`.
- Run from Unity Test Runner (PlayMode), or by Unity CLI in batch mode.
- See `TESTING.md` for exact commands and CI-oriented headless example.

## Known limitations
The current prototype is incomplete:
- Inventory flow has ongoing stabilization work; verify with PlayMode smoke tests before release changes.
- Work-in-progress drag-and-drop scripts reference prefabs and components that are missing from the repository.
- Only a basic targeting loop is implemented; there is no enemy AI or ship placement phase.

See `GAME_STATUS.md` for a detailed feature assessment and suggested follow-up work.

For a detailed Hungarian technical audit and roadmap focused on evolving this prototype into a dynamic pirate torpedo game, see `Docs/TECH_AUDIT_HU.md`.


For a mobile-first feature backlog and Google Play release plan in Hungarian, see `Docs/PRODUCT_PLAN_MOBILE_HU.md`.

