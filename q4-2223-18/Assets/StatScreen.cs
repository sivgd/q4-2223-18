using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class StatScreen : MonoBehaviour
{
    [Header("External References")]
    public PlayerMovement playerMovement;
    public Player[] party; 
    [Header("UI")]
    public TMP_Text[] playerList = new TMP_Text[3];
    public GameObject panel;

    [Header("Debug")]
    [SerializeField] private bool listNames;
    [SerializeField] private bool nameSelection;
    [SerializeField] private bool showStats; 
    [SerializeField] private int maxPlayers = 0;
    [SerializeField] private int currNameSelected = 0; 
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            playerMovement.canMove = !playerMovement.canMove;
            panel.SetActive(!panel.activeInHierarchy);
            listNames = true;
            
        }
        if (listNames)
        {
            readPartyData();
            listNames = false;
            nameSelection = true; 
        }
        else if(nameSelection)
        {
            playerNameSelection();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                nameSelection = false;
                showStats = true; 
            }
        }
        else if (showStats)
        {

        }
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
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currNameSelected = Mathf.Clamp(currNameSelected + 1, 0, maxPlayers);
            if (!playerList[currNameSelected].gameObject.activeInHierarchy)
            {
                currNameSelected -= 1;
            }
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

    }
}
