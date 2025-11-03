# Example Scene Configuration

This file shows exactly what your MainScene should look like when fully configured.

---

## Hierarchy Structure (Copy This)

```
MainScene (Scene)
│
├── Ground (Sprite)
│   │   Tag: none
│   │   Components:
│   │   ├─ Transform: Position(0,0,0) Scale(20,10,1)
│   │   ├─ SpriteRenderer: Color=#004400 (dark green)
│   │   └─ BoxCollider2D: isTrigger=false Size(1,1)
│   │
│   └── WildGrassArea (Sprite)
│       │   Tag: none
│       │   Components:
│       │   ├─ Transform: Position(3,0,0) Scale(5,3,1)
│       │   ├─ SpriteRenderer: Color=#00CC00 (light green)
│       │   ├─ BoxCollider2D: isTrigger=true Size(1,1)
│       │   └─ EncounterTrigger Script
│       │       └─ possibleCreatureIDs:
│       │          ├─ [0] "springbolt"
│       │          ├─ [1] "beerling"
│       │          ├─ [2] "donuragon"
│       │          └─ [3] "saxasaurus"
│       │       └─ encounterChance: 0.12
│       │
│       └── NPC_Barkeep (Sprite)
│           │   Tag: none
│           │   Components:
│           │   ├─ Transform: Position(-3,0,0) Scale(0.8,0.8,1)
│           │   ├─ SpriteRenderer: Color=#FF0000 (red)
│           │   ├─ BoxCollider2D: isTrigger=false Size(1,1)
│           │   └─ NPCController Script
│           │       ├─ npcName: "Barkeep"
│           │       └─ dialogue: "Welcome to Moe's!"
│           │
│           └── NPC_Homer (Sprite)
│               │   Tag: none
│               │   Components:
│               │   ├─ Transform: Position(-3,-3,0) Scale(0.8,0.8,1)
│               │   ├─ SpriteRenderer: Color=#FFCC00 (yellow)
│               │   ├─ BoxCollider2D: isTrigger=false Size(1,1)
│               │   └─ NPCController Script
│               │       ├─ npcName: "Homer"
│               │       └─ dialogue: "D'oh!"
│
├── Player (Sprite)
│   │   Tag: "Player"
│   │   Components:
│   │   ├─ Transform: Position(0,1,0) Scale(1,1,1)
│   │   ├─ SpriteRenderer: Color=#0066FF (blue)
│   │   ├─ BoxCollider2D: isTrigger=false Size(1,1)
│   │   ├─ Rigidbody2D:
│   │   │  ├─ Body Type: Dynamic
│   │   │  ├─ Gravity Scale: 0
│   │   │  ├─ Velocity: (0,0)
│   │   │  └─ Freeze Rotation Z: checked
│   │   ├─ Animator: Controller=[PlayerAnimator]
│   │   └─ PlayerController Script
│   │       └─ moveSpeed: 4
│   │
│   └── [No children]
│
├── Canvas (Canvas)
│   │   Canvas Scaler: Scale with Screen Size
│   │
│   ├── BattlePanel (Panel)
│   │   │   Active: false
│   │   │   Color: RGBA(0,0,0,200)
│   │   │
│   │   ├─ InfoText (Text)
│   │   │  └─ Text: "Battle Info Here"
│   │   │
│   │   ├─ CreatureImage (Image)
│   │   │  └─ Sprite: [none, assigned at runtime]
│   │   │
│   │   ├─ FightButton (Button)
│   │   │  └─ Text: "Fight"
│   │   │
│   │   ├─ CatchButton (Button)
│   │   │  └─ Text: "Catch"
│   │   │
│   │   └─ RunButton (Button)
│   │      └─ Text: "Run"
│   │
│   ├── DialoguePanel (Panel)
│   │   │   Active: false
│   │   │   Color: RGBA(20,20,60,200)
│   │   │
│   │   ├─ NameText (Text)
│   │   │  └─ Text: "NPC Name"
│   │   │
│   │   ├─ ContentText (Text)
│   │   │  └─ Text: "Dialogue content"
│   │   │
│   │   └─ ContinueButton (Button)
│   │      └─ Text: "Continue"
│   │
│   └── InventoryPanel (Panel)
│       │   Active: false
│       │   Color: RGBA(0,0,0,200)
│       │
│       └─ InventoryText (Text)
│          └─ Text: "Inventory contents"
│
├── GameManager (Empty GameObject)
│   │   Tag: none
│   │
│   ├─ BattleSystem Script
│   │  ├─ battleUIPanel: [Canvas > BattlePanel]
│   │  ├─ infoText: [Canvas > BattlePanel > InfoText]
│   │  ├─ creatureImage: [Canvas > BattlePanel > CreatureImage]
│   │  ├─ fightButton: [Canvas > BattlePanel > FightButton]
│   │  ├─ catchButton: [Canvas > BattlePanel > CatchButton]
│   │  └─ runButton: [Canvas > BattlePanel > RunButton]
│   │
│   ├─ CatchSystem Script
│   │  └─ infoText: [Canvas > BattlePanel > InfoText]
│   │
│   ├─ UIManager Script
│   │  ├─ dialoguePanel: [Canvas > DialoguePanel]
│   │  ├─ dialogueNameText: [Canvas > DialoguePanel > NameText]
│   │  ├─ dialogueContentText: [Canvas > DialoguePanel > ContentText]
│   │  ├─ continueButton: [Canvas > DialoguePanel > ContinueButton]
│   │  ├─ inventoryPanel: [Canvas > InventoryPanel]
│   │  └─ inventoryText: [Canvas > InventoryPanel > InventoryText]
│   │
│   └─ SaveSystem Script
│      (no public fields)
│
└── InventoryManager (Empty GameObject)
    │   Tag: none
    │   Don't Destroy On Load: true
    │
    └─ Inventory Script
       ├─ caughtCreatureIDs: [] (starts empty)
       └─ pokeballCount: 20
```

