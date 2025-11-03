using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class AutoSceneInitializer
{
    private static bool hasRun = false;

    [InitializeOnLoadMethod]
    private static void OnProjectLoad()
    {
        if (hasRun) return;
        hasRun = true;

        EditorApplication.delayCall += () =>
        {
            if (EditorSceneManager.GetActiveScene().name == "Untitled" || string.IsNullOrEmpty(EditorSceneManager.GetActiveScene().name))
            {
                CreateDefaultScene();
            }
        };
    }

    private static void CreateDefaultScene()
    {
        Debug.Log("🎮 AUTO-INITIALIZING SIMPSONS POCKET MONSTER GAME...");

        // Get or create the scene
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        scene.name = "MainScene";

        // Find or create main camera
        Camera camera = Object.FindObjectOfType<Camera>();
        if (camera == null)
        {
            GameObject cameraGO = new GameObject("Main Camera");
            camera = cameraGO.AddComponent<Camera>();
            cameraGO.tag = "MainCamera";
            cameraGO.transform.position = new Vector3(0, 0, -10);
        }

        camera.orthographic = true;
        camera.orthographicSize = 10;

        // Create Ground
        GameObject ground = new GameObject("Ground");
        ground.tag = "Ground";
        SpriteRenderer groundSprite = ground.AddComponent<SpriteRenderer>();
        groundSprite.color = new Color(0.2f, 0.6f, 0.2f);
        BoxCollider2D groundCollider = ground.AddComponent<BoxCollider2D>();
        groundCollider.size = new Vector2(50, 2);
        ground.transform.position = new Vector3(0, -10, 0);

        // Create Player
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        player.transform.position = new Vector3(0, 0, 0);

        SpriteRenderer playerSprite = player.AddComponent<SpriteRenderer>();
        playerSprite.color = new Color(1, 0.8f, 0);
        playerSprite.sortingOrder = 1;

        CircleCollider2D playerCollider = player.AddComponent<CircleCollider2D>();
        playerCollider.radius = 0.4f;

        Rigidbody2D playerRB = player.AddComponent<Rigidbody2D>();
        playerRB.gravityScale = 0;
        playerRB.freezeRotation = true;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;

        PlayerController playerController = player.AddComponent<PlayerController>();
        Animator playerAnimator = player.AddComponent<Animator>();

        // Create NPCs
        CreateNPC("Homer", new Vector3(-5, 0, 0), new Color(1, 0.5f, 0.5f), "Oh, donuts!");
        CreateNPC("Marge", new Vector3(5, 0, 0), new Color(0.5f, 0.5f, 1), "Mmm, nerds!");
        CreateNPC("Bart", new Vector3(0, 3, 0), new Color(1, 1, 0), "Eat my shorts!");

        // Create Wild Grass Area
        GameObject grassArea = new GameObject("WildGrassArea");
        grassArea.transform.position = new Vector3(10, 0, 0);
        
        SpriteRenderer grassSprite = grassArea.AddComponent<SpriteRenderer>();
        grassSprite.color = new Color(0.3f, 0.7f, 0.3f);

        BoxCollider2D grassCollider = grassArea.AddComponent<BoxCollider2D>();
        grassCollider.isTrigger = true;
        grassCollider.size = new Vector2(5, 5);

        EncounterTrigger encounterTrigger = grassArea.AddComponent<EncounterTrigger>();

        // Create GameManager
        GameObject gameManager = new GameObject("GameManager");
        gameManager.AddComponent<BattleSystem>();
        gameManager.AddComponent<CatchSystem>();

        // Create Inventory
        GameObject inventory = new GameObject("Inventory");
        inventory.AddComponent<Inventory>();

        // Create SaveSystem
        GameObject saveSystem = new GameObject("SaveSystem");
        saveSystem.AddComponent<SaveSystem>();

        // Create Canvas for UI
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler canvasScaler = canvasGO.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);

        GraphicRaycaster raycaster = canvasGO.AddComponent<GraphicRaycaster>();

        RectTransform canvasRect = canvasGO.GetComponent<RectTransform>();
        canvasRect.offsetMin = Vector2.zero;
        canvasRect.offsetMax = Vector2.zero;

        // Create Battle Panel
        GameObject battlePanel = new GameObject("BattlePanel");
        battlePanel.transform.SetParent(canvasGO.transform, false);
        Image battleImage = battlePanel.AddComponent<Image>();
        battleImage.color = new Color(0, 0, 0, 0.8f);

        RectTransform battleRect = battlePanel.GetComponent<RectTransform>();
        battleRect.offsetMin = Vector2.zero;
        battleRect.offsetMax = Vector2.zero;

        TextMeshProUGUI battleText = battlePanel.AddComponent<TextMeshProUGUI>();
        battleText.text = "Battle!";
        battleText.alignment = TextAlignmentOptions.Center;

        // Create UI Manager
        GameObject uiManager = new GameObject("UIManager");
        UIManager uiManagerScript = uiManager.AddComponent<UIManager>();

        // Create Dialogue Panel
        GameObject dialoguePanel = new GameObject("DialoguePanel");
        dialoguePanel.transform.SetParent(canvasGO.transform, false);
        Image dialogueImage = dialoguePanel.AddComponent<Image>();
        dialogueImage.color = new Color(0, 0, 0, 0.8f);

        RectTransform dialogueRect = dialoguePanel.GetComponent<RectTransform>();
        dialogueRect.anchoredPosition = new Vector2(0, -400);
        dialogueRect.sizeDelta = new Vector2(800, 200);

        TextMeshProUGUI dialogueText = dialoguePanel.AddComponent<TextMeshProUGUI>();
        dialogueText.text = "NPC Dialogue";
        dialogueText.alignment = TextAlignmentOptions.Center;

        // Create Inventory Panel
        GameObject inventoryPanel = new GameObject("InventoryPanel");
        inventoryPanel.transform.SetParent(canvasGO.transform, false);
        Image inventoryImage = inventoryPanel.AddComponent<Image>();
        inventoryImage.color = new Color(0, 0, 0, 0.8f);

        RectTransform inventoryRect = inventoryPanel.GetComponent<RectTransform>();
        inventoryRect.anchoredPosition = new Vector2(-400, 0);
        inventoryRect.sizeDelta = new Vector2(300, 600);

        TextMeshProUGUI inventoryText = inventoryPanel.AddComponent<TextMeshProUGUI>();
        inventoryText.text = "Creatures: 0\nPokeballs: 10";
        inventoryText.alignment = TextAlignmentOptions.TopLeft;

        // Create Event System
        if (Object.FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        // Save Scene
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/MainScene.unity");

        Debug.Log("✅ SCENE CREATED SUCCESSFULLY!");
        Debug.Log("📍 Location: Assets/Scenes/MainScene.unity");
        Debug.Log("🎮 Ready to play! Press Play button.");
    }

    private static void CreateNPC(string name, Vector3 position, Color color, string dialogue)
    {
        GameObject npc = new GameObject(name);
        npc.transform.position = position;

        SpriteRenderer npcSprite = npc.AddComponent<SpriteRenderer>();
        npcSprite.color = color;
        npcSprite.sortingOrder = 1;

        CircleCollider2D npcCollider = npc.AddComponent<CircleCollider2D>();
        npcCollider.isTrigger = true;
        npcCollider.radius = 0.5f;

        NPCController npcController = npc.AddComponent<NPCController>();
        npcController.SetDialogue(name, dialogue);
    }
}
