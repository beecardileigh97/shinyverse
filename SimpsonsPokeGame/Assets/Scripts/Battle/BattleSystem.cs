using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour {
    public static BattleSystem Instance;
    public GameObject battleUIPanel; // assign Battle Canvas here
    public Text infoText;
    public Image creatureImage;
    public Button fightButton;
    public Button catchButton;
    public Button runButton;
    
    Creature activeWild;
    bool battleActive = false;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        
        if (battleUIPanel != null) {
            battleUIPanel.SetActive(false);
        }
    }

    void Start() {
        if (fightButton != null) fightButton.onClick.AddListener(OnFightClicked);
        if (catchButton != null) catchButton.onClick.AddListener(OnCatchClicked);
        if (runButton != null) runButton.onClick.AddListener(OnRunClicked);
    }

    public void StartWildBattle(string creatureID) {
        if (battleActive) return;
        
        activeWild = CreatureDatabase.Get(creatureID);
        if (activeWild == null) {
            Debug.LogError($"Creature {creatureID} not found in database!");
            return;
        }
        
        StartCoroutine(RunWildBattle());
    }

    IEnumerator RunWildBattle() {
        battleActive = true;
        if (battleUIPanel != null) battleUIPanel.SetActive(true);
        
        if (infoText != null) {
            infoText.text = $"A wild {activeWild.displayName} appeared!";
        }
        if (creatureImage != null && activeWild.sprite != null) {
            creatureImage.sprite = activeWild.sprite;
        }
        
        yield return new WaitForSeconds(1.5f);
        
        // Player can choose to fight, catch, or run
        if (infoText != null) {
            infoText.text = "Choose an action!";
        }
    }

    void OnFightClicked() {
        if (!battleActive) return;
        StartCoroutine(SimulateFight());
    }

    IEnumerator SimulateFight() {
        // Simple damage
        int damage = Random.Range(5, 15);
        activeWild.currentHP -= damage;
        
        if (infoText != null) {
            infoText.text = $"Attacked {activeWild.displayName} for {damage} damage!";
        }
        
        yield return new WaitForSeconds(1f);
        
        if (activeWild.currentHP <= 0) {
            if (infoText != null) {
                infoText.text = $"{activeWild.displayName} fainted!";
            }
            yield return new WaitForSeconds(1f);
            EndBattle();
        } else {
            if (infoText != null) {
                infoText.text = $"{activeWild.displayName} attacked you!";
            }
            yield return new WaitForSeconds(1f);
            if (infoText != null) {
                infoText.text = "Choose an action!";
            }
        }
    }

    void OnCatchClicked() {
        if (!battleActive) return;
        StartCoroutine(CatchSystem.Instance.TryCatch(activeWild, () => EndBattle()));
    }

    void OnRunClicked() {
        if (!battleActive) return;
        if (infoText != null) {
            infoText.text = "Escaped safely!";
        }
        StartCoroutine(CloseAfter(1f));
    }

    void EndBattle() {
        battleActive = false;
        StartCoroutine(CloseAfter(1f));
    }

    IEnumerator CloseAfter(float t) {
        yield return new WaitForSeconds(t);
        if (battleUIPanel != null) battleUIPanel.SetActive(false);
    }
}
