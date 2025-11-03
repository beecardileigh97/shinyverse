# Game Architecture & System Diagram

## System Overview

```
┌──────────────────────────────────────────────────────────────────┐
│                    SIMPSONS POCKET MONSTER GAME                  │
├──────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │                    INPUT SYSTEM                          │  │
│  │  (Arrow Keys/WASD, Space, I)                            │  │
│  └────────────────────┬─────────────────────────────────────┘  │
│                       │                                         │
│       ┌───────────────┼───────────────┐                        │
│       ▼               ▼               ▼                        │
│  ┌──────────┐  ┌──────────┐   ┌────────────┐                  │
│  │ MOVEMENT │  │INTERACT. │   │ INVENTORY  │                  │
│  │          │  │          │   │ SYSTEM     │                  │
│  └────┬─────┘  └────┬─────┘   └─────┬──────┘                  │
│       │             │              │                          │
│       ▼             ▼              ▼                          │
│  ┌────────────────────────────────────────┐                   │
│  │      PLAYER CONTROLLER                 │                   │
│  │  - Position                            │                   │
│  │  - Animation State                     │                   │
│  │  - Collision Detection                 │                   │
│  └────────┬──────────────────────┬────────┘                   │
│           │                      │                            │
│           ▼                      ▼                            │
│  ┌────────────────────┐  ┌──────────────────┐                │
│  │ ENCOUNTER TRIGGER  │  │ NPC SYSTEM       │                │
│  │                    │  │                  │                │
│  │ Random encounters  │  │ - Dialogue       │                │
│  │ in grass areas     │  │ - Interactions   │                │
│  └────────┬───────────┘  └──────────────────┘                │
│           │                                                  │
│           ▼                                                  │
│  ┌────────────────────────────────────────┐                 │
│  │      CREATURE DATABASE                 │                 │
│  │  (Springbolt, Beerling, etc.)          │                 │
│  └────────┬───────────────────────────────┘                 │
│           │                                                  │
│           ▼                                                  │
│  ┌────────────────────────────────────────┐                 │
│  │      BATTLE SYSTEM                     │                 │
│  │  - Initialize wild battle              │                 │
│  │  - Manage turn flow                    │                 │
│  │  - Handle player choices               │                 │
│  └────────┬───────────────────┬───────────┘                 │
│           │                   │                              │
│      ┌────▼────┐         ┌────▼────┐                        │
│      │ CATCH    │         │ DAMAGE  │                        │
│      │ SYSTEM   │         │ SYSTEM  │                        │
│      │          │         │         │                        │
│      │ • Roll   │         │ • Calc  │                        │
│      │ • Prob   │         │ • Apply │                        │
│      └────┬─────┘         └──────────┘                        │
│           │                                                  │
│           ▼                                                  │
│  ┌────────────────────────────────────────┐                 │
│  │      INVENTORY SYSTEM                  │                 │
│  │  - Caught creatures                    │                 │
│  │  - Pokeballs                           │                 │
│  │  - Items                               │                 │
│  └────────┬───────────────────────────────┘                 │
│           │                                                  │
│           ▼                                                  │
│  ┌────────────────────────────────────────┐                 │
│  │      SAVE SYSTEM                       │                 │
│  │  - Persistence to JSON                 │                 │
│  │  - Load/Save logic                     │                 │
│  └────────────────────────────────────────┘                 │
│                                                               │
│  ┌────────────────────────────────────────┐                 │
│  │      UI MANAGER                        │                 │
│  │  - Battle Panel                        │                 │
│  │  - Dialogue Panel                      │                 │
│  │  - Inventory Panel                     │                 │
│  └────────────────────────────────────────┘                 │
│                                                               │
└──────────────────────────────────────────────────────────────┘
```

---

## Data Flow

### Battle Encounter Flow

