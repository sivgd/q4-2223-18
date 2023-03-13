using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PartyData", menuName = "PartyMechanics/PersistantPartyData", order = 1)]
public class PersistantPartyData : ScriptableObject
{
    
   [SerializeField] private int numMembers;
   [SerializeField] private float playerMoveSpeed;
   [SerializeField] private int playerAttack;
   [SerializeField] private int numInstance;

    public int NumMembers { get => numMembers; set => numMembers = value; }
    public float PlayerMoveSpeed { get => playerMoveSpeed; set => playerMoveSpeed = value; }
    public int PlayerAttack { get => playerAttack; set => playerAttack = value; }
    public int NumInstance { get => numInstance; set => numInstance = value; }

   
    public void addNumMembers(int num)
    {
        numMembers += num;
    }


}
