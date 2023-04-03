using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPrompt : MonoBehaviour
{
    public bool hasKey = false;
    public DialougeManager dm; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (hasKey)
            {
                //progress to next level
            }
            else
            {
                dm.nameBox.text = "";
                dm.gameObject.SetActive(true); 
                dm.changeCurrentDialouge(new string[] {"As you look at the door, you see a comically large golden padlock.","Without the proper key, you won't be able to get in.","You remember seeing a key like that down south at Elder Euclid's farm."},0.03f,false);
            }
        }
    }
}
