using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData",menuName = "Persistant/Player Data")]
public class Player : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private Attack[] attacks;
    public Sprite sprite; 
    public Player(int health, Attack[] attacks)
    {
        this.health = health;
        this.attacks = attacks; 
    }

    public int Health { get => health; set => health = value; }
    public Attack[] Attacks { get => attacks; set => attacks = value; }
}
public class Attack
{
    private AttackType attackType;
    private int damage;
    private string name;

    public Attack(AttackType attackType, int damage, string name)
    {
        this.attackType = attackType;
        this.damage = damage;
        this.name = name;
    }

    public AttackType AttackType { get => attackType; set => attackType = value; }
    public int Damage { get => damage; set => damage = value; }
    public string Name { get => name; set => name = value; }
}
public enum AttackType
{
    Physical,
    Magic
}
