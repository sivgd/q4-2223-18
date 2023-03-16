using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
[CreateAssetMenu(fileName = "CombatData", menuName = "Persistant/CombatSceneData", order = 1)]
public class CombatSceneData : ScriptableObject
{
    [SerializeField] private Sprite[] goodGuySprites = new Sprite[3];
    [SerializeField] private Sprite[] badGuySprites = new Sprite[3];
    public Sprite placeHolderSprite; 

    [Header("Player 1 Data")]
    [SerializeField] private float playerAttack;
    [SerializeField] private int playerHealth; 

    [Header("Player 2 Stats")]
    [SerializeField] private float player2Attack;
    [SerializeField] private int player2Health;
    //[SerializeField] private bool isMage;
    // [SerializeField] private bool isHealer; 
    // [SerializeField] private Spell[] spells; 

    [Header("Player 3 Stats")]
    [SerializeField] private float player3Attack;
    [SerializeField] private int player3Health;

    [Header("Enemy Stats")]
    [SerializeField] private int enemy1Health;
    [SerializeField] private float enemy1Attack;
    [SerializeField] private int enemy2Health;
    [SerializeField] private float enemy2Attack;
    [SerializeField] private int enemy3Health;
    [SerializeField] private float enemy3Attack;

    [SerializeField] private int enemy1Level;
    [SerializeField] private int enemy2Level;
    [SerializeField] private int enemy3Level;
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

    public float PlayerAttack { get => playerAttack; set => playerAttack = value; }
    public float Player2Attack { get => player2Attack; set => player2Attack = value; }
    public float Player3Attack { get => player3Attack; set => player3Attack = value; }
    public int PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public int Player2Health { get => player2Health; set => player2Health = value; }
    public int Player3Health { get => player3Health; set => player3Health = value; }
    public int Enemy1Health { get => enemy1Health; set => enemy1Health = value; }
    public float Enemy1Attack { get => enemy1Attack; set => enemy1Attack = value; }
    public int Enemy2Health { get => enemy2Health; set => enemy2Health = value; }
    public float Enemy2Attack { get => enemy2Attack; set => enemy2Attack = value; }
    public int Enemy3Health { get => enemy3Health; set => enemy3Health = value; }
    public float Enemy3Attack { get => enemy3Attack; set => enemy3Attack = value; }
    public int Enemy1Level { get => enemy1Level; set => enemy1Level = value; }
    public int Enemy2Level { get => enemy2Level; set => enemy2Level = value; }
    public int Enemy3Level { get => enemy3Level; set => enemy3Level = value; }

    private void Awake()
    {
        if (!SceneManager.GetActiveScene().name.Contains("Combat"))
        {
            for(int i = 0; i < goodGuySprites.Length; i++)
            {
                goodGuySprites[i] = placeHolderSprite;
                badGuySprites[i] = placeHolderSprite; 
            }
            enemy1Attack = 0;
            enemy1Health = 0;
            enemy2Attack = 0;
            enemy2Health = 0;
            enemy3Attack = 0;
            enemy3Health = 0;
        }
    }
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
            if(goodGuySprites[i] == null || goodGuySprites[i].Equals(placeHolderSprite))
            {
                goodGuySprites[i] = sprite;
                return; 
            }
        }
    }
    public void addBadGuySprite(Sprite sprite)
    {
        for (int i = 0; i < badGuySprites.Length; i++)
        {
            if (badGuySprites[i].Equals(sprite))
            {
                return;
            }
        }

        for (int i = 0; i < badGuySprites.Length; i++)
        {
            if (badGuySprites[i] == null || badGuySprites[i].Equals(placeHolderSprite))
            {
                badGuySprites[i] = sprite;
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
