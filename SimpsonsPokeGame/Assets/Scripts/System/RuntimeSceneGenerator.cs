using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Simpsons Pocket Monster - Runtime Scene Generator
/// 
/// Creates the entire game scene at runtime (when game starts).
/// Attach this to an empty GameObject and enable it to auto-generate the scene.
/// 
/// Place this in: Assets/Scripts/System/RuntimeSceneGenerator.cs
/// 
/// Usage:
/// 1. Create empty GameObject
/// 2. Attach this script
/// 3. Check "Auto Generate" in Inspector
/// 4. Press Play
/// </summary>
public class RuntimeSceneGenerator : MonoBehaviour
{
    [Header("Auto Generation")]
    public bool autoGenerateOnStart = true;

    [Header("Settings")]
    public float playerMoveSpeed = 4f;
    public float encounterChance = 0.12f;
    public int pokeballCount = 20;
    public bool useColorsForArt = true;

    private static bool sceneAlreadyGenerated = false;

    void Start()
    {
        if (autoGenerateOnStart && !sceneAlreadyGenerated)
        {
            GenerateCompleteScene();
            sceneAlreadyGenerated = true;
        }
    }

    /// <summary>
    /// Generates entire scene at runtime
    /// </summary>
    public void GenerateCompleteScene()
    {
        Debug.Log("🎮 Generating Simpsons Pocket Monster scene...");

        // Cleanup default objects
        CleanupDefaultObjects();

        // Create scene hierarchy
        CreateGround();
        CreatePlayer();
        CreateWildGrassArea();
        CreateNPCs();
        CreateUICanvas();
        CreateGameManagers();

        Debug.Log("✅ Scene generation complete! Press Play to start.");
    }

