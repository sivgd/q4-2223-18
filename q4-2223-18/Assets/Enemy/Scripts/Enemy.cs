using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemyData", menuName = "Persistant/Enemy Data")]
public class Enemy : ScriptableObject
{

    [SerializeField] private int health;
    [SerializeField] private Attack[] attacks;
    [SerializeField] private int baseAttack; 
    [SerializeField] private int level;
    [SerializeField] private string name; 
    public Sprite sprite;
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
    public Enemy(int health, int level ,int baseAttack)
    {
        this.health = health;
        this.baseAttack = baseAttack; 
        this.level = level; 
    }

    public int Health { get => health; set => health = value; }
    public Attack[] Attacks { get => attacks; set => attacks = value; }
    public int Level { get => level; set => level = value; }
    public int BaseAttack { get => baseAttack; set => baseAttack = value; }
    public string Name { get => name; set => name = value; }
}
