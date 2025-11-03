# Setup & Configuration Guide

## Initial Setup After Creating Project in Unity

### Step 1: Import Settings
1. In Unity, go to **Edit** → **Project Settings**
2. Under **Tags & Layers**, add a `Player` tag
3. Under **Physics2D**, ensure gravity is `(0, 0)` for top-down movement

### Step 2: Create the Main Scene
1. **File** → **New Scene** → **2D Scene**
2. Rename it `MainScene` and save to `Assets/Scenes/`

### Step 3: Set Up Ground
1. Create a **2D Object** → **Sprites** → **Square**
   - Name it `Ground`
   - Scale it to fill the screen (e.g., scale 20x10)
   - Drag sprite color to dark green
2. Add **BoxCollider2D** (not isTrigger)

### Step 4: Create Player
1. Create **2D Object** → **Sprites** → **Square**
   - Name it `Player`
   - Scale it 1x1 (smaller than ground)
   - Tag it as `Player`
   - Position: (0, 1, 0)
2. Add **Rigidbody2D:**
   - Body Type: Dynamic
   - Gravity Scale: 0
   - Freeze Rotation Z: checked
3. Add **BoxCollider2D** (not isTrigger)
4. Attach **PlayerController** script
5. Add **Animator** (optional, for animations later)

### Step 5: Create Wild Grass Encounter Area
1. Create a child under Ground: **2D Object** → **Sprites** → **Square**
   - Name it `WildGrassArea`
   - Scale it 5x3
   - Color it light green
   - Position: (3, 0, 0)
2. Add **BoxCollider2D:**
   - Check `isTrigger`
   - Adjust size to match sprite
3. Attach **EncounterTrigger** script
4. In Inspector, set **Possible Creature IDs:**
   - Element 0: `springbolt`
   - Element 1: `beerling`
   - Element 2: `donuragon`

### Step 6: Create NPCs
1. Create a **2D Object** → **Sprites** → **Square**
   - Name it `NPC_Barkeep`
   - Scale it 0.8x0.8
   - Color it red
   - Position: (-3, 0, 0)
2. Add **BoxCollider2D** (not isTrigger)
3. Attach **NPCController** script
4. In Inspector:
   - NPC Name: `Barkeep`
   - Dialogue: `Welcome to Moe's Tavern!`
5. Repeat for other NPCs as desired

### Step 7: Create UI Canvas
1. **Right-click in Hierarchy** → **UI** → **Canvas**
2. Name it `UICanvas`
3. Set **Canvas Scaler** to `Scale with Screen Size`

#### Battle Panel
1. Under Canvas, create **Panel** → name it `BattlePanel`
2. Set `Is Active` to **OFF** (unchecked)
3. Add child elements:
   - **Text** → name it `InfoText` (shows battle messages)
   - **Image** → name it `CreatureImage` (shows wild creature sprite)
   - **Button** → name it `FightButton` → Text: "Fight"
   - **Button** → name it `CatchButton` → Text: "Catch"
   - **Button** → name it `RunButton` → Text: "Run"
4. Layout and position buttons as desired

#### Dialogue Panel
1. Under Canvas, create **Panel** → name it `DialoguePanel`
2. Set `Is Active` to **OFF**
3. Add child elements:
   - **Text** → name it `NameText` (shows NPC name)
   - **Text** → name it `ContentText` (shows dialogue)
   - **Button** → name it `ContinueButton` → Text: "Continue"

#### Inventory Panel
1. Under Canvas, create **Panel** → name it `InventoryPanel`
2. Set `Is Active` to **OFF**
3. Add child element:
   - **Text** → name it `InventoryText` (displays inventory contents)

### Step 8: Create Game Manager
1. Create empty **GameObject** → name it `GameManager`
2. Attach scripts:
   - `BattleSystem`
   - `CatchSystem`
   - `UIManager`
   - `SaveSystem`
3. In Inspector, assign references:
   - **BattleSystem:**
     - Battle UI Panel: `UICanvas/BattlePanel`
     - Info Text: `UICanvas/BattlePanel/InfoText`
     - Creature Image: `UICanvas/BattlePanel/CreatureImage`
     - Fight Button: `UICanvas/BattlePanel/FightButton`
     - Catch Button: `UICanvas/BattlePanel/CatchButton`
     - Run Button: `UICanvas/BattlePanel/RunButton`
   - **CatchSystem:**
     - Info Text: `UICanvas/BattlePanel/InfoText`
   - **UIManager:**
     - Dialogue Panel: `UICanvas/DialoguePanel`
     - Dialogue Name Text: `UICanvas/DialoguePanel/NameText`
     - Dialogue Content Text: `UICanvas/DialoguePanel/ContentText`
     - Continue Button: `UICanvas/DialoguePanel/ContinueButton`
     - Inventory Panel: `UICanvas/InventoryPanel`
     - Inventory Text: `UICanvas/InventoryPanel/InventoryText`

### Step 9: Create Inventory Manager
1. Create empty **GameObject** → name it `InventoryManager`
2. Attach **Inventory** script
3. This will auto-persist across scenes via `DontDestroyOnLoad()`

### Step 10: Create Prefabs (Optional)
1. Drag **Player** into `Assets/Prefabs/` to create a prefab
2. Drag **NPC_Barkeep** into `Assets/Prefabs/` → rename to `NPC_prefab`
3. Drag **WildGrassArea** into `Assets/Prefabs/` → rename to `EncounterTrigger_prefab`

### Step 11: Save Scene & Play
1. Save the scene: **Ctrl+S** (or **Cmd+S** on Mac)
2. Press **Play** to test
3. Use **Arrow Keys** to move, **Space** to interact

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Player falls through ground | Ensure Rigidbody2D gravity is 0; check colliders |
| Encounters don't trigger | Verify `isTrigger: true` on EncounterTrigger collider; check `Player` tag |
| UI buttons don't work | Ensure `EventSystem` exists in scene; check button onClick assignments |
| NPCs don't respond | Verify `isTrigger: false` on NPC collider; check script assignment |
| Save file not found | Check `Application.persistentDataPath` in console for correct path |

## Editor Preferences

### Set VS Code as Default Editor
1. **Edit** → **Preferences** → **External Tools**
2. Set **External Script Editor** to `Visual Studio Code`
3. Click **Regenerate project files**

### Player Prefs for Testing
Add this to console for quick testing:
```csharp
PlayerPrefs.DeleteAll(); // Clear all saves
```

---

**Setup complete!** Start by playtesting movement, NPCs, and encounters.
