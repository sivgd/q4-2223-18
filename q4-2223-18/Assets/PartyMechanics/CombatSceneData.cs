using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CombatData", menuName = "PartyMechanics/CombatSceneData", order = 1)]
public class CombatSceneData : ScriptableObject
{
    [SerializeField] private Sprite[] goodGuySprites = new Sprite[3];
    [SerializeField] private Sprite[] badGuySprites = new Sprite[3]; 


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
