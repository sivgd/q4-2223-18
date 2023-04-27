using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitCenter : MonoBehaviour
{
   
    public Player[] playerParty;
    public DialougeManager dm;
    public PersistantEntityData eData; 
    public int healthBoost;
    public int attackBoost;
    public int defenseBoost;
    [SerializeField] private int id; 
    [TextArea]
    public string[] dialouge = { "You look upon this default unity cube sprite.", "Suddenly you are reminded that you should", "get back to work", "(get to work dummy)", "your stats have increased!"};
    [SerializeField] private bool hasEntered = false;
    [SerializeField] private bool changeSprite;
    [SerializeField] private Sprite newSprite;
    private void Start()
    {
        if (eData.hasEntity(id))
        {
            hasEntered = true; 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasEntered)
        {
            dm.nameBox.text = "";
            dm.gameObject.SetActive(true);
            dm.changeCurrentDialouge(dialouge, 0.03f, true, TalkerPersonality.narrator);
             for(int i = 0; i < playerParty.Length; i++)
             {
                playerParty[i].maxHealth += healthBoost;
                playerParty[i].BaseAttack += attackBoost;
                playerParty[i].BaseDefense += defenseBoost; 
             }
            hasEntered = true;
            if (changeSprite)
            {
                GetComponent<SpriteRenderer>().sprite = newSprite; 
            }
            eData.addEntity(id); 
        }
    }
}
