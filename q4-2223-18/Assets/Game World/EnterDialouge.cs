using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class EnterDialouge : MonoBehaviour
{
    private IndividualEntityData entityData; 
    public GameObject dialougeUI;
    public DialougeManager dm; 
    public TMP_Text dialougeBox;
    private void Start()
    {
        entityData = GetComponentInParent<IndividualEntityData>(); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialougeUI.SetActive(true);
            dm.nameBox.text = entityData.name; 
            dm.changeCurrentDialouge(0);
        }
    }
}
