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

    public Player[] party = new Player[3];
    public Enemy[] enemies = new Enemy[3]; 
    private Combat combatObject;
    [Header("UI Stuff")]
    public TMP_Text tutorialText;
    [SerializeField] private int textNum = 0;
    private bool isWriting = false;
    private bool isWritingFinished = false;
    private bool hasSelectedPlayer = false; 
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            combatObject.exitCombat();
            SceneManager.LoadScene(partyData.PreviousSceneName);
        }
        if (partyData.isCombatTutorial)
        {
            tutorialText.gameObject.SetActive(true);
            CombatTutorial(); 
        }
        else
        {
            tutorialText.gameObject.SetActive(false); 
        }
    }
    private void CombatTutorial()
    {
        if (!isWriting && !isWritingFinished)
        {
            switch (textNum)
            {
                case 0:
                    tutorialText.text = "";
                    StartCoroutine(typeWriterText("Welcome to Combat!\n [Use LMB to change text]", 0.03f)); 
                    isWriting = true; 
                    break;
                case 1:
                    tutorialText.text = "";
                    StartCoroutine(typeWriterText("Before we get started you need to select a player.\nYou can do so using the arrow keys to move the cursor, and pressing enter on the player you want to select.", 0.03f));
                    isWriting = true;
                    break;
                case 2:
                    tutorialText.text = "";
                    StartCoroutine(typeWriterText($"This is the Action Selection Menu{'\u2122'}", 0.03f));
                    isWriting = true;
                    break;
            }
        }
        if(combatObject.UiMode == UIMODE.playerActionSelection && combatObject.PlayerSelected == 0 && !hasSelectedPlayer)
        {
            hasSelectedPlayer = true;
            textNum++; 
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (combatObject.UiMode == UIMODE.playerActionSelection && combatObject.PlayerSelected == 0) 
            {
                textNum++;
                isWritingFinished = false; 
            }
            else
            {
                textNum = Mathf.Clamp(textNum + 1, 0, 1);
                isWritingFinished = false; 
            }
            
        }
        
    }
    private IEnumerator typeWriterText(string text, float charDelaySeconds)
    {

        while (!tutorialText.text.Equals(text))
        {
            for(int i = 0; i < text.Length; i++)
            {
                tutorialText.text += text[i];
                yield return new WaitForSecondsRealtime(charDelaySeconds); 
            }
        }
        isWriting = false;
        isWritingFinished = true; 
    }
}
