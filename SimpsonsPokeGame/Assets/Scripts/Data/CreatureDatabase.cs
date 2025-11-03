using System.Collections.Generic;
using UnityEngine;

public static class CreatureDatabase {
    static Dictionary<string, Creature> db = new Dictionary<string, Creature>();

    static CreatureDatabase() {
        // Add sample creatures (Simpsons-themed)
        Creature c1 = new Creature {
            id = "springbolt",
            displayName = "Springbolt",
            maxHP = 30,
            attack = 5,
            defense = 4,
            catchRate = 120
        };
        db.Add(c1.id, c1);

        Creature c2 = new Creature {
            id = "donuragon",
            displayName = "Donuragon",
            maxHP = 45,
            attack = 8,
            defense = 6,
            catchRate = 80
        };
        db.Add(c2.id, c2);

        Creature c3 = new Creature {
            id = "beerling",
            displayName = "Beerling",
            maxHP = 25,
            attack = 4,
            defense = 3,
            catchRate = 150
        };
        db.Add(c3.id, c3);

        Creature c4 = new Creature {
            id = "saxasaurus",
            displayName = "Saxasaurus",
            maxHP = 40,
            attack = 7,
            defense = 5,
            catchRate = 90
        };
        db.Add(c4.id, c4);
    }

    public static Creature Get(string id) {
        if (db.ContainsKey(id)) {
            return db[id].Clone();
        }
        return null;
    }

    public static List<string> GetAllIDs() {
        return new List<string>(db.Keys);
    }
}
