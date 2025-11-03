# Simpsons Pocket Monster Prototype

A Unity 2D top-down adventure game with creature catching mechanics, inspired by Pokémon and The Simpsons.

## Project Overview

**Engine:** Unity 2021.3 LTS or later  
**Language:** C# (MonoBehaviour scripts)  
**Genre:** 2D Top-Down Adventure + Creature Collection  
**Status:** Playable Prototype

## Features

- ✅ Player movement (top-down, directional)
- ✅ NPC interaction with dialogue
- ✅ Wild creature encounters (randomized)
- ✅ Turn-based battle UI
- ✅ Creature catching system with probability
- ✅ Basic inventory (Pokeballs, caught creatures)
- ✅ Save/Load system
- ✅ Pause menu and UI controls

## Quick Start

### 1. Prerequisites
- **Unity 2021.3 LTS** or later (download from [Unity Hub](https://unity.com/download))
- **Visual Studio Code** (optional, for script editing)

### 2. Open in Unity
1. Open **Unity Hub** → Click **Add** → Select the `SimpsonsPokeGame` folder
2. Unity will import the project
3. Open `Assets/Scenes/MainScene` (or create a new scene if needed)

### 3. Scene Setup
In the main scene, create the following hierarchy:

```
MainScene
├── Player (GameObject)
│   ├── SpriteRenderer (with player sprite)
│   ├── BoxCollider2D
│   ├── Rigidbody2D (Body Type: Dynamic, Gravity Scale: 0)
│   └── PlayerController (script)
├── Ground (Tilemap or sprite with collider)
├── WildGrass (Tilemap area with EncounterTrigger collider)
├── NPCs (various NPC GameObjects with NPCController + BoxCollider2D)
├── Canvas (UI)
│   ├── BattlePanel (inactive by default)
│   │   ├── InfoText
│   │   ├── CreatureImage
│   │   ├── FightButton
│   │   ├── CatchButton
│   │   └── RunButton
│   └── DialoguePanel (inactive by default)
│       ├── NameText
│       └── ContentText
├── GameManager (empty, attach BattleSystem, CatchSystem, UIManager, SaveSystem)
└── Inventory (empty, attach Inventory script)
```

### 4. Script Assignment
- **Player GameObject:** Attach `PlayerController` script
- **Wild Grass Area:** Attach `EncounterTrigger` script, set creature IDs
- **NPCs:** Attach `NPCController` script with name/dialogue
- **GameManager:** Attach `BattleSystem`, `CatchSystem`, `UIManager`, `SaveSystem`
- **Inventory Object:** Attach `Inventory` script

### 5. Inspector Setup
- **BattleSystem:**
  - Assign `battleUIPanel` (Canvas > BattlePanel)
  - Assign `infoText`, `creatureImage`, buttons
- **CatchSystem:**
  - Assign `infoText`
- **UIManager:**
  - Assign `dialoguePanel`, `inventoryPanel`, related texts
- **EncounterTrigger:**
  - Set `possibleCreatureIDs` to: `["springbolt", "beerling", "donuragon", "saxasaurus"]`
  - Adjust `encounterChance` (0.12 = 12%)

### 6. Play!
Press **Play** in Unity Editor. 

**Controls:**
- **Arrow Keys / WASD** → Move
- **Space** → Interact with NPCs / Objects
- **I** → Open Inventory
- **Esc** → Pause (can be added)

## Creatures in Database

| ID | Name | HP | Attack | Defense | Catch Rate |
|----|------|----|----|---------|-----------|
| springbolt | Springbolt | 30 | 5 | 4 | 120 |
| beerling | Beerling | 25 | 4 | 3 | 150 |
| donuragon | Donuragon | 45 | 8 | 6 | 80 |
| saxasaurus | Saxasaurus | 40 | 7 | 5 | 90 |

## Art & Audio Placeholders

### Sprites
Replace with your own or use free CC0 assets:
- **Kenney.nl:** Free 2D sprite packs
- **Itch.io:** Free indie game assets
- **OpenGameArt.org:** Community art library

Placeholder: Use colored rectangles (e.g., blue square for player, yellow for creature).

### Audio
- **Music Loop:** Place in `Assets/Audio/` - loop for encounters
- **SFX:** throw_sound, catch_sound, faint_sound (optional)

## Expanding the Game

### Phase 2: Leveling System
```csharp
public class Creature {
    public int level;
    public int experience;
    public List<Move> moves;
}
```

### Phase 3: Moves & Battles
Add move types, accuracy, status effects:
```csharp
[System.Serializable]
public class Move {
    public string name;
    public int power;
    public int accuracy;
    public string effect; // "burn", "paralyze", etc.
}
```

### Phase 4: Trainer Battles
Extend `BattleSystem` to handle NPC trainers with multiple creatures.

### Phase 5: Quest System
Add dialogue trees and quest objectives:
```csharp
public class Quest {
    public string id;
    public string title;
    public string objective;
    public bool completed;
}
```

### Phase 6: Day/Night Cycle
Toggle creature spawns based on time:
```csharp
public float timeOfDay; // 0-1 (0=midnight, 0.5=noon)
```

## Debugging Tips

- **Encounters not triggering?**
  - Check `EncounterTrigger` collider is `isTrigger: true`
  - Verify player has `Player` tag
  - Check `BattleSystem.Instance` is not null

- **Buttons not responding?**
  - Ensure `Button` component is on UI element
  - Check `EventSystem` exists in scene
  - Verify onClick listeners in `BattleSystem.Start()`

- **Save/Load not working?**
  - Check `Application.persistentDataPath` in console
  - Verify `SaveSystem.Instance` exists

- **NPCs not interacting?**
  - Ensure NPC has `BoxCollider2D` with `isTrigger: false`
  - Check `INPCInteractable` interface is implemented
  - Verify raycast direction in `PlayerController.TryInteract()`

## Project Structure

```
SimpsonsPokeGame/
├── Assets/
│   ├── Scripts/
│   │   ├── Player/        (PlayerController)
│   │   ├── World/         (EncounterTrigger, world logic)
│   │   ├── Battle/        (BattleSystem, CatchSystem)
│   │   ├── NPC/           (NPCController)
│   │   ├── UI/            (UIManager)
│   │   └── Data/          (Creature, Inventory, SaveSystem, CreatureDatabase)
│   ├── Art/               (sprites, animations)
│   ├── Audio/             (music, SFX)
│   ├── Scenes/            (MainScene.unity)
│   ├── Prefabs/           (reusable GameObjects)
│   └── UI/                (UI prefabs, canvases)
└── README.md
```

## Exporting & Deployment

### Standalone Build
1. **File** → **Build Settings**
2. Add `MainScene` to Scenes in Build
3. Select platform (Windows, Mac, Linux, WebGL)
4. Click **Build**

### WebGL (Play in Browser)
1. Switch to **WebGL** platform
2. Build and upload `.html` file to Netlify, Vercel, or GitHub Pages

## Dependencies
- Unity 2021.3 LTS+
- TextMesh Pro (included in newer Unity)
- No external packages required

## License
MIT or your chosen license

## Next Steps
1. Create placeholder art (colored squares)
2. Build and test movement
3. Add NPC to scene and test interaction
4. Trigger encounters in wild grass area
5. Test catch system
6. Iterate on balance and feel

---

**Happy game dev!** 🎮

For questions, refer to:
- [Unity Documentation](https://docs.unity3d.com/)
- [2D Games in Unity](https://unity.com/learn/pathways/beginner-2d-game)
- [Physics2D API](https://docs.unity3d.com/ScriptReference/Physics2D.html)
