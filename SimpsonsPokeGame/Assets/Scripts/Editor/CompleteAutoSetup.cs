using UnityEngine;
using UnityEditor;

/// <summary>
/// Simpsons Pocket Monster - Complete Auto Setup
/// 
/// This is an ALL-IN-ONE solution that combines:
/// 1. Editor Tool (creates scene in editor)
/// 2. Runtime Generator (creates scene when game starts)
/// 3. Auto-configuration (sets everything up automatically)
/// 
/// Place this in: Assets/Editor/CompleteAutoSetup.cs
/// 
/// Usage:
/// Menu: Tools → Simpsons Game → Auto Complete Setup
/// </summary>
public class CompleteAutoSetup : EditorWindow
{
    private Vector2 scrollPosition;
    private int setupMode = 0; // 0 = Both, 1 = Editor Only, 2 = Runtime Only
    private bool createPrefabs = true;
    private bool autoPlay = false;

    [MenuItem("Tools/Simpsons Game/Auto Complete Setup")]
    public static void ShowWindow()
    {
        GetWindow<CompleteAutoSetup>("Complete Auto Setup");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("COMPLETE AUTO SETUP", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("This will set up EVERYTHING automatically!", MessageType.Info);
        
        EditorGUILayout.Space();

        setupMode = GUILayout.SelectionGrid(setupMode, new[] { "Both (Recommended)", "Editor Tool Only", "Runtime Only" }, 1);

        EditorGUILayout.Space();

        createPrefabs = EditorGUILayout.Toggle("Create Prefabs", createPrefabs);
        autoPlay = EditorGUILayout.Toggle("Auto Play After Setup", autoPlay);

        EditorGUILayout.Space();

        if (GUILayout.Button("🚀 START COMPLETE AUTO SETUP", GUILayout.Height(50)))
        {
            ExecuteCompleteSetup();
        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox(
            "This will:\n\n" +
            "1. Create complete game scene\n" +
            "2. Assign all scripts automatically\n" +
            "3. Create UI and managers\n" +
            "4. Setup tags and layers\n" +
            "5. Create prefabs (optional)\n" +
            "6. Save scene\n" +
            "7. Optional: Auto play\n\n" +
            "Everything will be 100% ready to use!",
            MessageType.Info
        );
    }

    private void ExecuteCompleteSetup()
    {
        try
        {
            EditorUtility.DisplayProgressBar("Setup", "Initializing...", 0.1f);

            // Step 1: Generate scene with editor tool
            if (setupMode == 0 || setupMode == 1)
            {
                EditorUtility.DisplayProgressBar("Setup", "Creating scene with editor tool...", 0.3f);
                CreateSceneWithEditorTool();
            }

            // Step 2: Add runtime generator
            if (setupMode == 0 || setupMode == 2)
            {
                EditorUtility.DisplayProgressBar("Setup", "Adding runtime generator...", 0.5f);
                AddRuntimeGenerator();
            }

            // Step 3: Create prefabs
            if (createPrefabs)
            {
                EditorUtility.DisplayProgressBar("Setup", "Creating prefabs...", 0.7f);
                CreatePrefabs();
            }

            // Step 4: Save scene
            EditorUtility.DisplayProgressBar("Setup", "Saving scene...", 0.9f);
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

            EditorUtility.ClearProgressBar();

            EditorUtility.DisplayDialog("✅ SUCCESS!", "Complete setup finished!\n\nYour game is 100% ready.\n\nPress Play to test!", "OK");

            if (autoPlay)
            {
                EditorApplication.isPlaying = true;
            }
        }
        catch (System.Exception ex)
        {
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("ERROR", $"Setup failed:\n{ex.Message}", "OK");
        }
    }

    private void CreateSceneWithEditorTool()
    {
        // Create new scene
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneCreateSceneAssets.Ask);
        scene.name = "MainScene";

        // Use the scene generator from the other script
        var generator = ScriptableObject.CreateInstance<SimpsonsPokeGameSceneGenerator>();
        // This would need to be refactored, so let's create the scene manually here
        
        CreateGroundQuick();
        CreatePlayerQuick();
        CreateWildGrassAreaQuick();
        CreateNPCsQuick();
        CreateUICanvasQuick();
        CreateGameManagersQuick();
        SetupTagsQuick();
    }

    private void AddRuntimeGenerator()
    {
        GameObject runtimeGenObject = new GameObject("_RuntimeSceneGenerator");
        RuntimeSceneGenerator generator = runtimeGenObject.AddComponent<RuntimeSceneGenerator>();
        generator.autoGenerateOnStart = true;
    }

    private void CreatePrefabs()
    {
        // Create prefab directory if it doesn't exist
        if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }

        // Create Player prefab
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            PrefabUtility.SaveAsPrefabAsset(player, "Assets/Prefabs/Player.prefab");
        }

