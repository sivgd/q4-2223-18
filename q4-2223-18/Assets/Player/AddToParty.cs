using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToParty : MonoBehaviour
{
    [Header("Combat Stats")]
    public int maxHealth;
    public int baseDefense;
    public int baseAttack;
    public Attack[] attacks;
    [Header("Unique identifiers")]
    public Sprite sprite;
    public Habit habit;
    public string name;
    [Header("External References")]
    public Player[] party; 

    private void OnDestroy()
    {
       foreach(Player player in party)
        {
            if(player.Name == null || player.Name == "")
            {
                player.maxHealth = maxHealth;
                player.BaseDefense = baseDefense;
                player.Attacks = attacks;
                player.topDownSprite = sprite;
                player.combatSprite = sprite;
                player.habit = habit;
                player.Name = name;
                Debug.Log($"Say hello to {name}"); 
                return; 
            }
            else if(player.Name == name)
            {
                return; 
            }
        } 
    }
}
