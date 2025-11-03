# Quick Visual Guide - Simpsons Pocket Monster Game

## 🎮 What You're Building

A **2D top-down Pokémon-like game** with Simpsons characters:
- Walk around Springfield
- Encounter wild creatures in grass
- Catch them with Pokéballs
- Build your inventory
- Save & load progress

---

## 🗺️ Game Screen Layout

```
┌─────────────────────────────────────────────┐
│           SIMPSONS POCKET MONSTER            │
├─────────────────────────────────────────────┤
│                                              │
│  ┌──────────────────────────────────────┐  │
│  │                                      │  │
│  │     🟦 Player                        │  │
│  │                                      │  │
│  │    🔴 NPC          🟢 Wild Grass   │  │
│  │    (Barkeep)         Area           │  │
│  │                                      │  │
│  │  🟩 Ground (Walkable)               │  │
│  │                                      │  │
│  └──────────────────────────────────────┘  │
│                                              │
│  Controls:                                   │
│  ⬅️ ➡️ ⬆️ ⬇️ = Move    Space = Talk     │
│  I = Inventory                               │
│                                              │
└─────────────────────────────────────────────┘
```

---

## 🎬 Game Flow

```
START
  ↓
WALKING AROUND
  ├─ Press SPACE near NPC
  │  ↓
  │  NPC DIALOGUE
  │  ↓
  │  RESUME WALKING
  │
  └─ Walk through grass
     ↓
     RANDOM CHANCE?
     ├─ YES → ENCOUNTER
     │        ↓
     │        WILD CREATURE APPEARS!
     │        ↓
     │        CHOOSE ACTION
     │        ├─ FIGHT → Damage creature → Loop
     │        ├─ CATCH → Roll probability
     │        │         ├─ Success → Add inventory
     │        │         └─ Fail → Try again
     │        └─ RUN → Escape
     │        ↓
     │        RESUME WALKING
     │
     └─ NO → Continue walking
```

---

## 📦 Your Inventory

```
When you press I:

═══════════════════
    INVENTORY
═══════════════════
Pokéballs: 20
───────────────────
Caught Creatures:
  • Springbolt
  • Beerling
  • Donuragon
═══════════════════
```

---

## ⚔️ Battle Screen

```
┌─────────────────────────────────────┐
│       A wild Springbolt appeared!   │
├─────────────────────────────────────┤
│                                     │
│  [Creature Image]    HP: 30/30 ▓▓▓ │
│                                     │
│  ┌─────────────────────────────┐  │
│  │ Choose an action:           │  │
│  │                             │  │
│  │  [Fight]  [Catch]  [Run]   │  │
│  └─────────────────────────────┘  │
│                                     │
└─────────────────────────────────────┘

Battle Actions:
├─ Fight: Deal damage, lower HP
├─ Catch: Roll success chance
│         (Lower HP = higher chance)
└─ Run: Escape safely
```

---

## 🎲 Catch Probability

**The lower the creature's HP, the higher your catch chance:**

```
Springbolt Stats:
- Max HP: 30
- Catch Rate: 120 (out of 255)

Example Scenarios:

At Full HP (30/30):
  Formula: (120/255) × (1 - 1.0 × 0.5) = 23.5% chance

At Half HP (15/30):
  Formula: (120/255) × (1 - 0.5 × 0.5) = 35.3% chance

At Low HP (5/30):
  Formula: (120/255) × (1 - 0.167 × 0.5) = 43.9% chance

So: DAMAGE FIRST, THEN CATCH! 
```

---

## 🐢 Creatures You'll Find

| Creature | HP | ATK | Type | Catch Rate |
|----------|----|----|------|-----------|
| Springbolt 🔋 | 30 | 5 | Electric | Easy (120) |
| Beerling 🍺 | 25 | 4 | Drink | Very Easy (150) |
| Donuragon 🍩 | 45 | 8 | Food | Hard (80) |
| Saxasaurus 🎷 | 40 | 7 | Music | Medium (90) |

---