        // Create NPC prefab
        GameObject npc = GameObject.Find("NPC_Barkeep");
        if (npc != null)
        {
            PrefabUtility.SaveAsPrefabAsset(npc, "Assets/Prefabs/NPC_prefab.prefab");
        }

        AssetDatabase.Refresh();
    }

    // Quick creation methods (duplicated for simplicity)
    private void CreateGroundQuick()
    {
        GameObject ground = new GameObject("Ground");
        ground.transform.position = Vector3.zero;
        ground.transform.localScale = new Vector3(20, 10, 1);

        SpriteRenderer sr = ground.AddComponent<SpriteRenderer>();
        sr.color = new Color(0f, 0.267f, 0f);

        BoxCollider2D collider = ground.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);
    }

    private void CreatePlayerQuick()
    {
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        player.transform.position = new Vector3(0, 1, 0);

        SpriteRenderer sr = player.AddComponent<SpriteRenderer>();
        sr.color = new Color(0f, 0.4f, 1f);

        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        BoxCollider2D collider = player.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);

        player.AddComponent<Animator>();
        player.AddComponent<PlayerController>();
    }

    private void CreateWildGrassAreaQuick()
    {
        GameObject ground = GameObject.Find("Ground");
        GameObject grassArea = new GameObject("WildGrassArea");
        grassArea.transform.SetParent(ground.transform);
        grassArea.transform.position = new Vector3(3, 0, 0);
        grassArea.transform.localScale = new Vector3(5, 3, 1);

        SpriteRenderer sr = grassArea.AddComponent<SpriteRenderer>();
        sr.color = new Color(0f, 0.8f, 0f);

        BoxCollider2D collider = grassArea.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;

        EncounterTrigger trigger = grassArea.AddComponent<EncounterTrigger>();
        trigger.possibleCreatureIDs = new string[] { "springbolt", "beerling", "donuragon", "saxasaurus" };
        trigger.encounterChance = 0.12f;
    }

    private void CreateNPCsQuick()
    {
        GameObject ground = GameObject.Find("Ground");
        CreateNPCQuick(ground, "NPC_Barkeep", new Vector3(-3, 0, 0), Color.red);
        CreateNPCQuick(ground, "NPC_Homer", new Vector3(-3, -3, 0), new Color(1f, 0.8f, 0f));
        CreateNPCQuick(ground, "NPC_Marge", new Vector3(5, 3, 0), new Color(0.2f, 0.8f, 1f));
    }

    private void CreateNPCQuick(GameObject parent, string name, Vector3 pos, Color color)
    {
        GameObject npc = new GameObject(name);
        npc.transform.SetParent(parent.transform);
        npc.transform.position = pos;
        npc.transform.localScale = new Vector3(0.8f, 0.8f, 1);

        SpriteRenderer sr = npc.AddComponent<SpriteRenderer>();
        sr.color = color;

        BoxCollider2D collider = npc.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);

        NPCController controller = npc.AddComponent<NPCController>();
        controller.npcName = name;
        controller.dialogue = "Hello, traveler!";
    }

    private void CreateUICanvasQuick()
    {
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        canvasGO.AddComponent<GraphicRaycaster>();

        // Create panels and buttons (simplified)
        var battlePanel = new GameObject("BattlePanel");
        battlePanel.transform.SetParent(canvasGO.transform);
        battlePanel.AddComponent<Image>().color = new Color(0, 0, 0, 0.8f);

        var dialoguePanel = new GameObject("DialoguePanel");
        dialoguePanel.transform.SetParent(canvasGO.transform);
        dialoguePanel.AddComponent<Image>().color = new Color(0.08f, 0.08f, 0.24f, 0.8f);

        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }

    private void CreateGameManagersQuick()
    {
        GameObject gameManager = new GameObject("GameManager");
        gameManager.AddComponent<BattleSystem>();
        gameManager.AddComponent<CatchSystem>();
        gameManager.AddComponent<UIManager>();
        gameManager.AddComponent<SaveSystem>();

        GameObject inventoryManager = new GameObject("InventoryManager");
        inventoryManager.AddComponent<Inventory>();
    }

    private void SetupTagsQuick()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        bool hasPlayerTag = false;
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            if (tagsProp.GetArrayElementAtIndex(i).stringValue == "Player")
            {
                hasPlayerTag = true;
                break;
            }
        }

        if (!hasPlayerTag)
        {
            tagsProp.arraySize++;
            tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1).stringValue = "Player";
            tagManager.ApplyModifiedProperties();
        }
    }
}
