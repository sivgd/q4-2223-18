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

}
