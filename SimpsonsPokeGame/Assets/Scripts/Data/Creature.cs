using UnityEngine;

[System.Serializable]
public class Creature {
    public string id;
    public string displayName;
    public int maxHP;
    public int currentHP;
    public int attack;
    public int defense;
    public Sprite sprite;
    public int catchRate; // 1-255: higher = easier to catch

    public Creature Clone() {
        return new Creature {
            id = this.id,
            displayName = this.displayName,
            maxHP = this.maxHP,
            currentHP = this.maxHP,
            attack = this.attack,
            defense = this.defense,
            sprite = this.sprite,
            catchRate = this.catchRate
        };
    }
}
