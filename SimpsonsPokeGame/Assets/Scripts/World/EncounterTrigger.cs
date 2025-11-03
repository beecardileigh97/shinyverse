using UnityEngine;

public class EncounterTrigger : MonoBehaviour {
    public string[] possibleCreatureIDs;
    public float encounterChance = 0.12f; // 12% per trigger

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        TryEncounter();
    }

    void TryEncounter() {
        if (Random.value <= encounterChance && possibleCreatureIDs.Length > 0) {
            string id = possibleCreatureIDs[Random.Range(0, possibleCreatureIDs.Length)];
            if (BattleSystem.Instance != null) {
                BattleSystem.Instance.StartWildBattle(id);
            }
        }
    }
}