```
Player walks through grass
    ↓
EncounterTrigger OnTriggerEnter2D
    ↓
Random(0-1) <= encounterChance?
    ├─ YES → CreatureDatabase.Get(randomID)
    │        ↓
    │        BattleSystem.StartWildBattle()
    │        ↓
    │        Show BattlePanel (UI)
    │        Display creature image
    │        Show "Choose Action" message
    │        ↓
    │    Player clicks button
    │        ├─ Fight → Apply damage
    │        │          └─ Creature HP -= damage
    │        ├─ Catch → CatchSystem.TryCatch()
    │        │          └─ Roll probability
    │        │          └─ On success: Add to Inventory
    │        │          └─ Update UI
    │        └─ Run → Close battle panel
    │
    └─ NO → Continue walking
```

### Catch Probability Calculation

```
Catch Attempt
    ↓
catchChance = (catchRate / 255) × (1 - (currentHP / maxHP) × 0.5)
    ↓
Example: Springbolt (catchRate 120, HP 30/30)
    = (120/255) × (1 - (30/30) × 0.5)
    = 0.47 × 0.5
    = 0.235 = 23.5% success
    ↓
    When HP is lower, success rate increases
    HP 15/30 → (120/255) × (1 - 0.25) = 0.35 = 35% success
    HP 5/30  → (120/255) × (1 - 0.083) = 0.44 = 44% success
```

---

## Class Relationships

```
┌─────────────────┐
│   MonoBehaviour │ (Unity base class)
└────────┬────────┘
         │
    ┌────┴────────────────────┬────────────┬──────────────┐
    │                         │            │              │
    ▼                         ▼            ▼              ▼
┌──────────────┐      ┌──────────────┐  ┌────────┐  ┌──────────┐
│PlayerControl │      │BattleSystem  │  │NPCCtrl │  │UIManager │
└──────────────┘      └──────────────┘  └────────┘  └──────────┘
    │                      │ uses         │ uses
    │ uses                 ▼              ▼
    ▼                  ┌──────────────┐  ┌──────────────────┐
┌──────────────┐      │CatchSystem   │  │INPCInteractable  │
│EncounterTrig│      └──────────────┘  │   (interface)    │
│    ger       │                        └──────────────────┘
└──────────────┘          │ uses
    │ uses                 ▼
    │            ┌──────────────────┐
    │            │CreatureDatabase  │
    │            │ [static]         │
    │            └──────────────────┘
    │                    │ returns
    │                    ▼
    ▼            ┌──────────────────┐
┌──────────────┐ │  Creature        │
│  Inventory   │ │  [Serializable]  │
│ [Singleton]  │ └──────────────────┘
└──────────────┘          △
    │ contains            │ many
    │ many                │
    └────────────────────┘

┌──────────────┐
│ SaveSystem   │
│[Singleton]   │
│ uses ────────┼──→ Inventory
│              │
└──────────────┘
```

---

## File Organization

```
Assets/
│
├── Scripts/
│   │
│   ├── Player/
│   │   └── PlayerController.cs
│   │       └─ Handles movement, animation, raycast interact
│   │
│   ├── World/
│   │   └── EncounterTrigger.cs
│   │       └─ Spawns wild battles when probability triggers
│   │
│   ├── Battle/
│   │   ├── BattleSystem.cs
│   │   │   └─ Main battle flow controller
│   │   │
│   │   └── CatchSystem.cs
│   │       └─ Catch probability and outcomes
│   │
│   ├── NPC/
│   │   └── NPCController.cs
│   │       └─ Dialogue and interaction logic
│   │
│   ├── UI/
│   │   └── UIManager.cs
│   │       └─ Panel management (battle, dialogue, inventory)
│   │
│   └── Data/
│       ├── Creature.cs
│       │   └─ Data model: id, name, HP, attack, etc.
│       │
│       ├── Inventory.cs
│       │   └─ Singleton: tracks caught creatures, items
│       │
│       ├── CreatureDatabase.cs
│       │   └─ Static: lookup by ID
│       │
│       └── SaveSystem.cs
│           └─ Singleton: JSON persistence
│
├── Art/
│   ├── Player/              (sprites, animations)
│   ├── Creatures/           (monster sprites)
│   ├── NPCs/                (NPC sprites)
│   └── Environment/         (tileset, ground)
│
├── Audio/
│   ├── Music/               (background loops)
│   └── SFX/                 (effects: throw, catch, etc)
│
├── Scenes/
│   └── MainScene.unity      (primary playable scene)
│
├── Prefabs/
│   ├── Player.prefab
│   ├── Creature_prefab.prefab
│   └── NPC_prefab.prefab
│
└── UI/
    ├── BattleCanvas.prefab
    ├── DialogueCanvas.prefab
    └── InventoryCanvas.prefab
```

