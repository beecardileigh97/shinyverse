using UnityEngine;

public interface INPCInteractable {
    void Interact();
}

public class NPCController : MonoBehaviour, INPCInteractable {
    public string npcName = "NPC";
    public string dialogue = "Hello, traveler!";
    public UIManager uiManager;

    void Start() {
        if (uiManager == null) {
            uiManager = FindObjectOfType<UIManager>();
        }
    }

    public void Interact() {
        Debug.Log($"{npcName}: {dialogue}");
        if (uiManager != null) {
            uiManager.ShowDialogue(npcName, dialogue);
        }
    }
}
