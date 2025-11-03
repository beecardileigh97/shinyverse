using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CatchSystem : MonoBehaviour {
    public static CatchSystem Instance;
    public Text infoText;
    public ParticleSystem catchEffectPrefab; // optional visual effect

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public IEnumerator TryCatch(Creature wild, Action onComplete = null) {
        Inventory inv = Inventory.Instance;
        
        if (inv == null || !inv.HasPokeballs()) {
            if (infoText != null) {
                infoText.text = "No Pokeballs left!";
            }
            yield return new WaitForSeconds(1f);
            onComplete?.Invoke();
            yield break;
        }

        inv.UsePokebal();
        
        if (infoText != null) {
            infoText.text = "You threw a Pokeball!";
        }
        
        yield return new WaitForSeconds(1f);

        // Catch calculation: simple formula
        // higher catchRate and lower HP = higher catch chance
        float hpFactor = (float)wild.currentHP / wild.maxHP; // 0-1
        float baseCatchChance = wild.catchRate / 255f;
        float finalChance = baseCatchChance * (1f - hpFactor * 0.5f);
        finalChance = Mathf.Clamp01(finalChance);

        bool caught = Random.value <= finalChance;

        if (caught) {
            inv.AddCaught(wild.id);
            if (infoText != null) {
                infoText.text = $"Caught {wild.displayName}!";
            }
        } else {
            if (infoText != null) {
                infoText.text = $"{wild.displayName} broke free!";
            }
        }

        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
    }
}
