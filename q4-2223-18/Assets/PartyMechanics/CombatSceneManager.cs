using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSceneManager : MonoBehaviour
{
    [Header("Scene References")]
    public SpriteRenderer[] goodGuyRenderers;
    public SpriteRenderer[] badGuyRenderers;
    public CombatSceneData combatSceneData; 

    //[Header("External References")]
    private Sprite[] goodGuySprites;
    private Sprite[] badGuySprites; 

    // Start is called before the first frame update
    void Start()
    {
        goodGuySprites = combatSceneData.getGoodGuySprites();
        badGuySprites = combatSceneData.getBadGuySprites(); 
        for(int i = 0; i < goodGuySprites.Length; i++) goodGuyRenderers[i].sprite = goodGuySprites[i];
        for (int i = 0; i < badGuySprites.Length; i++) badGuyRenderers[i].sprite = badGuySprites[i]; 

    }

    
}