---

## Inspector Setup Screenshots (Text Format)

### Player GameObject Inspector

```
GameObject: Player
├─ Active: checked
├─ Tag: "Player"
├─ Layer: Default
│
Components:
│
├─ Transform
│  ├─ Position: X=0, Y=1, Z=0
│  ├─ Rotation: X=0, Y=0, Z=0
│  └─ Scale: X=1, Y=1, Z=1
│
├─ Sprite Renderer
│  ├─ Sprite: (white square or custom)
│  ├─ Color: R=0, G=102, B=255 (blue)
│  └─ Sorting Order: 0
│
├─ Box Collider 2D
│  ├─ Is Trigger: unchecked
│  ├─ Used by Effector: unchecked
│  ├─ Offset: X=0, Y=0
│  └─ Size: X=1, Y=1
│
├─ Rigidbody 2D
│  ├─ Body Type: Dynamic
│  ├─ Mass: 1
│  ├─ Linear Drag: 0
│  ├─ Angular Drag: 0.05
│  ├─ Gravity Scale: 0
│  ├─ Freeze Rotation: Z
│  ├─ Collision Detection: Continuous
│  └─ Constraints: (none)
│
├─ Animator
│  ├─ Controller: PlayerAnimator [AnimatorController]
│  └─ Avatar: (none)
│
└─ PlayerController (Script)
   └─ Move Speed: 4
```

### BattleSystem Component Inspector

```
Game Manager > BattleSystem (Script)
│
├─ Battle UI Panel: Canvas > BattlePanel
├─ Info Text: Canvas > BattlePanel > InfoText (Text)
├─ Creature Image: Canvas > BattlePanel > CreatureImage (Image)
├─ Fight Button: Canvas > BattlePanel > FightButton (Button)
├─ Catch Button: Canvas > BattlePanel > CatchButton (Button)
└─ Run Button: Canvas > BattlePanel > RunButton (Button)

Button OnClick Events:
├─ FightButton.onClick → BattleSystem.OnFightClicked()
├─ CatchButton.onClick → BattleSystem.OnCatchClicked()
└─ RunButton.onClick → BattleSystem.OnRunClicked()
```

### EncounterTrigger Component Inspector

```
WildGrassArea > EncounterTrigger (Script)
│
├─ Possible Creature IDs: Size 4
│  ├─ [0]: "springbolt"
│  ├─ [1]: "beerling"
│  ├─ [2]: "donuragon"
│  └─ [3]: "saxasaurus"
│
└─ Encounter Chance: 0.12
```

---

## Scene Setup Checklist

Use this to verify your scene is correct:

### Player Setup
- [ ] Player GameObject created
- [ ] Player tagged as "Player"
- [ ] Player has SpriteRenderer (any sprite)
- [ ] Player has BoxCollider2D (isTrigger: false)
- [ ] Player has Rigidbody2D (Dynamic, Gravity: 0, Freeze Rot Z)
- [ ] PlayerController script attached
- [ ] Player can move with arrow keys

