using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnterSingleCombatTrigger : MonoBehaviour
{
    public int id;
    public GameObject parent; 
    public Player[] party = new Player[3];
    public Enemy[] enemies = new Enemy[3]; 
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
            party[0] = new Player( collision.GetComponent<IndividualEntityData>().health,null);
            enemies[0] = new Enemy(parentData.health, 1, (int)parentData.attack);
            party[0].sprite = playerSprite;
            enemies[0].sprite = (enemySprite);
            //TODO: find a way to set enemy health
            Debug.Log("Saving party data");
            pData.PlayerPosition = collision.transform.position; 
            Debug.Log("Entering combat!");
            eData.addEntity(id); 
            SceneManager.LoadScene("Combat");
        }
    }
}
