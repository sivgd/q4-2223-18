using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class EnterDialouge : MonoBehaviour
{
    public bool finishedDialouge = false;
    public GameObject dialougeUI;
    public TMP_Text dialougeBox;
    public string[] dialouge; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialougeUI.SetActive(true); 
        }
    }
}
