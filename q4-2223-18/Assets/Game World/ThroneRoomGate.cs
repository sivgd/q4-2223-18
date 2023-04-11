using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneRoomGate : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject openSprite;
    public GameObject closedSprite; 

    private void Update()
    {
        if (isOpen && !openSprite.activeInHierarchy)
        {
            openSprite.SetActive(true);
            closedSprite.SetActive(false);
            Debug.Log("Door Open"); 
        }
        else if(!isOpen && !closedSprite.activeInHierarchy)
        {
            closedSprite.SetActive(true);
            openSprite.SetActive(false);
            Debug.Log("Door Closed");
        }
    }
}
