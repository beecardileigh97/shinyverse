# Quick Reference - Getting Started

## 📁 Project Location
```
c:\Users\CHANN0$\shinyverse\shinyverse\SimpsonsPokeGame\
```

## 🚀 First Steps

### 1. Install Unity 2021.3 LTS
Download from: https://unity.com/download

### 2. Open Project in Unity Hub
- Launch **Unity Hub**
- Click **Add** → Select `SimpsonsPokeGame` folder
- Click to open (may take a few minutes on first load)

### 3. Open Main Scene
- In **Project window** (left panel): `Assets/Scenes/MainScene`
- Double-click to open (or create new if doesn't exist)

### 4. Review Scene Hierarchy
Expected structure in **Hierarchy window** (left):
```
MainScene
├── Player
├── Ground
├── WildGrassArea
├── NPCs
├── Canvas (UI)
├── GameManager
└── InventoryManager
```

### 5. Play & Test!
- Press **Play** button (top center)
- Move with **Arrow Keys** or **WASD**
- Press **Space** to interact with NPCs
- Walk through **Wild Grass Area** for encounters
- Press **I** to open inventory

---

## 📝 Key Files

| File | Purpose |
|------|---------|
| `README.md` | Project overview |
| `SETUP_GUIDE.md` | Step-by-step scene setup |
| `ART_ASSETS_GUIDE.md` | Art creation & import |
| `DEVELOPMENT_ROADMAP.md` | Features to add next |
| `TROUBLESHOOTING.md` | Common issues & fixes |

---

## 🎮 Controls

| Key | Action |
|-----|--------|
| ⬅️ ➡️ ⬆️ ⬇️ / WASD | Move |
| **Space** | Interact (NPC, doors, etc.) |
| **I** | Inventory |
| **Esc** | Menu (not implemented yet) |

---

## 🛠️ Script Locations

```
Assets/Scripts/
├── Player/
│   └── PlayerController.cs        # Movement & interaction
├── World/
│   └── EncounterTrigger.cs        # Wild encounter system
├── Battle/
│   ├── BattleSystem.cs            # Battle flow
│   └── CatchSystem.cs             # Catch mechanics
├── NPC/
│   └── NPCController.cs           # NPC interaction
├── UI/
│   └── UIManager.cs               # UI panels & display
└── Data/
    ├── Creature.cs                # Creature data model
    ├── Inventory.cs               # Player inventory
    ├── CreatureDatabase.cs        # Creature lookup
    └── SaveSystem.cs              # Save/load
```

---

## 🎨 Art Setup (Placeholder)

**Quick:** Use colored rectangles for prototyping:
1. Create **2D Sprite → Square**
2. Change color in Inspector
3. Scale to desired size
4. Attach to script

**Later:** Replace with sprites from:
- Kenney.nl (free 2D packs)
- Itch.io (community assets)
- Your own art

---

## ⚡ Quick Testing

### Test Encounters
1. Walk to **WildGrassArea** (light green zone)
2. Stand still for ~3 seconds
3. Should trigger wild battle

### Test Catch
1. In battle UI, click **Catch** button
2. See probability calculate based on creature HP
3. Success adds to inventory

### Test Save/Load
1. Catch some creatures
2. Close game
3. Reopen - creatures should persist

---

## 🐛 Debugging Tips

### Check Console for Errors
- **Ctrl + Shift + C** → Opens Console
- Red text = errors (click to see details)
- Yellow text = warnings

### Debug a Feature
```csharp
Debug.Log("Message here"); // Prints to console
```

### Pause During Play
- Click **Pause** button (or press Ctrl+Alt+P)
- Inspect GameObject values in real-time
- Resume with Play button

---

## 📚 Documentation

All guides are in the project root:

```
SimpsonsPokeGame/
├── README.md                    ← Start here
├── SETUP_GUIDE.md               ← Scene setup details
├── ART_ASSETS_GUIDE.md          ← Art & sprite import
├── DEVELOPMENT_ROADMAP.md       ← What to build next
├── TROUBLESHOOTING.md           ← Problem solving
└── QUICK_START.md               ← This file
```

---

## 💡 Pro Tips

1. **Save frequently:** Ctrl+S (scene) & File → Save Project
2. **Use Play mode:** Test changes before committing
3. **Inspector is powerful:** You can preview sprites, test buttons, etc.
4. **Organize assets:** Keep folder structure clean
5. **Comment code:** Future you will appreciate it

---

## 🎯 Next Quick Wins

Pick one to implement next:

1. **Add XP & Leveling**
   - Creatures gain XP after battle
   - Level up after 100 XP
   - Stats increase per level

2. **Add a Second Area**
   - Duplicate MainScene
   - Change sprite colors/positions
   - Create transition zone

3. **Add Moves**
   - Each creature has 1-4 moves
   - Select move in battle UI
   - Damage based on move power

4. **Add a Trainer NPC**
   - NPC with 3 creatures
   - Battle like wild encounters
   - Reward after win

---

## 📞 Getting Unstuck

1. **Check TROUBLESHOOTING.md** for your issue
2. **Search Google** + "[issue] Unity 2021"
3. **Ask on** [Unity Forums](https://forum.unity.com/) or Discord
4. **Review code comments** in relevant script

---

## 🎉 You're Ready!

Everything is set up. Open Unity and start playing! 🎮

**Enjoy building your game!**
