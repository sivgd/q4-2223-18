using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class CombatSceneManager : MonoBehaviour
{
    [Header("Scene References")]
    public SpriteRenderer[] goodGuyRenderers;
    public SpriteRenderer[] badGuyRenderers;
    public SFXManager sfxManager; 

    public Player[] party = new Player[3];
    public Enemy[] enemies = new Enemy[3]; 
    private Combat combatObject;
    [Header("UI Stuff")]
    public TMP_Text tutorialText;
    public GameObject tutorialParent; 
    [Header("UI Debug")]
    [SerializeField] private int textNum = 0;
    [SerializeField] private bool isWriting = false;
    [SerializeField] private bool isWritingFinished = false;
    [SerializeField] private bool hasSelectedPlayer = false;
    [SerializeField] private bool hasSelectedAttack = false;
    [SerializeField] private bool hasSelectedEnemy = false; 
    [Header("Outside References")]
    public PersistantPartyData partyData; 
    //[Header("External References")]
    private Sprite[] goodGuySprites;
    private Sprite[] badGuySprites; 

    // Start is called before the first frame update
    void Start()
    {
        

        goodGuySprites = new Sprite[party.Length];
        badGuySprites = new Sprite[enemies.Length];      
        combatObject = GetComponent<Combat>();
        for (int i = 0; i < party.Length; i++) goodGuySprites[i] = party[i].combatSprite;
        for (int i = 0; i < enemies.Length; i++) badGuySprites[i] = enemies[i].sprite;
        for(int i = 0; i < goodGuySprites.Length; i++) goodGuyRenderers[i].sprite = goodGuySprites[i];
        for (int i = 0; i < badGuySprites.Length; i++) badGuyRenderers[i].sprite = badGuySprites[i];
        
        
    }
    
    private void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.E))
        {
            exitCombat(); 
        }*/
        if (partyData.isCombatTutorial)
        {
            tutorialParent.SetActive(true); 
            tutorialText.gameObject.SetActive(true); 
            Debug.Log("combat tutorial");
            CombatTutorial(); 
        }
        
    }
    private void CombatTutorial()
    {
        if (!isWriting && !isWritingFinished)
        {
            switch (textNum)
            {
                case 0:
                    StopAllCoroutines();
                    tutorialText.text = "";                   
                    StartCoroutine(typeWriterText("Welcome to Combat!\n [Use LMB to change text]", 0.03f)); 
                    isWriting = true; 
                    break;
                case 1:
                    StopAllCoroutines();
                    tutorialText.text = "";           
                    StartCoroutine(typeWriterText("Before we get started you need to select a player.\nYou can do so using the arrow keys to move the cursor, and pressing enter on the player you want to select.", 0.03f));
                    isWriting = true;
                    break;
                case 2:
                    StopAllCoroutines();
                    tutorialText.text = "";
                    Debug.Log("Combat tutorial text 2"); 
                    StartCoroutine(typeWriterText($"This is the Action Selection Menu{'\u2122'}\n[Use LMB to change text]", 0.03f));
                    isWriting = true;
                    break;
                case 3:
                    StopAllCoroutines();
                    tutorialText.text = "";
                    StartCoroutine(typeWriterText("From here you can select whether you want to attack or use an item.\n[Use LMB to change text]", 0.03f));
                    isWriting = true;
                    break;
                case 4:
                    StopAllCoroutines();
                    tutorialText.text = "";
                    StartCoroutine(typeWriterText("During combat, each party member can only perform 1 action. Using items does not count as an action\n[Use LMB to change text]", 0.03f));
                    isWriting = true;
                    break;
                case 5:
                    StopAllCoroutines();
                    tutorialText.text = "";
                    StartCoroutine(typeWriterText("Try attacking an enemy. You can do so by entering the combat menu and selecting what attack you want to use.", 0.03f));
                    isWriting = true;
                    break;
                case 6:
                    StopAllCoroutines();
                    tutorialText.text = "";
                    StartCoroutine(typeWriterText("Now select what enemy you would like to attack by using the arrow keys, and hit enter when you are ready to perform your attack", 0.03f));
                    isWriting = true;
                    break;
                case 7:
                    StopAllCoroutines();
                    tutorialText.text = "";
                    StartCoroutine(typeWriterText("Congrats! You have just exercised your right to self defense!\n[Use LMB to change text]", 0.03f));
                    isWriting = true;
                    break;
                case 8:
                    StopAllCoroutines();
                    tutorialText.text = "";
                    StartCoroutine(typeWriterText("Keep in mind, your attacks have a cooldown that may take multiple turns to finish\n[Use LMB to change text]", 0.03f));
                    isWriting = true;
                    break;
                case 9:
                    StopAllCoroutines();
                    tutorialText.text = "";
                    StartCoroutine(typeWriterText("Thats it for the tutorial, remember to keep an eye on your health, and most importantly, HAVE FUN!\n[Use LMB to end tutorial]", 0.03f));
                    isWriting = true;
                    break;
                default:
                    //StopAllCoroutines();
                    partyData.isCombatTutorial = false;
                    tutorialText.gameObject.SetActive(false);
                    isWriting = false;
                    isWritingFinished = true; 
                    break; 
            }
        }
        if(combatObject.playerHasBeenSelected && !hasSelectedPlayer)
        {
            hasSelectedPlayer = true;
            isWriting = false;
            isWritingFinished = false; 
            textNum = 2;
            CombatTutorial(); 
        }
        if (combatObject.hasAttackBeenSelected && !hasSelectedAttack)
        {
            hasSelectedAttack = true;
            isWriting = false;
            isWritingFinished = false;
            textNum = 6; 
            CombatTutorial(); 
        }
        if(combatObject.hasEnemyBeenSelected && !hasSelectedEnemy)
        {
            hasSelectedEnemy = true;
            isWriting = false;
            isWritingFinished = false;
            textNum = 7;
            CombatTutorial();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hasSelectedPlayer) 
            {
                if (!hasSelectedAttack)
                {
                    textNum = Mathf.Clamp(textNum+1,0,5);
                    isWritingFinished = false;
                }
                else if (!hasSelectedEnemy)
                {
                    textNum = Mathf.Clamp(textNum + 1, 0, 6);
                    isWritingFinished = false;
                }
                else
                {
                    textNum++;
                    isWritingFinished = false;
                }
            }
            else
            {
                textNum = Mathf.Clamp(textNum + 1, 0, 1);
                isWritingFinished = false; 
            }
            
        }
        
    }
    public IEnumerator exitCombat()
    {
        combatObject.exitCombat();
        tutorialText.gameObject.SetActive(false) ;
        textNum = 0; 
        tutorialText.text = "";
        isWritingFinished = false; 
        StartCoroutine(typeWriterText("You defeated the enemy!\n[Press LMB to continue]", 0.03f));
        yield return new WaitUntil(()=>isWritingFinished);
        SceneManager.LoadScene(partyData.PreviousSceneName);
       
       
    }
    private IEnumerator typeWriterText(string text, float charDelaySeconds)
    {
        tutorialText.maxVisibleCharacters = 0; 
        tutorialText.text = text; 
        while(tutorialText.maxVisibleCharacters != text.Length)
        {
            tutorialText.maxVisibleCharacters++;
            yield return new WaitForSecondsRealtime(charDelaySeconds); 
        }
        isWriting = false;
        isWritingFinished = true; 
    }
}
