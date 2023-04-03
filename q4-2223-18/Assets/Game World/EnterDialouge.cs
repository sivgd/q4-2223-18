using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class EnterDialouge : MonoBehaviour
{
    public string[] dialouge = { "Heloo!", "this is a test.", "this dialouge was stored outside of the Dialouge Manager >:D" }; 
    public GameObject dialougeUI;
    public DialougeManager dm;
    public string name; 
  //  public TMP_Text dialougeBox;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialougeUI.SetActive(true);
            dm.nameBox.text = name; 
            dm.changeCurrentDialouge(dialouge,0.05f,true);
            Destroy(gameObject); 
        }
    }
}
