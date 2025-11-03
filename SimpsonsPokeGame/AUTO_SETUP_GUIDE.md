# 🤖 Complete Auto Setup - All in One

Three powerful tools to automatically create your entire game scene!

---

## 🎯 Three Options Available

### **Option A: Editor Tool (One-Click Scene Generation)** ⭐ EASIEST
- **Location:** `Window → Simpsons Pocket Monster → Generate Scene`
- **What it does:** Creates entire scene in Unity Editor with one click
- **Time:** 2 seconds
- **When to use:** You want everything done in the editor immediately

### **Option B: Runtime Generator (Auto-Generation on Play)**
- **Location:** Automatic when game starts
- **What it does:** Creates scene automatically when you press Play
- **Time:** 1 second on startup
- **When to use:** You want the scene created dynamically at runtime

### **Option C: Complete Auto Setup (Everything at Once)** ⭐ RECOMMENDED
- **Location:** `Tools → Simpsons Game → Auto Complete Setup`
- **What it does:** Combines both tools + creates prefabs + auto saves
- **Time:** 5 seconds
- **When to use:** You want 100% automation, no manual steps

---

## 🚀 Quick Start Guide

### Using Option C (Complete Auto Setup) - RECOMMENDED

1. **Open Unity**
   - Open your SimpsonsPokeGame project
   - Wait for compilation

2. **Click the Magic Button**
   - Go to `Tools → Simpsons Game → Auto Complete Setup`
   - A window appears with 3 options

3. **Choose Setup Mode**
   - Select `Both (Recommended)` - creates both editor AND runtime versions
   - Check `Create Prefabs` ✓
   - Check `Auto Play After Setup` ✓

4. **Click "🚀 START COMPLETE AUTO SETUP"**
   - Watch the progress bar
   - Scene generates automatically
   - Game starts playing!

5. **Test the Game**
   - Use Arrow Keys to move
   - Press Space to talk to NPCs
   - Walk through grass to encounter creatures
   - Press I for inventory

**Total Time:** ~5 seconds ⚡

---

## 📋 What Gets Created

After running auto setup, you'll have:

### Scene Hierarchy
```
MainScene
├── Player (with controller, movement)
├── Ground (dark green walkable area)
├── WildGrassArea (light green encounter zone)
├── NPCs (3 characters with dialogue)
├── Canvas (complete UI system)
├── GameManager (all game systems)
└── InventoryManager (item tracking)
```

### UI Elements
- ✅ Battle Panel (Fight/Catch/Run buttons)
- ✅ Dialogue Panel (NPC conversations)
- ✅ Inventory Panel (creature tracking)
- ✅ Event System (button interactions)

### Game Systems
- ✅ PlayerController (movement)
- ✅ BattleSystem (encounters)
- ✅ CatchSystem (catching mechanics)
- ✅ UIManager (panel management)
- ✅ SaveSystem (persistence)
- ✅ Inventory (item tracking)

### Prefabs (if enabled)
- ✅ Player.prefab
- ✅ NPC_prefab.prefab

---

## 🔧 Individual Tools (If You Prefer)

### Option A: Editor Tool Only

**Menu:** `Window → Simpsons Pocket Monster → Generate Scene`

**How to use:**
1. Click window title
2. Adjust settings (optional)
3. Click "✨ Generate Complete Scene"
4. Scene appears instantly
5. Click Play

**Advantages:**
- Fast (one-click)
- Full control in editor
- Can customize before playing

---

### Option B: Runtime Generator

**How to use:**
1. In editor, create empty GameObject named "_RuntimeGen"
2. Attach `RuntimeSceneGenerator.cs` script
3. Check `autoGenerateOnStart` ✓
4. Play the game
5. Scene generates on startup

**Advantages:**
- Scene regenerates each playtest
- Good for development iteration
- Useful for testing different configurations

---

## ⚙️ Configuration Options

When using the tools, you can customize:

```
Player Move Speed       = 4.0 (how fast player moves)
Encounter Chance       = 0.12 (12% chance to encounter creatures)
Starting Pokeballs     = 20 (inventory items at start)
Use Colors for Art     = true (placeholder colors vs empty sprites)
Create Prefabs         = true (save as reusable prefabs)
Auto Play              = true (auto start game after setup)
```

---

## 🎮 After Setup - What to Do

### Immediate (Test Everything)
1. Press Play
2. Move with Arrow Keys
3. Press Space to talk to NPCs
4. Press I for inventory
5. Walk through grass area

### Next (Add Your Art)
1. Replace colored squares with real sprites
2. Follow `ART_ASSETS_GUIDE.md`
3. Import sprites into Assets/Art/

### Then (Expand Features)
1. Follow `DEVELOPMENT_ROADMAP.md`
2. Add XP & leveling
3. Add moves for creatures
4. Add trainer battles

---

## 🐛 Troubleshooting

### "Tools menu doesn't show Simpsons Game option"
- Save all scripts first (Ctrl+S)
- Wait for compilation (check bottom-right console)
- Restart Unity if needed

### "Scene doesn't generate, just shows black screen"
- Check Console for errors (Ctrl+Shift+C)
- Ensure all scripts compiled successfully
- Try Option A instead (direct editor tool)

### "Player falls through ground"
- Physics2D gravity might not be 0
- Check Player Rigidbody2D → Gravity Scale = 0
- Check Ground has BoxCollider2D (not trigger)

### "Buttons don't work in battle"
- Ensure EventSystem exists in Canvas
- Check all UI references are assigned
- Try re-running the setup

---

## 📊 Comparison Table

| Feature | Option A | Option B | Option C |
|---------|----------|----------|----------|
| One-Click | ✅ | ✅ | ✅ |
| Editor | ✅ | ❌ | ✅ |
| Runtime | ❌ | ✅ | ✅ |
| Prefabs | ❌ | ❌ | ✅ |
| Auto Play | ❌ | ✅ | ✅ |
| Speed | Fast | Fast | Medium |
| Ease | Easiest | Easy | Easiest |

---

## 🎯 Recommended Workflow

### For Quick Testing
```
1. Use Option C (Complete Auto Setup)
2. Click "🚀 START"
3. Game starts automatically
4. Test immediately
```

### For Development
```
1. Use Option A (Editor Tool)
2. Generate scene
3. Modify things in editor
4. Save and play
5. Iterate
```

### For Production
```
1. Use Option B (Runtime Generator) disabled
2. Use Option A to create final scene
3. Fine-tune everything
4. Build game
```

---

## 🎉 You're Ready!

Pick your method:
- **Want it DONE in 2 seconds?** → Option C
- **Want control in editor?** → Option A
- **Want dynamic generation?** → Option B
- **Want ALL of them?** → Use Option C with both enabled

**Next step:** Go to Unity and try one of the options! 🚀

---

## 📝 Script Locations

- **Editor Tool:** `Assets/Scripts/Editor/SceneGenerator.cs`
- **Runtime Generator:** `Assets/Scripts/System/RuntimeSceneGenerator.cs`
- **Complete Setup:** `Assets/Scripts/Editor/CompleteAutoSetup.cs`

All three are fully documented with comments explaining every step!

---

**Ready? Open Unity and try it!** 🎮✨
