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
    private void Start()
    {
        if (eData.hasEntity(id))
        {
            Destroy(gameObject);
        }
    }

}
