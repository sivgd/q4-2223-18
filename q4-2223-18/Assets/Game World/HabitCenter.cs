using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitCenter : MonoBehaviour
{
   
    public Player[] playerParty;
    public DialougeManager dm;
    public int healthBoost;
    public int attackBoost;
    public int defenseBoost;
    public string[] dialouge = { "You look upon this default unity cube sprite.", "Suddenly you are reminded that you should", "get back to work", "(get to work dummy)", "your stats have increased!" };
    [SerializeField] private bool hasEntered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasEntered)
        {
            dm.nameBox.text = "";
            dm.gameObject.SetActive(true);
            dm.changeCurrentDialouge(dialouge,0.05f);
            foreach(Player p in playerParty)
            {
                p.maxHealth += healthBoost;
                p.BaseAttack += attackBoost;
                p.BaseDefense += defenseBoost; 
            }
            hasEntered = true; 
        }
    }
}