### Ground Setup
- [ ] Ground GameObject created
- [ ] Ground has SpriteRenderer (dark green)
- [ ] Ground has BoxCollider2D (isTrigger: false)
- [ ] Player doesn't fall through ground

### Wild Grass Area
- [ ] WildGrassArea GameObject created
- [ ] WildGrassArea has SpriteRenderer (light green)
- [ ] WildGrassArea has BoxCollider2D (isTrigger: TRUE)
- [ ] EncounterTrigger script attached
- [ ] possibleCreatureIDs array filled (4+ creatures)
- [ ] encounterChance set to 0.12
- [ ] Encounters trigger when walking through

### NPCs
- [ ] At least 1 NPC GameObject created
- [ ] NPC has SpriteRenderer (any color)
- [ ] NPC has BoxCollider2D (isTrigger: false)
- [ ] NPCController script attached
- [ ] npcName and dialogue assigned in Inspector
- [ ] Player can interact with Space key

### Canvas/UI
- [ ] Canvas exists in hierarchy
- [ ] BattlePanel created, set to inactive
- [ ] BattlePanel has InfoText, CreatureImage, Buttons
- [ ] DialoguePanel created, set to inactive
- [ ] DialoguePanel has NameText, ContentText, Button
- [ ] InventoryPanel created, set to inactive
- [ ] InventoryPanel has InventoryText
- [ ] EventSystem exists (auto-created or manual)

### Managers
- [ ] GameManager GameObject created
- [ ] BattleSystem script attached, all UI refs assigned
- [ ] CatchSystem script attached, infoText assigned
- [ ] UIManager script attached, all panel refs assigned
- [ ] SaveSystem script attached
- [ ] InventoryManager GameObject created
- [ ] Inventory script attached
- [ ] InventoryManager has "Don't Destroy On Load" checked

### Final Tests
- [ ] Press Play
- [ ] Player moves smoothly
- [ ] Can interact with NPC (see dialogue)
- [ ] Dialogue panel opens/closes
- [ ] Walk through grass area
- [ ] Wild battle triggers (eventually)
- [ ] Battle panel appears
- [ ] Can click buttons in battle
- [ ] Can catch creatures
- [ ] Press I to see inventory
- [ ] Save/load works (optional first)

---

## Common Configuration Errors

### Error: "Player falls through ground"
**Fix:**
- Player Rigidbody2D → Gravity Scale must be 0
- Ground must have BoxCollider2D with isTrigger: FALSE
- Player must have BoxCollider2D with isTrigger: FALSE

### Error: "Encounters never happen"
**Fix:**
- WildGrassArea BoxCollider2D → isTrigger: TRUE
- EncounterTrigger script attached to grass area
- possibleCreatureIDs array is NOT empty
- Player tag is set to "Player"

### Error: "Buttons don't respond"
**Fix:**
- EventSystem exists in Canvas hierarchy
- Buttons are in Canvas (children or direct children)
- onClick listeners added in BattleSystem.Start()
- No errors in Console

### Error: "NPCs won't interact"
**Fix:**
- NPC BoxCollider2D → isTrigger: FALSE
- NPCController script attached
- Player raycast working (test with Debug.DrawRay)
- Player Space key triggers TryInteract()

---

## Performance Tips

1. **Limit colliders:** Only what's necessary
2. **Use simple shapes:** BoxCollider2D over PolygonCollider2D
3. **Cache references:** Don't use FindObjectOfType() in loops
4. **Batch UI updates:** Update inventory panel only when inventory changes
5. **Pool encounters:** Reuse battle UI instead of creating new

---

## Next Session Checklist

Before you start developing:
- [ ] Scene created in Assets/Scenes/
- [ ] All GameObjects in hierarchy
- [ ] All scripts attached
- [ ] All Inspector references assigned
- [ ] No errors in Console
- [ ] Player can move
- [ ] Movement feels smooth
- [ ] Encounters trigger
- [ ] Battles appear
- [ ] Catch system works

Once all checked, you're ready to add features!

---

## Testing Workflow

Each session, follow this:

```
1. Open Scene
2. Press Play
3. Test Movement
   └─ Arrow keys work?
4. Test Interaction
   └─ Space key opens dialogue?
5. Test Encounters
   └─ Walk through grass → battle appears?
6. Test Catch
   └─ Catch button → probability calculates?
7. Test Inventory
   └─ I key shows caught creatures?
8. Press Stop
9. Review Console
   └─ Any errors?
10. Iterate
```

Happy building! 🎮
