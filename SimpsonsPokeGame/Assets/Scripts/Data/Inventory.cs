using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory Instance;
    public List<string> caughtCreatureIDs = new List<string>();
    public int pokeballCount = 20;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void AddCaught(string id) {
        if (!caughtCreatureIDs.Contains(id)) {
            caughtCreatureIDs.Add(id);
            Debug.Log($"Added {id} to inventory. Total caught: {caughtCreatureIDs.Count}");
        }
    }

    public void UsePokebal() {
        if (pokeballCount > 0) {
            pokeballCount--;
        }
    }

    public bool HasPokeballs() {
        return pokeballCount > 0;
    }
}
