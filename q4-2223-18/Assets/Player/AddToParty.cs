using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToParty : MonoBehaviour
{
    [Header("Public Logic")]
    public bool addToParty = false; 
    [Header("Combat Stats")]
    public int maxHealth;
    public int baseDefense;
    public int baseAttack;
    public Attack[] attacks;
    public TrackPartyData tPartyData; 
    [Header("Unique identifiers")]
    public Sprite sprite;
    public Habit habit;
    public string name;
    [Header("External References")]
    public Player[] party; 

    private void OnDestroy()
    {
        if (addToParty)
        {
            foreach (Player player in party)
            {
                if (player.Name == null || player.Name == "")
                {
                    player.maxHealth = maxHealth;
                    player.Health = maxHealth; 
                    player.BaseDefense = baseDefense;
                    player.BaseAttack = baseAttack; 
                    player.Attacks = attacks;
                    player.topDownSprite = sprite;
                    player.combatSprite = sprite;
                    player.habit = habit;
                    player.Name = name;
                    Debug.Log($"Say hello to {name}");
                    tPartyData.loadPreviousData(); 
                    return; 
                }
                else if (player.Name == name)
                {
                    return;
                }
               
                
            }
        }
    }
}
