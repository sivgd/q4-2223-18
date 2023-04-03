using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlibberFlabberFight : MonoBehaviour
{
    [Header("Persistant Entity")]
    public int id; 
    [Header("Enemy Attributes")]
    public IndividualEntityData[] enemies = new IndividualEntityData[3];
    [Header("External References")]
    public GameObject parent;
    public EnterCombatManager ecm;
    public PersistantEntityData eData;
    public EnterDialouge ed;

    private void Start()
    {
        if (eData.hasEntity(id))
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            eData.addEntity(id); 
            ecm.enterCombat(enemies,false);
        }
    }
    private void OnDestroy()
    {
        ed.currentDialouge = 2;
        Destroy(parent);
    }
}
