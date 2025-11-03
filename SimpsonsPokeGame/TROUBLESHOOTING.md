# Common Issues & Solutions

## Installation & Setup

### Q: Unity won't open the project
**A:** Ensure the project folder is named correctly and contains an `Assets/` folder.
- Delete `Library/` folder (regenerates on next open)
- Delete `.sln` files and regenerate via **Assets → Open C# Project**

### Q: Scripts don't compile
**A:** Missing references or syntax errors.
- Check **Console** window for error messages (Ctrl+Shift+C)
- Ensure all file names match class names (e.g., `PlayerController.cs` contains `class PlayerController`)
- Verify using statements are correct

### Q: "EventSystem" error
**A:** No UI EventSystem in scene.
- **Right-click Canvas → EventSystem** OR
- **Hierarchy → Create → UI → Event System**

---

## Gameplay Issues

### Encounters never trigger
**Checklist:**
- [ ] Player GameObject has `Player` tag?
- [ ] Grass area has **BoxCollider2D** with `isTrigger: true`?
- [ ] `EncounterTrigger` script attached to grass area?
- [ ] `possibleCreatureIDs` list is not empty?
- [ ] `BattleSystem.Instance` is assigned in hierarchy?

**Fix:**
```csharp
// Debug log in EncounterTrigger
void TryEncounter() {
    Debug.Log($"Encounter roll: {Random.value} vs {encounterChance}");
    // ... rest of code
}
```

### Player falls through ground
**Checklist:**
- [ ] Ground has **BoxCollider2D** with `isTrigger: false`?
- [ ] Player has **Rigidbody2D** with `Gravity Scale: 0`?
- [ ] Player Rigidbody is `Body Type: Dynamic`?

**Fix:** Select Player → Inspector → Rigidbody2D → Set these values explicitly.

### NPCs don't respond to interaction
**Checklist:**
- [ ] NPC has **BoxCollider2D** with `isTrigger: false`?
- [ ] NPC script `NPCController` attached?
- [ ] NPC is in range when pressing Space?
- [ ] Player raycast direction is correct?

**Debug:**
```csharp
// In PlayerController.TryInteract()
RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.5f);
Debug.DrawRay(transform.position, direction * 1.5f, Color.red, 1f); // Shows raycast
```

### Battle UI buttons don't work
**Checklist:**
- [ ] Buttons have **Button** component (not just Image)?
- [ ] **EventSystem** exists in scene?
- [ ] Button onClick listeners assigned in code or Inspector?
- [ ] `BattleSystem` script has `Start()` method with `onClick.AddListener()`?

**Test:**
1. Click button in Play mode
2. Check Console for errors
3. Verify function name is correct

---

## UI & Display Issues

### Dialogue not showing
**Checklist:**
- [ ] `UIManager` script attached to GameObject in scene?
- [ ] `dialoguePanel` assigned in Inspector?
- [ ] Panel was set to inactive (disabled)?
- [ ] Text fields have actual text content?

**Fix:**
```csharp
// Manually test in Inspector
UIManager.Instance.ShowDialogue("Test NPC", "Hello!");
```

### Inventory not updating
**Checklist:**
- [ ] `Inventory` script attached to persistent GameObject?
- [ ] `Inventory.Instance` is not null?
- [ ] `caughtCreatureIDs` list is public?

**Test:**
```csharp
// In Console: Ctrl+Shift+C, type in Play mode
Inventory.Instance.AddCaught("springbolt");
Debug.Log(Inventory.Instance.caughtCreatureIDs.Count);
```

### Screen tearing / performance lag
**Solutions:**
1. **Reduce update frequency:** In `Update()`, add checks for significant position changes
2. **Limit physics queries:** Cache Raycasts if calling frequently
3. **Profile:** **Window → Analysis → Profiler** to find bottlenecks

---

## Save/Load Issues

### Save file not found
**Debug:**
```csharp
Debug.Log(Application.persistentDataPath); // Shows where saves go
```

