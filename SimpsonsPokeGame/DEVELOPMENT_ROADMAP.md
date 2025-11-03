# Simpsons Pocket Monster - Development Roadmap

## Current Status: **Phase 1 - Prototype Core**

---

## Phase 1: Core Prototype ✅ COMPLETE

### Completed Features:
- ✅ **Player Movement** - Top-down directional movement (WASD/Arrows)
- ✅ **NPC System** - Basic NPC interaction with dialogue
- ✅ **Creature Database** - 4 sample creatures with stats
- ✅ **Wild Encounters** - Random encounter trigger in grass areas
- ✅ **Battle UI** - Simple battle interface with actions
- ✅ **Catch System** - Probability-based creature catching
- ✅ **Inventory** - Pokeballs and caught creatures tracking
- ✅ **Save/Load System** - JSON-based persistence

### Scripts:
- `PlayerController.cs` - Movement & interaction
- `EncounterTrigger.cs` - Wild encounter logic
- `BattleSystem.cs` - Battle flow control
- `CatchSystem.cs` - Catch probability calculation
- `NPCController.cs` - NPC dialogue interaction
- `UIManager.cs` - UI panel management
- `Inventory.cs` - Player inventory management
- `CreatureDatabase.cs` - Creature data lookup
- `SaveSystem.cs` - Save/load functionality

---

## Phase 2: Expanded Battles (Recommended Next)

### Features to Add:
- [ ] **Move System** - Creatures have attacks (e.g., "Scratch", "Thunder", "Heal")
- [ ] **Experience & Leveling** - Creatures gain XP and level up
- [ ] **Type System** - Creature types (Fire, Water, Grass, Electric, etc.)
- [ ] **Status Effects** - Burn, Paralyze, Sleep, Poison
- [ ] **Items in Battle** - Use items (potions, revival, etc.)
- [ ] **Stat Growth** - HP, Attack, Defense increase per level

### New Classes:
```csharp
public class Move {
    public string name;
    public int power;
    public int accuracy; // 0-100
    public string type;
    public int pp; // Power Points (uses per battle)
}

public class CreatureInstance {
    public Creature base;
    public int level;
    public int experience;
    public List<Move> moves;
    public Dictionary<string, int> stats; // HP, ATK, DEF, etc.
}
```

### Implementation Steps:
1. Extend `Creature` to support moves
2. Create `Move` class
3. Update `BattleSystem` to cycle through moves
4. Add XP calculation after battle win
5. Implement leveling UI

---

## Phase 3: Trainer Battles

### Features to Add:
- [ ] **Trainer AI** - NPC trainers with Pokémon teams
- [ ] **Team Management** - Player can have multiple creatures
- [ ] **Battle Switching** - Switch creatures mid-battle
- [ ] **Trainer Progression** - Difficulty scaling
- [ ] **Badge/Reward System** - Unlock areas by defeating trainers

### New Classes:
```csharp
public class Trainer {
    public string name;
    public List<CreatureInstance> team;
    public int difficulty; // 1-5 stars
}

public class PlayerTeam {
    public List<CreatureInstance> party; // Max 6
}
```

---

## Phase 4: World Expansion

### Features to Add:
- [ ] **Multiple Areas/Towns** - Springfield districts
- [ ] **Day/Night Cycle** - Different creatures spawn at different times
- [ ] **Weather System** - Rain, snow affecting encounters
- [ ] **NPCs with Quests** - Simple questlines (fetch, defeat trainer)
- [ ] **Shops** - Buy/sell items and Pokeballs
- [ ] **Gyms/Landmarks** - Trainer areas with themes

### Scene Structure:
```
MainScene (HUB)
├── TownScene (Moe's Tavern area)
├── ForestScene (Spooky Woods)
├── CityScene (Downtown)
└── MountainScene (Mt. Snowball)
```

---

## Phase 5: Polish & Refinement

### Features to Add:
- [ ] **Animations** - Walk cycles, attack animations, catch animation
- [ ] **Sound Design** - BGM per area, SFX for actions
- [ ] **Particle Effects** - Catch sparkle, attack effects, level-up glow
- [ ] **Menu System** - Start screen, pause menu, settings
- [ ] **Difficulty Modes** - Easy, Normal, Hard
- [ ] **Achievements** - Catch all creatures, beat all trainers, etc.
- [ ] **Leaderboard** - Track playtime, creatures caught, etc.

---

## Phase 6: Advanced Features (Optional)

- [ ] **PvP Battles** - Player vs Player (local multiplayer)
- [ ] **Trading System** - Exchange creatures between players
- [ ] **Breeding System** - Create new creatures from two adults
- [ ] **Evolution System** - Creatures evolve at level/conditions
- [ ] **Custom Move Learner** - Creatures can learn new moves
- [ ] **Hidden Stats** - IVs (Individual Values) for min-maxing
- [ ] **Rare Encounters** - Shiny creatures, legendary encounters

---

## Development Timeline

| Phase | Estimated Time | Priority |
|-------|-----------------|----------|
| 1. Core Prototype | 1-2 weeks | 🔴 **DONE** |
| 2. Expanded Battles | 1 week | 🟢 **NEXT** |
| 3. Trainer Battles | 1-2 weeks | 🟡 **HIGH** |
| 4. World Expansion | 2-3 weeks | 🟡 **HIGH** |
| 5. Polish & Audio | 1-2 weeks | 🟡 **MEDIUM** |
| 6. Advanced Features | 2+ weeks | 🔵 **LOW** |

---

## Quick Wins (Next Session)

1. **Add one move per creature** - Just hardcode 1-2 moves per creature
2. **Implement basic XP system** - Creatures gain 10 XP per battle
3. **Add leveling UI** - Display level-up notifications
4. **Create 2nd area** - Copy MainScene, change sprite colors/layout
5. **Add a Trainer NPC** - Use BattleSystem but with multiple creatures

---

## Code Patterns to Remember

### Adding a Feature
1. Create/modify data class (in `Data/`)
2. Add logic to relevant system (Battle, NPC, etc.)
3. Update UI to display new info
4. Test in Play mode
5. Iterate on balance

### Testing Checklist
- [ ] Feature works in isolation
- [ ] No console errors
- [ ] UI updates properly
- [ ] Save/load persists changes
- [ ] No performance degradation

---

## Resources & Inspiration

- **Pokémon Red/Blue Design** - https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_Red_and_Blue
- **Unity 2D Tutorials** - https://unity.com/learn/pathways/beginner-2d-game
- **Game Feel** (Additive Animation) - https://www.gamasutra.com/view/feature/134411/game_feel_the_game_design_of.php
- **Simpsons Art Style** - Reference episodes or fan art on DeviantArt

---

## Notes

- Keep scope manageable; playable > polished
- Balance gameplay before adding content
- Test frequently to catch bugs early
- Get feedback from testers between phases
- Save often; version control is your friend!

---

**Let's make this game! 🎮**