    private void CleanupDefaultObjects()
    {
        GameObject[] defaultObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in defaultObjects)
        {
            if (obj.name == "Main Camera" && obj.GetComponent<Camera>() != null && FindObjectsOfType<Camera>().Length > 1)
                Destroy(obj);
        }
    }

    private void CreateGround()
    {
        GameObject ground = new GameObject("Ground");
        ground.transform.position = Vector3.zero;
        ground.transform.localScale = new Vector3(20, 10, 1);

        // Sprite
        SpriteRenderer sr = ground.AddComponent<SpriteRenderer>();
        if (useColorsForArt)
            sr.color = new Color(0f, 0.267f, 0f); // Dark green

        // Collider
        BoxCollider2D collider = ground.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);

        Debug.Log("✓ Ground created");
    }

    private void CreatePlayer()
    {
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        player.transform.position = new Vector3(0, 1, 0);
        player.transform.localScale = Vector3.one;

        // Sprite Renderer
        SpriteRenderer sr = player.AddComponent<SpriteRenderer>();
        if (useColorsForArt)
            sr.color = new Color(0f, 0.4f, 1f); // Blue
        sr.sortingOrder = 1;

        // Rigidbody2D
        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Collider
        BoxCollider2D collider = player.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);

        // Animator
        player.AddComponent<Animator>();

        // Player Controller
        PlayerController pc = player.AddComponent<PlayerController>();
        pc.moveSpeed = playerMoveSpeed;

        Debug.Log("✓ Player created");
    }

    private void CreateWildGrassArea()
    {
        GameObject ground = GameObject.Find("Ground");
        if (ground == null)
        {
            Debug.LogError("Ground not found!");
            return;
        }

        GameObject grassArea = new GameObject("WildGrassArea");
        grassArea.transform.SetParent(ground.transform);
        grassArea.transform.position = new Vector3(3, 0, 0);
        grassArea.transform.localScale = new Vector3(5, 3, 1);

        // Sprite
        SpriteRenderer sr = grassArea.AddComponent<SpriteRenderer>();
        if (useColorsForArt)
            sr.color = new Color(0f, 0.8f, 0f); // Light green
        sr.sortingOrder = 0;

        // Trigger Collider
        BoxCollider2D collider = grassArea.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(1, 1);

        // Encounter Trigger Script
        EncounterTrigger trigger = grassArea.AddComponent<EncounterTrigger>();
        trigger.possibleCreatureIDs = new string[] { "springbolt", "beerling", "donuragon", "saxasaurus" };
        trigger.encounterChance = encounterChance;

        Debug.Log("✓ Wild Grass Area created");
    }

    private void CreateNPCs()
    {
        GameObject ground = GameObject.Find("Ground");
        if (ground == null) return;

        CreateNPC(ground, "NPC_Barkeep", new Vector3(-3, 0, 0), Color.red, "Barkeep", "Welcome to Moe's Tavern!");
        CreateNPC(ground, "NPC_Homer", new Vector3(-3, -3, 0), new Color(1f, 0.8f, 0f), "Homer", "D'oh! Gotta catch 'em all!");
        CreateNPC(ground, "NPC_Marge", new Vector3(5, 3, 0), new Color(0.2f, 0.8f, 1f), "Marge", "Be safe on your adventure!");

        Debug.Log("✓ NPCs created");
    }

    private void CreateNPC(GameObject parent, string name, Vector3 position, Color color, string npcName, string dialogue)
    {
        GameObject npc = new GameObject(name);
        npc.transform.SetParent(parent.transform);
        npc.transform.position = position;
        npc.transform.localScale = new Vector3(0.8f, 0.8f, 1);

        // Sprite
        SpriteRenderer sr = npc.AddComponent<SpriteRenderer>();
        if (useColorsForArt)
            sr.color = color;
        sr.sortingOrder = 1;

        // Collider
        BoxCollider2D collider = npc.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);

        // NPC Controller
        NPCController controller = npc.AddComponent<NPCController>();
        controller.npcName = npcName;
        controller.dialogue = dialogue;
    }

    private void CreateUICanvas()
    {
        // Canvas
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasGO.AddComponent<GraphicRaycaster>();

        // Battle Panel
        GameObject battlePanel = CreatePanel(canvasGO, "BattlePanel", new Color(0, 0, 0, 0.8f), false);
        CreateText(battlePanel, "InfoText", "Battle Info Here", 20);
        CreateButton(battlePanel, "FightButton", "Fight", new Vector3(200, -100, 0));
        CreateButton(battlePanel, "CatchButton", "Catch", new Vector3(0, -100, 0));
        CreateButton(battlePanel, "RunButton", "Run", new Vector3(-200, -100, 0));

        // Dialogue Panel
        GameObject dialoguePanel = CreatePanel(canvasGO, "DialoguePanel", new Color(0.08f, 0.08f, 0.24f, 0.8f), false);
        CreateText(dialoguePanel, "NameText", "NPC Name", 24);
        CreateText(dialoguePanel, "ContentText", "Dialogue content", 20);

        // Inventory Panel
        GameObject inventoryPanel = CreatePanel(canvasGO, "InventoryPanel", new Color(0, 0, 0, 0.8f), false);
        CreateText(inventoryPanel, "InventoryText", "Inventory", 20);

        // Event System
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        Debug.Log("✓ UI Canvas created");
    }

    private GameObject CreatePanel(GameObject parent, string name, Color color, bool active)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent.transform, false);

        Image image = panel.AddComponent<Image>();
        image.color = color;

        RectTransform rect = panel.GetComponent<RectTransform>();
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        panel.SetActive(active);
        return panel;
    }

    private void CreateText(GameObject parent, string name, string content, int fontSize)
    {
        GameObject textGO = new GameObject(name);
        textGO.transform.SetParent(parent.transform, false);

        Text text = textGO.AddComponent<Text>();
        text.text = content;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = fontSize;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;

        RectTransform rect = textGO.GetComponent<RectTransform>();
        rect.offsetMin = new Vector2(10, 10);
        rect.offsetMax = new Vector2(-10, -10);
    }

    private void CreateButton(GameObject parent, string name, string label, Vector3 position)
    {
        GameObject buttonGO = new GameObject(name);
        buttonGO.transform.SetParent(parent.transform, false);

        Button button = buttonGO.AddComponent<Button>();
        Image image = buttonGO.AddComponent<Image>();
        image.color = new Color(0.2f, 0.2f, 0.2f);

        RectTransform rect = buttonGO.GetComponent<RectTransform>();
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(120, 50);

        // Button text
        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(buttonGO.transform, false);
        Text text = textGO.AddComponent<Text>();
        text.text = label;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 16;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;

        RectTransform textRect = textGO.GetComponent<RectTransform>();
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;
    }

    private void CreateGameManagers()
    {
        // GameManager
        GameObject gameManager = new GameObject("GameManager");
        gameManager.transform.position = Vector3.zero;

        BattleSystem battleSystem = gameManager.AddComponent<BattleSystem>();
        gameManager.AddComponent<CatchSystem>();
        gameManager.AddComponent<UIManager>();
        gameManager.AddComponent<SaveSystem>();

        // Link UI references
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            Transform battlePanelTransform = canvas.transform.Find("BattlePanel");
            if (battlePanelTransform != null)
            {
                battleSystem.battleUIPanel = battlePanelTransform.gameObject;
                battleSystem.infoText = battlePanelTransform.Find("InfoText")?.GetComponent<Text>();
                battleSystem.fightButton = battlePanelTransform.Find("FightButton")?.GetComponent<Button>();
                battleSystem.catchButton = battlePanelTransform.Find("CatchButton")?.GetComponent<Button>();
                battleSystem.runButton = battlePanelTransform.Find("RunButton")?.GetComponent<Button>();
            }
        }

        // Inventory Manager
        GameObject inventoryManager = new GameObject("InventoryManager");
        inventoryManager.transform.position = Vector3.zero;
        Inventory inventory = inventoryManager.AddComponent<Inventory>();
        inventory.pokeballCount = pokeballCount;

        Debug.Log("✓ Game Managers created");
    }
}
