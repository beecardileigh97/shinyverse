using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {
    public Vector3 playerPosition;
    public List<string> caughtCreatures;
    public int pokeballCount;
}

public class SaveSystem : MonoBehaviour {
    public static SaveSystem Instance;
    string savePath;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        
        savePath = Application.persistentDataPath + "/savegame.json";
    }

    public void SaveGame(PlayerController player, Inventory inventory) {
        SaveData data = new SaveData {
            playerPosition = player.transform.position,
            caughtCreatures = new List<string>(inventory.caughtCreatureIDs),
            pokeballCount = inventory.pokeballCount
        };

        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(savePath, json);
        Debug.Log($"Game saved to {savePath}");
    }

    public SaveData LoadGame() {
        if (System.IO.File.Exists(savePath)) {
            string json = System.IO.File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded!");
            return data;
        } else {
            Debug.Log("No save file found.");
            return null;
        }
    }
}
