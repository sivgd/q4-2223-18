using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CombatData", menuName = "Persistant/CombatSceneData", order = 1)]
public class CombatSceneData : ScriptableObject
{
    [SerializeField] private Sprite[] goodGuySprites = new Sprite[3];
    [SerializeField] private Sprite[] badGuySprites = new Sprite[3];

    [Header("Player 1 Data")]
    [SerializeField] private float playerAttack;

    [Header("Player 2 Stats")]
    [SerializeField] private float player2Attack;
    //[SerializeField] private bool isMage;
    // [SerializeField] private bool isHealer; 
    // [SerializeField] private Spell[] spells; 

    [Header("Player 3 Stats")]
    [SerializeField] private float player3Attack;


    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

    public float PlayerAttack { get => playerAttack; set => playerAttack = value; }
    public float Player2Attack { get => player2Attack; set => player2Attack = value; }
    public float Player3Attack { get => player3Attack; set => player3Attack = value; }

    public void addGoodGuySprite(Sprite sprite)
    {
        for(int i = 0; i < goodGuySprites.Length; i++)
        {
            if (goodGuySprites[i].Equals(sprite))
            {
                return; 
            }
        }

        for(int i = 0; i < goodGuySprites.Length; i++)
        {
            if(goodGuySprites[i] == null)
            {
                goodGuySprites[i] = sprite;
                return; 
            }
        }
    }
    public Sprite[] getGoodGuySprites()
    {
        return goodGuySprites; 
    }
    public Sprite[] getBadGuySprites()
    {
        return badGuySprites; 
    }

}
