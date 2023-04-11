using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class KeyPrompt : MonoBehaviour
{
    [Header("Scalene Citadel References")]
    [SerializeField] private GameObject closedDoor;
    [SerializeField] private GameObject openDoor;
    [Header("General References")]
    [SerializeField] private GameObject keyGiver;
    public DialougeManager dm;
    [TextArea]
    public string[] errorDialouge = new string[] { "As you look at the door, you see a comically large golden padlock.", "Without the proper key, you won't be able to get in.", "You remember seeing a key like that down south at Elder Euclid's farm." };
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (keyGiver == null)
            {
                if(!SceneManager.GetActiveScene().name.Equals("Scalene Citadel"))
                {
                    SceneManager.LoadScene("Scalene Citadel");
                }
                else
                {
                    gameObject.GetComponentInParent<ThroneRoomGate>().isOpen = true; 
                }
            }
            else
            {
                dm.nameBox.text = "";
                dm.gameObject.SetActive(true); 
                dm.changeCurrentDialouge(errorDialouge,0.03f,true);
            }
        }
    }
}