## 📁 File Organization (What Goes Where)

```
Your Project Folder:
SimpsonsPokeGame/

Scripts Go In:
└── Assets/Scripts/
    ├── Player/         ← Movement code
    ├── Battle/         ← Battle system
    ├── World/          ← Encounter system
    ├── NPC/            ← Dialogue
    ├── UI/             ← Menus & panels
    └── Data/           ← Creature database

Art Goes In:
└── Assets/Art/
    ├── Player/         ← Player sprite
    ├── Creatures/      ← Creature sprites
    ├── NPCs/           ← NPC sprites
    └── Environment/    ← Ground, grass, etc.

Audio Goes In:
└── Assets/Audio/
    ├── Music/          ← Background music
    └── SFX/            ← Sound effects

Scenes Go In:
└── Assets/Scenes/
    └── MainScene.unity ← Your game level
```

---

## 🎬 Step-by-Step Setup (Visual)

### Step 1: Create Player
```
┌─────┐
│ 🟦 │ ← Blue square = your player
└─────┘
- Add Rigidbody2D (Body: Dynamic, Gravity: 0)
- Add BoxCollider2D
- Attach PlayerController script
```

### Step 2: Create Ground
```
┌──────────────────────┐
│ 🟩 (Dark Green)      │ ← Large ground platform
└──────────────────────┘
- Add BoxCollider2D
- Player should stand on top (not fall through!)
```

### Step 3: Add Wild Grass
```
┌──────────────────────┐
│ 🟩    🟢 🟢 🟢       │ ← Light green area = where creatures appear
│       Tall Grass     │
└──────────────────────┘
- Add BoxCollider2D (isTrigger: TRUE ← Important!)
- Add EncounterTrigger script
```

### Step 4: Add NPC
```
┌──────────────────────┐
│ 🟩    🔴             │ ← Red square = NPC
│  Player  NPC         │
└──────────────────────┘
- Add BoxCollider2D
- Add NPCController script
- Press Space near NPC to talk
```

### Step 5: Create UI
```
┌─────────────────────────────┐
│   Battle Screen             │
├─────────────────────────────┤
│ Info: "A wild X appeared!"  │
│ [Image]     HP: XX/XX ▓▓▓  │
│ [Fight] [Catch] [Run]       │
└─────────────────────────────┘

Create in Canvas:
- BattlePanel (shown during battle)
- DialoguePanel (shown when talking to NPC)
- InventoryPanel (shown when pressing I)
```

---

## 🎮 Controls Reference

```
┌──────────────────────────────────────┐
│        GAME CONTROLS                 │
├──────────────────────────────────────┤
│                                      │
│  Movement:                           │
│    ⬅️ ➡️ ⬆️ ⬇️    or    WASD        │
│                                      │
│  Interact:                           │
│    SPACE  (talk to NPCs)             │
│                                      │
│  Inventory:                          │
│    I      (open/close inventory)     │
│                                      │
│  Battle Actions:                     │
│    Click buttons on Battle Panel     │
│                                      │
│  Pause (future feature):             │
│    ESC    (pause game)               │
│                                      │
└──────────────────────────────────────┘
```

---

## 🔍 What's Happening Behind the Scenes

```
YOU PRESS → CODE RUNS → RESULT SHOWS

Arrow Key Down
  → PlayerController.Update()
  → move.y = -1
  → Rigidbody2D.MovePosition(down)
  → Player moves down on screen

Space Key
  → PlayerController.TryInteract()
  → Raycast forward
  → Found NPC?
    → NPCController.Interact()
    → UIManager.ShowDialogue()
    → Dialogue panel appears

Walk Through Grass
  → EncounterTrigger.OnTriggerEnter2D()
  → Random(0-1) < 0.12?
    → YES: BattleSystem.StartWildBattle()
    → Battle panel appears
    → Choose action (Fight/Catch/Run)
```

---

## 💾 Save Game Structure

