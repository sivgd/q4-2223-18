using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private AttackType attackType;
    [SerializeField] private int damage;
    [SerializeField] private string name;
    [SerializeField] private int cooldown; 

    public Attack(AttackType attackType, int damage, string name, int cooldown)
    {
        this.attackType = attackType;
        this.damage = damage;
        this.name = name;
        this.cooldown = cooldown; 
    }

    public AttackType AttackType { get => attackType; set => attackType = value; }
    public int Damage { get => damage; set => damage = value; }
    public string Name { get => name; set => name = value; }
    public int Cooldown { get => cooldown; set => cooldown = value; }
}
public enum AttackType
{
    Physical,
    Magic
}