---

## Singleton Pattern (for Global Access)

```
Inventory.Instance
├── Static reference globally accessible
├── Creates once (Awake)
├── Persists across scenes (DontDestroyOnLoad)
├── Methods:
│   ├── AddCaught(id)
│   ├── HasPokeballs()
│   └── UsePokebal()
└── Properties:
    ├── caughtCreatureIDs : List<string>
    └── pokeballCount : int

BattleSystem.Instance
├── Static reference
├── Methods:
│   ├── StartWildBattle(id)
│   └── EndBattle()
└── Properties:
    ├── activeWild : Creature
    └── battleActive : bool

SaveSystem.Instance
├── Static reference
├── Methods:
│   ├── SaveGame(player, inventory)
│   └── LoadGame() : SaveData
└── Properties:
    └── savePath : string

CatchSystem.Instance
├── Static reference
├── Coroutines:
│   └── TryCatch(creature) : IEnumerator
└── Properties:
    └── infoText : Text
```

---

## State Machine (Simple Battle States)

```
Battle States:
├── INIT
│   └─ Setup creatures, show UI
│   └─ Next: CHOOSING
│
├── CHOOSING
│   └─ Wait for player input
│   └─ Next: FIGHTING | CATCHING | RUNNING
│
├── FIGHTING
│   └─ Apply damage, update HP
│   └─ Check if fainted
│   └─ Next: CHOOSING or ENDING
│
├── CATCHING
│   └─ Roll catch probability
│   └─ Show result
│   └─ On success: Add to inventory
│   └─ Next: ENDING
│
├── RUNNING
│   └─ Escape battle
│   └─ Next: ENDING
│
└── ENDING
    └─ Close UI, resume gameplay
    └─ Next: IDLE
```

---

## Key Design Decisions

1. **Singleton Pattern**
   - Used for: Inventory, BattleSystem, SaveSystem, CatchSystem
   - Reason: Global access, persist across scenes

2. **Coroutines for Sequences**
   - Used in: BattleSystem, CatchSystem
   - Reason: Clean timing, yield for animations/pauses

3. **Static Database**
   - CreatureDatabase.Get(id)
   - Reason: No need for multiple instances

4. **Interface for Interactions**
   - INPCInteractable
   - Reason: Extensible (doors, chests, etc. can implement)

5. **JSON Save Format**
   - JsonUtility (built-in)
   - Reason: Simple, human-readable, no external dependencies

---

## Extensibility Points

```
Easy to add:
├── New creatures → CreatureDatabase.db.Add()
├── New moves → Create Move class, add to Creature
├── New UI panels → UIManager.Show[Feature]()
├── NPC quests → Extend NPCController with Quest data
├── Trainer battles → Extend BattleSystem with trainer param
└── Areas/Scenes → Duplicate MainScene, change spawns

Medium complexity:
├── Leveling system → Add XP, level, stat growth
├── Type system → Add type property, damage calculator
├── Evolution → Add evolution conditions, transform creature
└── Multiplayer → Network save/load, trade system

High complexity:
├── Procedural generation → Random encounters, dungeons
├── AI trainers → Decision tree, move selection
├── Trading → Server/local multiplayer
└── Competitive play → Rating system, matchmaking
```

---

This architecture is modular, extensible, and follows Unity best practices. Each system is independent but connected through managers and singletons.
