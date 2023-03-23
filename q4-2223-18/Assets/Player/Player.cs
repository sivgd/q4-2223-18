using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData",menuName = "Persistant/Player Data")]
public class Player : ScriptableObject
{

    [SerializeField] private int health;
    [SerializeField] private Attack[] attacks;
    [SerializeField] private List<Item> items; 
    [SerializeField] private int baseAttack = 1; 
    public Sprite combatSprite;
    public Sprite topDownSprite; 
    [SerializeField] private int baseDefense;
    [SerializeField] private int tempDefenseBoost; 
    [SerializeField] private string name;
    [SerializeField] private int tempAttackBoost; 
    [SerializeField] private bool hasGoneDuringTurn; 
    public Player(int health, Attack[] attacks)
    {
        this.health = health;
        this.attacks = attacks; 
    }
    
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
    public int Health { get => health; set => health = value; }
    public Attack[] Attacks { get => attacks; set => attacks = value; }
    public string Name { get => name; set => name = value; }
    public bool HasGoneDuringTurn { get => hasGoneDuringTurn; set => hasGoneDuringTurn = value; }
    public int BaseAttack { get => baseAttack; set => baseAttack = value; }
    public List<Item> Items { get => items; set => items = value; }
    public int TempAttackBoost { get => tempAttackBoost; set => tempAttackBoost = value; }
    public int BaseDefense { get => baseDefense; set => baseDefense = value; }
    public int TempDefenseBoost { get => tempDefenseBoost; set => tempDefenseBoost = value; }
}
