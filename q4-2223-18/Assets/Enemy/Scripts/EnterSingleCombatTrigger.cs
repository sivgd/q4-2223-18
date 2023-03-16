using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnterSingleCombatTrigger : MonoBehaviour
{
    public int id;
    public GameObject parent; 
    public CombatSceneData cData;
    public PersistantPartyData pData;
    public PersistantEntityData eData;
   
    private void Start()
    {
        if (eData.hasEntity(id)) 
        {
            Destroy(parent); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IndividualEntityData parentData = parent.GetComponent<IndividualEntityData>(); 
            Debug.Log("Getting combat sprites");
            Sprite playerSprite = collision.GetComponent<IndividualEntityData>().combatSprite;
            Sprite enemySprite = parentData.combatSprite; 
            Debug.Log("adding combat sprites to combat data"); 
            cData.addGoodGuySprite(playerSprite);
            cData.addBadGuySprite(enemySprite);
            cData.PlayerHealth = collision.GetComponent<IndividualEntityData>().health; 
            cData.Enemy1Health = parentData.health;
            cData.Enemy1Attack = parentData.attack; 
            //TODO: find a way to set enemy health
            Debug.Log("Saving party data");
            pData.PlayerPosition = collision.transform.position; 
            Debug.Log("Entering combat!");
            eData.addEntity(id); 
            SceneManager.LoadScene("Combat");
        }
    }
}
