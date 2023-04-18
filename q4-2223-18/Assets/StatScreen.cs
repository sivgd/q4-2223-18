using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class StatScreen : MonoBehaviour
{
    [Header("External References")]
    public PlayerMovement playerMovement;
    public Player[] party;
    public SFXManager sfx; 
    [Header("UI")]
    public TMP_Text[] playerList = new TMP_Text[3];
    public GameObject[] statTextObjs; 
    public TMP_Text nameText;
    public TMP_Text defText;
    public TMP_Text attackText;
    public TMP_Text habitName;
    public TMP_Text habitDescription; 
    public TMP_Text hpText;
    public TMP_Text hpBoost;
    public TMP_Text defenseBoost;
    public TMP_Text attackBoost;
    public GameObject panel;

    [Header("Debug")]
    [SerializeField] private bool listNames;
    [SerializeField] private bool nameSelection;
    [SerializeField] private bool showStats; 
    [SerializeField] private int maxPlayers = 0;
    [SerializeField] private int currNameSelected = 0;
    [SerializeField] private bool panelActive = false; 
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            
            panelActive = !panelActive; 
            listNames = true;
            showStats = false;
            if (panelActive) sfx.playAudio(2);
            else sfx.playAudio(3);            
        }
        if (!panelActive)
        {
            Debug.Log("Panel not active"); 
            listNames = false;
            showStats = false;
            nameSelection = false; 
        }
        if (listNames)
        {
            readPartyData();
            listNames = false;
            nameSelection = true;
        }
        else if (nameSelection)
        {
            playerNameSelection();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                nameSelection = false;
                playerList[0].gameObject.SetActive(false);
                playerList[1].gameObject.SetActive(false);
                playerList[2].gameObject.SetActive(false);
                showStats = true;
                sfx.playAudio(2); 
            }
        }
        else if (showStats)
        {
            ShowStats();
        }
        if (!showStats) {
            for (int i = 0; i < statTextObjs.Length; i++)
            {
                if (statTextObjs[i].activeInHierarchy)
                {
                    statTextObjs[i].SetActive(false);
                }
            }
        }
        panel.SetActive(panelActive);
        playerMovement.canMove = !panelActive; 
    }
    private void readPartyData()
    {
        string[] names = new string[3];
        int currIndex = 0; 
        foreach(Player player in party)
        {
            if(player.Name != null && player.Name != "")
            {
                names[currIndex] = player.Name;
                currIndex++; 
            }
        }
        maxPlayers = currIndex; 
        for(int i = 0; i < names.Length; i++)
        {
            if (names[i] != null && names[i] != "")
            {
                maxPlayers++; 
            }
        } 
        currIndex = 0; 
        for(int i = 0; i < names.Length; i++)
        {
            if(names[i] != null && names[i] != "")
            {
                playerList[currIndex].text = $"{i + 1}. {names[i]}.";
                playerList[currIndex].gameObject.SetActive(true); 
                currIndex++; 
            }
        }      
    }
    private void playerNameSelection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currNameSelected = Mathf.Clamp(currNameSelected - 1, 0, maxPlayers);
            if (!playerList[currNameSelected].gameObject.activeInHierarchy)
            {
                currNameSelected += 1;              
            }
            sfx.playAudio(1);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currNameSelected = Mathf.Clamp(currNameSelected + 1, 0, maxPlayers);
            if (!playerList[currNameSelected].gameObject.activeInHierarchy)
            {
                currNameSelected -= 1;     
            }
            sfx.playAudio(1);
        }
        switch (currNameSelected)
        {
            case 0:
                playerList[0].color = Color.green;
                playerList[1].color = Color.white;
                playerList[2].color = Color.white;
                break;
            case 1:
                playerList[0].color = Color.white;
                playerList[1].color = Color.green;
                playerList[2].color = Color.white;
                break;
            case 2:
                playerList[0].color = Color.white;
                playerList[1].color = Color.white;
                playerList[2].color = Color.green;
                break;
        }
    }
    private void ShowStats()
    {
        for(int i =0; i < statTextObjs.Length; i++)
        {
            if (!statTextObjs[i].activeInHierarchy)
            {
                statTextObjs[i].SetActive(true); 
            }
        }
        Player pSelected = party[currNameSelected];
        nameText.text = pSelected.Name;
        hpText.text = $"HP: {pSelected.Health}/{pSelected.maxHealth}";
        defText.text = "Defense: " + pSelected.BaseDefense;
        attackText.text = "Attack: " + pSelected.BaseAttack;
        habitName.text = pSelected.habit.name;
        string habitType = (pSelected.habit.habitType == HabitType.Defender) ? "defense" : pSelected.habit.habitType.ToString() + " attacks"; 
        habitDescription.text = pSelected.habit.explanation + "\n" + (pSelected.habit.Boost * 100) + "% boost to " + habitType;
        hpBoost.text = "";
        defenseBoost.text = (habitType.Contains("defense")) ? "+" + ((pSelected.BaseDefense * (1 + pSelected.habit.Boost)) - pSelected.BaseDefense) : " ";
        attackBoost.text = (habitType.Contains("defense")) ? "" :"+" + pSelected.habit.Boost * 100 + "% to " + pSelected.habit.habitType.ToString().ToLower() + " attacks";

    }
}
