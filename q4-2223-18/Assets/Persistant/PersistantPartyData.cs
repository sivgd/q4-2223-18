using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PartyData", menuName = "Persistant/PersistantPartyData", order = 1)]
public class PersistantPartyData : ScriptableObject
{
    //for the future, when creating party members, have seperate template prefabs for each party member, then load data from this script to modify said templates 
    // [SerializeField] private int numMembers;
    public bool isCombatTutorial;
    public bool loadPreviousPos;
    public bool defeatedTaxMan = false;
    [SerializeField] private string previousSceneName; 
    [Header("Player Data")]
    [SerializeField] private float playerMoveSpeed;
    //[SerializeField] private Transform playerTransform;
    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private Quaternion playerRotation; 

    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
    private void OnValidate() => playerPosition = Vector2.zero; 
    // public int NumMembers { get => numMembers; set => numMembers = value; }
    public float PlayerMoveSpeed { get => playerMoveSpeed; set => playerMoveSpeed = value; }
    public Vector2 PlayerPosition { get => playerPosition; set => playerPosition = value; }
    public Quaternion PlayerRotation { get => playerRotation; set => playerRotation = value; }
    public string PreviousSceneName { get => previousSceneName; set => previousSceneName = value; }

    //public Transform PlayerTransform { get => playerTransform; set => playerTransform = value; }

    /* private void OnValidate()
     {
         playerPosition = Vector2.zero; 
     }*/
  /*  public void addNumMembers(int num)
    {
        numMembers += num;
    }*/


}
