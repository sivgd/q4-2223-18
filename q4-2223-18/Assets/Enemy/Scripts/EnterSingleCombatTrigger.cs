using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnterSingleCombatTrigger : MonoBehaviour
{
    public int id;
    public GameObject parent; 
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
            TrackPartyData partyData = collision.GetComponent<TrackPartyData>(); 
            Debug.Log("Getting combat sprites");
            Sprite enemySprite = parentData.combatSprite; 
            Debug.Log("adding combat sprites to combat data"); 
            enemies[0].sprite = (enemySprite);
            Debug.Log("Saving party data");
            partyData.savePartyData(); 
            Debug.Log("Entering combat!");
            eData.addEntity(id); 
            SceneManager.LoadScene("Combat");
        }
    }
}
