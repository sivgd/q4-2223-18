using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData",menuName = "Persistant/Player Data")]
public class Player : ScriptableObject
{

    [SerializeField] private int health;
    [SerializeField] private Attack[] attacks;
    public Sprite sprite;
    [SerializeField] private string name;
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
}
