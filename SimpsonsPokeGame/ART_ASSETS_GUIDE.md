# Art & Assets Guide

## Placeholder Art (Quick Start)

Use colored rectangles to prototype gameplay quickly:

| Element | Color | Size |
|---------|-------|------|
| Player | Blue (#0066FF) | 1x1 |
| NPC | Red (#FF0000) | 0.8x0.8 |
| Wild Creature | Yellow (#FFFF00) | 1.5x1.5 |
| Ground | Dark Green (#004400) | 20x10 |
| Wild Grass Area | Light Green (#00CC00) | 5x3 |

## Free Art Resources

### 2D Sprite Packs
- **Kenney.nl** - Free, high-quality 2D assets
  - Categories: Characters, Monsters, Top-Down
  - Link: https://kenney.nl/assets
- **Itch.io** - Community game assets
  - Search: "Simpsons sprites", "Pokemon-like", "2D top-down"
  - Link: https://itch.io
- **OpenGameArt.org** - Open-source game art
  - Link: https://opengameart.org

### Character Art
- **LPC Sprite Sheet** - Free character base with many poses
- **MV Character Generator** - Customizable 2D characters

### UI Elements
- **Kenney UI Pack** - Free buttons, panels, icons
- **OpenUI** - Free UI element pack

## Importing Sprites into Unity

1. Drag `.png` or `.jpg` file into `Assets/Art/`
2. Select sprite in Inspector
3. Set **Sprite Mode:** `Single` (for individual sprites) or `Multiple` (for sprite sheets)
4. Set **Pixels Per Unit:** `16` (common for pixel art) or `100` (default)
5. Check **Read/Write Enabled** if needed for scripting
6. Click **Apply**

## Sprite Sheet Setup

If using sprite sheets (multiple sprites in one image):

1. Select sprite in `Assets/Art/`
2. In Inspector, set **Sprite Mode:** `Multiple`
3. Click **Sprite Editor**
4. In Sprite Editor window:
   - Click **Slice** → **Grid By Cell Size**
   - Set **Pixel Size:** e.g., (16, 16) for tile size
   - Click **Slice**
   - Click **Apply**
5. Now individual sprites are accessible in code

## Animation Setup

### Create a Simple Walk Animation
1. Select your Player sprite
2. Drag it to the scene to create a GameObject (if not already)
3. In Project, create **Animation Clip:**
   - **Right-click** in `Assets/Art/` → **Create** → **Animation Clip**
   - Name it `Player_Walk`
4. In Animator window (open from the Player GameObject):
   - Create **Animator Controller:** `PlayerAnimator`
   - Assign to Player's **Animator** component
5. Drag walk-cycle sprites into the timeline
6. Set **Sample Rate:** 10 fps (adjust for speed)
7. In code, call `animator.SetBool("Walking", true)` to trigger

### Simple Animator Setup
In `PlayerController.cs`, we already have:
```csharp
animator.SetFloat("MoveX", move.x);
animator.SetFloat("MoveY", move.y);
animator.SetFloat("Speed", move.sqrMagnitude);
```

Create Animator states:
- **Idle** (no movement)
- **Walk** (any direction)

## Audio Setup

### Add Background Music
1. Import `.wav` or `.mp3` to `Assets/Audio/`
2. Create empty GameObject → name it `AudioManager`
3. Add **AudioSource** component
4. Drag music file into the `AudioClip` field
5. Check **Loop**
6. In code:
   ```csharp
   AudioSource.playOnAwake = true;
   ```

### Add Sound Effects
1. Create `AudioSource` for each SFX type:
   - `EncounterSFX`
   - `CatchSFX`
   - `BattleSFX`
2. Assign clips in Inspector
3. In code, call:
   ```csharp
   audioSource.PlayOneShot(catchSoundClip);
   ```

## Simpsons-Themed Art Tips

### Style
- **Chunky outlines:** Black lines around shapes
- **Flat colors:** Minimal shading, bright hues
- **Cartoony proportions:** Large heads, small bodies
- **Yellow skin tones:** For Simpsons characters (hex `#FFD700`)

### Color Palette
| Element | Color |
|---------|-------|
| Simpsons Yellow | #FFD700 |
| Springfield Green | #00AA00 |
| Duff Blue | #0066FF |
| Neon Danger Red | #FF0000 |
| Dark Outline | #000000 |

### Creating Simple Sprites
Use free tools:
- **Aseprite** (paid, $20 - best for pixel art)
- **Piskel** (free web-based)
- **LibreSprite** (free, open-source fork of Aseprite)
- **GIMP** (free Photoshop alternative)

## Organizing Assets

```
Assets/
├── Art/
│   ├── Player/
│   │   ├── player_idle.png
│   │   └── player_walk.png
│   ├── Creatures/
│   │   ├── springbolt.png
│   │   ├── beerling.png
│   │   └── ...
│   ├── NPCs/
│   │   ├── barkeep.png
│   │   └── ...
│   ├── UI/
│   │   ├── buttons.png
│   │   └── icons.png
│   └── Environment/
│       ├── ground_tileset.png
│       └── grass_tileset.png
├── Audio/
│   ├── Music/
│   │   └── encounter_loop.mp3
│   ├── SFX/
│   │   ├── throw_sound.wav
│   │   ├── catch_sound.wav
│   │   └── faint_sound.wav
└── ...
```

## Next Steps

1. **Start with placeholders** (colored rectangles) to test gameplay
2. **Commission or download** simple Simpsons-style art
3. **Import and assign** sprites to GameObjects
4. **Test animations** with Animator window
5. **Add audio** for immersion
6. **Iterate** based on gameplay feedback

---

Remember: **Gameplay first, art second!** A fun game with placeholder art beats a beautiful game with bad mechanics.
