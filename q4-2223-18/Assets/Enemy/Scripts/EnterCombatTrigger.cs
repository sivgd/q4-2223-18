using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnterCombatTrigger : MonoBehaviour
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
            Debug.Log("Getting combat sprites");
            Sprite playerSprite = collision.GetComponent<IndividualEntityData>().combatSprite;
            Debug.Log("adding combat sprites to combat data"); 
            cData.addGoodGuySprite(playerSprite);
            Debug.Log("Saving party data");
            pData.PlayerPosition = collision.transform.position; 
            Debug.Log("Entering combat!");
            eData.addEntity(id); 
            SceneManager.LoadScene("Combat");
        }
    }
}
