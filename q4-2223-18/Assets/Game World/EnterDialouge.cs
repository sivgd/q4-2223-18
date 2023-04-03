using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class EnterDialouge : MonoBehaviour
{
    public string[] dialouge = { "Heloo!", "this is a test.", "this dialouge was stored outside of the Dialouge Manager >:D" };
    public string[] secondDialouge; 
    public GameObject dialougeUI;
    [SerializeField] private bool hasSecondaryDialouge = false;
    public GameObject[] additionalDestroyables; 
    public int currentDialouge = 1; 
    public DialougeManager dm;
    public string name; 
  //  public TMP_Text dialougeBox;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (currentDialouge)
            {
                case 1:
                    dialougeUI.SetActive(true);
                    dm.nameBox.text = name;
                    dm.changeCurrentDialouge(dialouge, 0.03f, true);
                    //currentDialouge++;
                    break;
                case 2:
                    dialougeUI.SetActive(true);
                    dm.nameBox.text = "";
                    dm.changeCurrentDialouge(secondDialouge, 0.03f, true);
                    foreach(GameObject dest in additionalDestroyables) Destroy(dest);
                    Destroy(gameObject);
                    break; 
            }
        }
    }
}