```
When you catch creatures and save:

Game Saves (JSON file):
{
  "playerPosition": (0, 1, 0),
  "caughtCreatures": [
    "springbolt",
    "beerling"
  ],
  "pokeballCount": 18
}

Next time you load:
- Player appears at same position
- Your caught creatures are there
- Pokéball count restored
```

---

## 🐛 Quick Debug Tips

```
If something seems broken:

1. Check Console (Ctrl+Shift+C)
   - Any red errors?
   - Click error to see details

2. Common Issues:
   
   "Player falls through ground?"
   → Check Ground has BoxCollider2D
   → Check Player Gravity Scale = 0
   
   "Encounters never happen?"
   → Check Grass Area has isTrigger: TRUE
   → Check EncounterTrigger script attached
   → Check Player tag = "Player"
   
   "Buttons don't work?"
   → Check EventSystem exists in Canvas
   → Check Button onClick listeners assigned
   
   "NPC won't talk?"
   → Check NPC has BoxCollider2D
   → Check NPCController script attached
   → Try pressing Space multiple times

3. When Stuck:
   → Read TROUBLESHOOTING.md
   → Check Console for errors
   → Follow SETUP_GUIDE.md again
```

---

## 📈 Game Progression Example

```
MINUTE 0-5:
- Player wakes up in Springfield
- Can walk around
- Meets Barkeep NPC (can talk)

MINUTE 5-15:
- Venture into wild grass area
- Encounter first creature (Springbolt)
- Battle tutorial: Fight, Catch, Run
- Catch first creature! 🎉

MINUTE 15-30:
- Continue catching creatures
- Build inventory (6 creatures max later)
- Meet other NPCs
- Explore different areas

MINUTE 30+:
- Battle other trainers (future)
- Level up creatures (future)
- Learn new moves (future)
- Complete quests (future)
```

---

## 🎯 Success Checkpoints

As you develop, verify:

```
✅ Can move with arrow keys
✅ Movement feels smooth & responsive
✅ Can talk to NPCs with Space key
✅ Dialogue appears & closes
✅ Walk through grass triggers encounter
✅ Battle panel appears
✅ Can click buttons (Fight/Catch/Run)
✅ Catch system calculates probability
✅ Caught creatures appear in inventory
✅ Save/Load works (data persists)
```

Once all ✅, your game is playable!

---

## 🎨 Why Colored Rectangles?

**Placeholder Art Strategy:**

```
PHASE 1: Get mechanics working
├─ Blue square = player
├─ Red square = NPC
├─ Yellow square = creature
├─ Green square = ground
└─ All placeholder! (fast iteration)

PHASE 2: Add real art
├─ Import sprite sheets
├─ Replace colored squares
├─ Add animations
├─ Simpsons-style visuals!
└─ Same code, better look

Result: Game works perfectly with placeholder art,
        then looks awesome when you add real sprites!
```

---

## 📚 Documentation Map

```
Need to...?              → Read...
────────────────────────   ──────────────────────
Set up the scene           → SETUP_GUIDE.md
Get started quickly        → QUICK_START.md
Understand the code        → ARCHITECTURE.md
Find an error              → TROUBLESHOOTING.md
Add new creatures          → DEVELOPMENT_ROADMAP.md
Import artwork             → ART_ASSETS_GUIDE.md
See complete hierarchy     → EXAMPLE_SCENE_CONFIG.md
Know what's included       → PROJECT_SUMMARY.md
```

---

## 🚀 Ready?

1. **Install Unity 2021.3 LTS** → https://unity.com/download
2. **Read QUICK_START.md** → 5 minutes
3. **Follow SETUP_GUIDE.md** → 45 minutes
4. **Create MainScene** → In Unity Editor
5. **Press Play** → See it work!
6. **Iterate** → Add features from roadmap

---

## 🎉 You've Got This!

Your game is **fully scaffolded** and **ready to build on**.

Think of it like:
- **Scripts** = The blueprint/instructions
- **Documentation** = The builder's manual
- **You** = The architect customizing it

Start simple, iterate often, have fun! 🎮✨

---

**Next Step:** Open QUICK_START.md →
