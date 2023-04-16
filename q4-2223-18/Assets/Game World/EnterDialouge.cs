using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class EnterDialouge : MonoBehaviour
{
    [TextArea]
    public string[] dialouge = { "Heloo!", "this is a test.", "this dialouge was stored outside of the Dialouge Manager >:D" };
    [TextArea]
    public string[] secondDialouge; 
    public GameObject dialougeUI;
    [SerializeField] private bool hasSecondaryDialouge = false;
    [SerializeField] private bool hasAdditionalDestroyables = true; 
    public GameObject[] additionalDestroyables;
   // public bool[] additionalBooleans; 
    public int currentDialouge = 1; 
    public DialougeManager dm;
    public TalkerPersonality tPersonality; 
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
                    dm.changeCurrentDialouge(dialouge, 0.03f, true,tPersonality);
                    if (hasSecondaryDialouge == false) StartCoroutine(destoryGameObjects());
                    //currentDialouge++;
                    break;
                case 2:
                    dialougeUI.SetActive(true);
                    dm.nameBox.text = "";
                    dm.changeCurrentDialouge(secondDialouge, 0.03f, true,tPersonality);
                    StartCoroutine(destoryGameObjects()); 
                    break; 
            }
        }
    }
    private IEnumerator destoryGameObjects()
    {
        yield return new WaitUntil(() => (dm.CurrentDialougeString >= secondDialouge.Length - 1 || !dialougeUI.activeInHierarchy));
        if (hasAdditionalDestroyables)
        {
            for (int i = 0; i < additionalDestroyables.Length; i++)
            {
                if (additionalDestroyables[i].GetComponent<AddToParty>())
                {
                    additionalDestroyables[i].GetComponent<AddToParty>().addToParty = true;
                }
                Destroy(additionalDestroyables[i]);
            }
        }
        Destroy(gameObject);
    }
}
