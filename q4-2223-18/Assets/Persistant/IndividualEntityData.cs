using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualEntityData : MonoBehaviour
{
    public Sprite combatSprite;
    public PersistantEntityData eData; 
    public string name;
    public int id = -1;
    public int health;
    public float attack;
    public int level;

    public IndividualEntityData(Sprite combatSprite, PersistantEntityData eData, string name, int id, int health, float attack, int level)
    {
        this.combatSprite = combatSprite;
        this.eData = eData;
        this.name = name;
        this.id = id;
        this.health = health;
        this.attack = attack;
        this.level = level;
    }

    private void Start()
    {
        if (eData.hasEntity(id))
        {
            Destroy(gameObject);
        }
    }
  

}
