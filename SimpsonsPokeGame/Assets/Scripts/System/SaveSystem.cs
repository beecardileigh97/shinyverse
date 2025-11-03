using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public List<Creature> creatures = new List<Creature>();
    public int pokeballs;
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private const string SAVE_KEY = "GameSave";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            data.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        if (Inventory.Instance != null)
        {
            data.creatures = Inventory.Instance.GetCaughtCreatures();
            data.pokeballs = Inventory.Instance.GetPokeballCount();
        }

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
        Debug.Log("Game saved!");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = data.playerPosition;
            }

            Debug.Log("Game loaded!");
        }
    }
}