**Note:** Save path differs per platform:
- **Windows:** `C:\Users\[Username]\AppData\LocalLow\[Company]\[ProductName]\`
- **Mac:** `~/Library/Application Support/[Company]/[ProductName]/`
- **Android:** Internal storage in app folder

### Save contains old data
**Solution:** Clear old save before testing
```csharp
// In Console during Play mode
PlayerPrefs.DeleteAll();
System.IO.File.Delete(Application.persistentDataPath + "/savegame.json");
```

---

## Performance Issues

### Game runs slow
**Checklist:**
- [ ] Sprite pixel density is reasonable (not 1 pixel = 1 meter)?
- [ ] Physics updates aren't too frequent?
- [ ] Not spawning hundreds of GameObjects?

**Optimization:**
1. **Object Pooling:** Reuse bullets/enemies instead of Instantiate/Destroy
2. **Batch Rendering:** Use sorting layers for depth
3. **Reduce Collider Count:** Use simple shapes, combine when possible

---

## Audio Issues

### Music not playing
**Checklist:**
- [ ] AudioSource component added?
- [ ] AudioClip assigned?
- [ ] **Play On Awake** is checked?
- [ ] Volume is not 0?

### Sound effects cut off
**Solution:** Use `PlayOneShot()` instead of `Play()` for SFX:
```csharp
audioSource.PlayOneShot(soundClip); // Multiple can play simultaneously
// NOT audioSource.Play(); // Stops previous sound
```

---

## Animation Issues

### Character not animating
**Checklist:**
- [ ] GameObject has **Animator** component?
- [ ] Animator Controller assigned?
- [ ] Correct parameter names in code (case-sensitive)?

**Test:**
```csharp
// Check if animator exists
if (animator == null) Debug.Log("Animator is null!");
```

### Animation triggers don't work
**Solution:** Use **SetBool()** or **SetFloat()** for continuous parameters:
```csharp
// Use this
animator.SetFloat("Speed", moveSpeed);

// NOT this for continuous
animator.SetTrigger("Walk"); // Triggers only fire once
```

---

## Build & Deployment Issues

### Build is much larger than expected
**Solution:** Remove unused assets
- **Assets → Find References**
- Delete unused sprites, audio files

### Standalone exe runs differently than editor
**Checklist:**
- [ ] Using correct physics settings for 2D?
- [ ] Asset references valid (not missing)?
- [ ] TextMesh Pro fonts included?

### WebGL build is slow
**Optimization:**
1. Reduce texture sizes
2. Optimize scripts (less garbage collection)
3. Use simpler physics

---

## Debug Tips

### Enable Debug Logging
```csharp
// Add to any script to trace execution
Debug.Log($"Function called with {value}");
Debug.LogWarning($"Unexpected state: {state}");
Debug.LogError($"Critical error: {error}");
```

### Visual Debugging
```csharp
// Draw raycast
Debug.DrawRay(start, direction * distance, Color.red, 2f);

// Draw line between points
Debug.DrawLine(from, to, Color.green, 2f);

// Draw wire cube
Debug.DrawLine(min, max, Color.blue, 2f);
```

### Console Commands
```csharp
// Type in Console during Play mode (Ctrl+Shift+C)
Time.timeScale = 0; // Pause game
Time.timeScale = 1; // Resume

// List all GameObjects
FindObjectsOfType<GameObject>().Length;

// Find specific object
FindObjectOfType<PlayerController>();
```

---

## Getting Help

1. **Check Unity Documentation:** https://docs.unity3d.com/
2. **Search Stack Overflow:** Most issues have solutions
3. **Unity Forums:** https://forum.unity.com/
4. **Discord Communities:** #gamedev channels for quick help
5. **YouTube Tutorials:** Search "[your issue] Unity 2021"

---

## Prevention Tips

- **Save often:** Ctrl+S in scene editor
- **Version control:** Use Git to track changes
- **Test frequently:** Play test after each script change
- **Organize:** Keep Assets folder structure clean
- **Backup:** Regular backups of project folder

---

**Most issues are small syntax errors or missing Inspector assignments. Check these first!** 🔍
