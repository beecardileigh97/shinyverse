using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Simpsons Pocket Monster - Automatic Scene Generator (Editor Tool)
/// 
/// This script creates the entire game scene automatically with one click!
/// Place this in: Assets/Editor/SceneGenerator.cs
/// 
/// Usage: Window → Simpsons Pocket Monster → Generate Scene
/// </summary>
public class SimpsonsPokeGameSceneGenerator : EditorWindow
{
    private Vector2 scrollPosition;
    private bool showSettings = true;
    private bool showProgress = false;
    private string progressMessage = "";

    // Settings
    private float playerMoveSpeed = 4f;
    private float encounterChance = 0.12f;
    private int pokeballCount = 20;

    [MenuItem("Window/Simpsons Pocket Monster/Generate Scene")]
    public static void ShowWindow()
    {
        GetWindow<SimpsonsPokeGameSceneGenerator>("Simpsons Game Setup");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Simpsons Pocket Monster - Auto Scene Generator", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Settings Section
        showSettings = EditorGUILayout.Foldout(showSettings, "Settings", true);
        if (showSettings)
        {
            EditorGUI.indentLevel++;
            playerMoveSpeed = EditorGUILayout.FloatField("Player Move Speed", playerMoveSpeed);
            encounterChance = EditorGUILayout.Slider("Encounter Chance", encounterChance, 0f, 1f);
            pokeballCount = EditorGUILayout.IntField("Starting Pokeballs", pokeballCount);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        // Generation Buttons
        EditorGUILayout.LabelField("Scene Generation", EditorStyles.boldLabel);
        
        if (GUILayout.Button("✨ Generate Complete Scene", GUILayout.Height(40)))
        {
            GenerateCompleteScene();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("🔧 Generate Scene Components Only", GUILayout.Height(35)))
        {
            GenerateSceneComponentsOnly();
        }

        if (GUILayout.Button("📦 Create Prefabs", GUILayout.Height(35)))
        {
            CreatePrefabs();
        }

        if (GUILayout.Button("🧹 Clean Scene", GUILayout.Height(35)))
        {
            CleanScene();
        }

        EditorGUILayout.Space();

        // Help Section
        EditorGUILayout.HelpBox(
            "Click 'Generate Complete Scene' to automatically create:\n" +
            "• MainScene with all GameObjects\n" +
            "• Player with controllers\n" +
            "• Ground and grass areas\n" +
            "• NPCs with dialogue\n" +
            "• UI panels (Battle, Dialogue, Inventory)\n" +
            "• All game managers\n" +
            "• Script assignments\n\n" +
            "Everything will be ready to Play immediately!",
            MessageType.Info
        );

        EditorGUILayout.EndScrollView();
    }

    private void GenerateCompleteScene()
    {
        progressMessage = "Generating scene...";
        showProgress = true;

        // Create new scene
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneCreateSceneAssets.Ask);
        scene.name = "MainScene";

        try
        {
            // Destroy default camera/light if exists
            var defaultObjects = FindObjectsOfType<GameObject>();
            foreach (var obj in defaultObjects)
            {
                if (obj.name == "Main Camera" || obj.name == "Directional Light")
                    DestroyImmediate(obj);
            }

            progressMessage = "Creating ground...";
            CreateGround();

            progressMessage = "Creating player...";
            CreatePlayer();

            progressMessage = "Creating encounter areas...";
            CreateWildGrassArea();

            progressMessage = "Creating NPCs...";
            CreateNPCs();

            progressMessage = "Creating UI canvas...";
            CreateUICanvas();

            progressMessage = "Creating managers...";
            CreateGameManagers();

            progressMessage = "Setting up tags...";
            SetupTags();

            // Save scene
            EditorSceneManager.SaveScene(scene, "Assets/Scenes/MainScene.unity");

            EditorUtility.DisplayDialog("Success!", "Scene generated successfully!\n\nPress Play to test the game.", "OK");
            showProgress = false;
        }
        catch (System.Exception ex)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to generate scene:\n{ex.Message}", "OK");
            Debug.LogError($"Scene generation error: {ex}");
            showProgress = false;
        }
    }

    private void GenerateSceneComponentsOnly()
    {
        if (EditorSceneManager.GetActiveScene().isModified)
        {
            EditorUtility.DisplayDialog("Warning", "Save current scene first!", "OK");
            return;
        }

        GenerateCompleteScene();
    }

    private void CreateGround()
    {
        GameObject ground = new GameObject("Ground");
        ground.transform.position = Vector3.zero;
        ground.transform.localScale = new Vector3(20, 10, 1);

        SpriteRenderer sr = ground.AddComponent<SpriteRenderer>();
        sr.color = new Color(0f, 0.267f, 0f); // Dark green

        BoxCollider2D collider = ground.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);
    }

    private void CreatePlayer()
    {
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        player.transform.position = new Vector3(0, 1, 0);
        player.transform.localScale = Vector3.one;

        // Sprite Renderer
        SpriteRenderer sr = player.AddComponent<SpriteRenderer>();
        sr.color = new Color(0f, 0.4f, 1f); // Blue

        // Rigidbody2D
        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Collider
        BoxCollider2D collider = player.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);

        // Animator (for future use)
        player.AddComponent<Animator>();

        // Script
        player.AddComponent<PlayerController>();
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.moveSpeed = playerMoveSpeed;
    }

    private void CreateWildGrassArea()
    {
        GameObject ground = GameObject.Find("Ground");
        GameObject grassArea = new GameObject("WildGrassArea");
        grassArea.transform.SetParent(ground.transform);
        grassArea.transform.position = new Vector3(3, 0, 0);
        grassArea.transform.localScale = new Vector3(5, 3, 1);

        // Sprite
        SpriteRenderer sr = grassArea.AddComponent<SpriteRenderer>();
        sr.color = new Color(0f, 0.8f, 0f); // Light green

        // Trigger Collider
        BoxCollider2D collider = grassArea.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(1, 1);

        // Script
        EncounterTrigger trigger = grassArea.AddComponent<EncounterTrigger>();
        trigger.possibleCreatureIDs = new string[] { "springbolt", "beerling", "donuragon", "saxasaurus" };
        trigger.encounterChance = encounterChance;
    }

    private void CreateNPCs()
    {
        GameObject ground = GameObject.Find("Ground");

        // NPC 1: Barkeep
        CreateNPC(ground, "NPC_Barkeep", new Vector3(-3, 0, 0), Color.red, "Barkeep", "Welcome to Moe's Tavern!");

        // NPC 2: Homer
        CreateNPC(ground, "NPC_Homer", new Vector3(-3, -3, 0), new Color(1f, 0.8f, 0f), "Homer", "D'oh! I'm catching monsters!");

        // NPC 3: Marge
        CreateNPC(ground, "NPC_Marge", new Vector3(5, 3, 0), new Color(0.2f, 0.8f, 1f), "Marge", "Good luck with your adventure!");
    }

    private void CreateNPC(GameObject parent, string name, Vector3 position, Color color, string npcName, string dialogue)
    {
        GameObject npc = new GameObject(name);
        npc.transform.SetParent(parent.transform);
        npc.transform.position = position;
        npc.transform.localScale = new Vector3(0.8f, 0.8f, 1);

        // Sprite
        SpriteRenderer sr = npc.AddComponent<SpriteRenderer>();
        sr.color = color;

        // Collider (NOT trigger)
        BoxCollider2D collider = npc.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);

        // Script
        NPCController controller = npc.AddComponent<NPCController>();
        controller.npcName = npcName;
        controller.dialogue = dialogue;
    }

    private void CreateUICanvas()
    {
        // Main Canvas
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasGO.AddComponent<GraphicRaycaster>();

        // Battle Panel
        CreateUIPanel(canvasGO, "BattlePanel", new Color(0, 0, 0, 0.8f), false);

        // Dialogue Panel
        CreateUIPanel(canvasGO, "DialoguePanel", new Color(0.08f, 0.08f, 0.24f, 0.8f), false);

        // Inventory Panel
        CreateUIPanel(canvasGO, "InventoryPanel", new Color(0, 0, 0, 0.8f), false);

        // Add Event System if not present
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }

    private void CreateUIPanel(GameObject canvas, string panelName, Color color, bool active)
    {
        GameObject panelGO = new GameObject(panelName);
        panelGO.transform.SetParent(canvas.transform, false);

        Image panelImage = panelGO.AddComponent<Image>();
        panelImage.color = color;

        RectTransform rectTransform = panelGO.GetComponent<RectTransform>();
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        panelGO.SetActive(active);

        // Add text child
        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(panelGO.transform, false);
        Text text = textGO.AddComponent<Text>();
        text.text = panelName;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 20;
        text.alignment = TextAnchor.MiddleCenter;

        RectTransform textRect = textGO.GetComponent<RectTransform>();
        textRect.offsetMin = new Vector2(10, 10);
        textRect.offsetMax = new Vector2(-10, -10);

        // Add buttons for Battle Panel
        if (panelName == "BattlePanel")
        {
            CreateUIButton(panelGO, "FightButton", "Fight", new Vector3(200, 0, 0));
            CreateUIButton(panelGO, "CatchButton", "Catch", new Vector3(0, 0, 0));
            CreateUIButton(panelGO, "RunButton", "Run", new Vector3(-200, 0, 0));
        }
    }

    private void CreateUIButton(GameObject parent, string buttonName, string buttonText, Vector3 position)
    {
        GameObject buttonGO = new GameObject(buttonName);
        buttonGO.transform.SetParent(parent.transform, false);

        Button button = buttonGO.AddComponent<Button>();
        Image buttonImage = buttonGO.AddComponent<Image>();
        buttonImage.color = new Color(0.2f, 0.2f, 0.2f);

        RectTransform rect = buttonGO.GetComponent<RectTransform>();
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(150, 60);

        // Button text
        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(buttonGO.transform, false);
        Text text = textGO.AddComponent<Text>();
        text.text = buttonText;
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

        // Assign UI references
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            battleSystem.battleUIPanel = canvas.transform.Find("BattlePanel").gameObject;
            battleSystem.infoText = canvas.transform.Find("BattlePanel/Text").GetComponent<Text>();
            battleSystem.fightButton = canvas.transform.Find("BattlePanel/FightButton").GetComponent<Button>();
            battleSystem.catchButton = canvas.transform.Find("BattlePanel/CatchButton").GetComponent<Button>();
            battleSystem.runButton = canvas.transform.Find("BattlePanel/RunButton").GetComponent<Button>();
        }

        // Inventory Manager
        GameObject inventoryManager = new GameObject("InventoryManager");
        inventoryManager.transform.position = Vector3.zero;
        Inventory inventory = inventoryManager.AddComponent<Inventory>();
        inventory.pokeballCount = pokeballCount;
    }

    private void SetupTags()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        if (!HasTag("Player"))
        {
            tagsProp.arraySize++;
            tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1).stringValue = "Player";
            tagManager.ApplyModifiedProperties();
        }
    }

    private bool HasTag(string tag)
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            if (tagsProp.GetArrayElementAtIndex(i).stringValue == tag)
                return true;
        }
        return false;
    }

    private void CreatePrefabs()
    {
        EditorUtility.DisplayDialog("Info", "Create prefabs from existing GameObjects in scene.\n\nDrag GameObjects into Assets/Prefabs/", "OK");
    }

    private void CleanScene()
    {
        if (EditorUtility.DisplayDialog("Clean Scene?", "Remove all game objects?", "Yes", "No"))
        {
            var roots = EditorSceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var root in roots)
            {
                if (root.name != "EventSystem")
                    DestroyImmediate(root);
            }
        }
    }
}
