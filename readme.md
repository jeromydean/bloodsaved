BloodSaved (Bloodstained Save Editor) — a save game editor for [Bloodstained: Ritual of the Night](https://store.steampowered.com/app/692850/Bloodstained_Ritual_of_the_Night/).

## 📖 About

BloodSaved edits story-save data for Bloodstained: Ritual of the Night. Supported tabs:

- 📊 **Stats** — coins, experience, difficulty (Normal / Hard / Nightmare), familiar experience, and archive counters from the in-game **Game Record** and **PC Record** (kills, room entries, quests, alchemy crafts, treasure boxes, unique familiars summoned, and more)
- 🎒 **Inventory** — set quantities for items across all categories (weapons, gear, food, materials, books, keys, etc.)
- 💎 **Shards** — set shard grade and rank, including skill shards
- ⚔️ **Techniques** — mark individual or selected techniques as mastered; the editor applies the native in-game use counts, experience values, and codex entries needed for Archives listing and mastery
- 📜 **Quests** — mark individual or selected quests as completed, including the related quest, grave, event-listener, and scenario-flag data needed for quest names and Archives completion
- 😈 **Demons** — mark individual, selected, or all demon archive entries as discovered, including known internal alias keys and shard-name archive entries
- 🗺️ **Map** — view map progression (discovered vs. missing rooms), set the map to 100% discovered, zoom and pan, and export the map as an SVG file

You can inject Aurora's familiars and shards into Miriam's save if you want to experiment. Shard grade and rank can be pushed up to 9999, though that tends to make combat trivial.

The UI is built with [Avalonia](https://avaloniaui.net/) 12. Save parsing lives in a separate `BloodSaved.Parsing` library, with round-trip and regression tests in `BloodSaved.Parsing.Tests`.

Saves from the PC (Steam / GOG) and PS4 versions have been tested. PS4 saves were exported with Apollo or Save Wizard before editing.

## ⬇️ Download

Pre-built releases are published on GitHub:

https://github.com/jeromydean/bloodsaved/releases

## 🎮 Usage

1. Open a **story** save file (`Story_Slot*.sav`). System saves and classic-mode variants are not supported.
2. Edit values on the **Stats**, **Inventory**, **Shards**, **Techniques**, **Quests**, **Demons**, or **Map** tabs.
3. Use **File → Save** to write changes back to the same file, or **File → Save As** to choose a new file name and save an encrypted or decrypted copy.

### 💾 Save file locations (PC)

| Platform | Path |
| --- | --- |
| Steam / GOG | `%LOCALAPPDATA%\BloodstainedRotN\Saved\SaveGames\` |
| Xbox Game Pass | `%LOCALAPPDATA%\Packages\505GAMESS.P.A.BloodstainedRitualoftheNightPCGP_tefn33qh9azfc\LocalCache\Local\BloodstainedRotN\Saved\SaveGames\` |

Back up your save before editing.

### 💡 Tips

- On the **Inventory** and **Shards** tabs, use Ctrl+A to select all rows, Shift+click for multi-select, and right-click to set quantity, grade, or rank on the selection.
- On the **Techniques** tab, check **Mastered** for a technique or right-click selected rows to master them in bulk. The editor writes the correct progress data so techniques appear in Archives and register as mastered in-game.
- On the **Quests** tab, check **Completed** for individual quests or right-click selected rows to complete them in bulk. This also updates the supporting save sections that control quest visibility and completion state.
- On the **Demons** tab, check **Discovered** for individual demons or right-click selected rows to discover them in bulk. Discovering demons also unlocks associated shard names in Archives when the shard mapping is known.
- On the **Stats** tab, **Unique Familiars Summoned** is capped at 11 (the number of familiar shard types in the game).
- On the **Map** tab, use **Set Map 100% Discovered** to mark every room as visited, then save. Ctrl+mouse wheel zooms, click-and-drag pans, and right-click exports the map as SVG.
- Food first-time consumption bonuses are preserved when you edit other values and save.

## 🛠️ Build from source

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/jeromydean/bloodsaved.git
cd bloodsaved/src
dotnet build BloodSaved.sln
dotnet run --project BloodSaved/BloodSaved.csproj
```

To run the parsing tests:

```bash
dotnet test BloodSaved.Parsing.Tests/BloodSaved.Parsing.Tests.csproj
```

## 📸 Screenshots

![Stats screen](./images/screen1.png)
![Inventory screen](./images/screen2.png)
![Shards screen](./images/screen3.png)
![Map screen](./images/screen4.png)

## 📜 License

BloodSaved is licensed under the [MIT License](license.md).
