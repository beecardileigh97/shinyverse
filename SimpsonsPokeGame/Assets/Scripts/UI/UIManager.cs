using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject dialoguePanel;
    public Text dialogueNameText;
    public Text dialogueContentText;
    public Button continueButton;
    public GameObject inventoryPanel;
    public Text inventoryText;

    void Start() {
        if (dialoguePanel != null) {
            dialoguePanel.SetActive(false);
        }
        if (inventoryPanel != null) {
            inventoryPanel.SetActive(false);
        }

        if (continueButton != null) {
            continueButton.onClick.AddListener(CloseDialogue);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            ToggleInventory();
        }
    }

    public void ShowDialogue(string npcName, string content) {
        if (dialoguePanel != null) {
            dialoguePanel.SetActive(true);
            if (dialogueNameText != null) dialogueNameText.text = npcName;
            if (dialogueContentText != null) dialogueContentText.text = content;
        }
    }

    void CloseDialogue() {
        if (dialoguePanel != null) {
            dialoguePanel.SetActive(false);
        }
    }

    void ToggleInventory() {
        if (inventoryPanel == null) return;

        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        
        if (inventoryPanel.activeSelf) {
            UpdateInventoryDisplay();
        }
    }

    void UpdateInventoryDisplay() {
        Inventory inv = Inventory.Instance;
        if (inv == null) return;

        string text = "=== INVENTORY ===\n";
        text += $"Pokeballs: {inv.pokeballCount}\n\n";
        text += "Caught Creatures:\n";
        foreach (string id in inv.caughtCreatureIDs) {
            text += $"- {id}\n";
        }

        if (inventoryText != null) {
            inventoryText.text = text;
        }
    }
}
