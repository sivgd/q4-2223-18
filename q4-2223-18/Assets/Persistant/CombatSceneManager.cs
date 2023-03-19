using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class CombatSceneManager : MonoBehaviour
{
    [Header("Scene References")]
    public SpriteRenderer[] goodGuyRenderers;
    public SpriteRenderer[] badGuyRenderers;

    public Player[] party = new Player[3];
    public Enemy[] enemies = new Enemy[3]; 
    private Combat combatObject; 

    //[Header("External References")]
    private Sprite[] goodGuySprites;
    private Sprite[] badGuySprites; 

    // Start is called before the first frame update
    void Start()
    {
        combatObject = GetComponent<Combat>();
        for (int i = 0; i < party.Length; i++) goodGuySprites[i] = party[i].sprite;
        for (int i = 0; i < enemies.Length; i++) badGuySprites[i] = enemies[i].sprite;
        for(int i = 0; i < goodGuySprites.Length; i++) goodGuyRenderers[i].sprite = goodGuySprites[i];
        for (int i = 0; i < badGuySprites.Length; i++) badGuyRenderers[i].sprite = badGuySprites[i]; 
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            combatObject.exitCombat();
            SceneManager.LoadScene("TestScene");
        }
    }
    

}
