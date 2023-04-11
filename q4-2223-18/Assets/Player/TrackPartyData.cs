using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class TrackPartyData : MonoBehaviour
{
    [Header("S.O. References")]
    public Player[] party = new Player[3];
    public PersistantPartyData partyData;

    [Header("GameWorld References")]
    [SerializeField] private string sceneName; 
    [SerializeField] private Vector2 worldPos;
    [SerializeField] private Quaternion wordRot;

    [Header("Party References")]
    public Sprite[] topDownSprites;
    public Sprite[] combatSprites; 
    private void Start()
    {
        if (!partyData.PreviousSceneName.Equals(SceneManager.GetActiveScene().name))
        {
            partyData.PlayerPosition = Vector2.zero;
            partyData.PreviousSceneName = SceneManager.GetActiveScene().name;
        }
        else
        {
            partyData.PreviousSceneName = SceneManager.GetActiveScene().name;
            transform.position = partyData.PlayerPosition;
            transform.rotation = partyData.PlayerRotation;
        }
        /*for(int i = 0; i < party.Length; i++)
        {
           // topDownSprites[i] = party[i].topDownSprite;
           // combatSprites[i] = party[i].combatSprite; 
        }*/
    }
    public void savePartyData()
    {
        Debug.Log("Saving party Data"); 
        for (int i = 0; i < party.Length; i++)
        {
            party[i].topDownSprite = topDownSprites[i];
            Debug.Log("saving top down sprites"); 
            party[i].combatSprite = combatSprites[i];
            Debug.Log("Saving combat Sprites"); 
        }
        worldPos = transform.position;
        wordRot = transform.rotation;
        partyData.PlayerPosition = worldPos;
        partyData.PlayerRotation = wordRot;
    }



}
