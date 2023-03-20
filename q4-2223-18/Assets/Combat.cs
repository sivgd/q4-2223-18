using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 


public class Combat : MonoBehaviour
{
    private int[] playerHealthValues;
    private int[] enemyHealthValues; 

    [Header("External References")]
    public Player[] party = new Player[3];
    public Enemy[] enemies = new Enemy[3]; 
    public GameObject selectionCursor;
    public Transform[] playerPartyTransforms = new Transform[3];
    public Transform[] enemyPartyTransforms = new Transform[3]; 
    
    [Header("UI References")]
    public TMP_Text playerHealth;
    public TMP_Text enemyHealth;
    public GameObject[] combatButtons;
 


    [Header("DEBUG")]
   // [SerializeField] private int turn = 1;
    [SerializeField] private bool playerSelection = false;
    [SerializeField] private int playerPosSelected = 2;
    [SerializeField] private int combatButtonSelected = 2;
    [SerializeField] private int action = 1; 
    [SerializeField] private AttackMode attackMode = AttackMode.none;
    
    private void Start()
    {
        playerHealthValues = new int[party.Length];
        enemyHealthValues = new int[enemies.Length]; 
        for(int i = 0; i < playerHealthValues.Length; i++)
        {
            playerHealthValues[i] = party[i].Health; 
        }
       for(int i = 0; i < enemyHealthValues.Length; i++)
        {
            enemyHealthValues[i] = enemies[i].Health; 
        }

        // enemyHealthValues[0] = 
    }
    public void exitCombat()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].BaseAttack= 0;
            enemies[i].Health = 0; 
        }
    }
    private void Update()
    {
        playerHealth.text = $"PlayerHealth: {playerHealthValues[0]}";
        enemyHealth.text = $"EnemyHealth: {enemyHealthValues[0]}"; 
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerHealthValues[0] -= EnemyAttack.getPhysicalDamageFromAttack((int)enemies[0].BaseAttack,enemies[0].Level);
        }
        if (playerSelection)
        {
            PlayerSelectionMode(); 
        }
        else if(attackMode == AttackMode.player)
        {
            //Select Enemy; 
            // PlayerAttack.attackWithStats(); 
            //selectionCursor.SetActive(true);
            PlayerAttackMode(playerPosSelected); 
        }
    }
    private void PlayerAttackMode(int playerIndex)
    {
        bool button1Active = false, button2Active = false;
        Debug.Log("Player Attack Mode"); 
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            combatButtonSelected = Mathf.Clamp(combatButtonSelected + 1, 1, 2);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            combatButtonSelected = Mathf.Clamp(combatButtonSelected - 1, 1, 2);
        }
        switch (combatButtonSelected)
        {
            case 1:
                button2Active = false;
                button1Active = true;                
                break;
            case 2:
                button1Active = false;
                button2Active = true; 
                break; 
        }
        combatButtons[0].SetActive(!button1Active); combatButtons[1].SetActive(button1Active);
        combatButtons[2].SetActive(!button2Active); combatButtons[3].SetActive(button2Active);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"Entering menu {combatButtonSelected}");
            
        }
    }

    private void attackEnemyWithAttack(int playerIndex)
    {

    }
        
    private void PlayerSelectionMode()
    {
        attackMode = AttackMode.none;
        selectionCursor.SetActive(true);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerPosSelected = Mathf.Clamp(playerPosSelected + 1, 1, 3);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerPosSelected = Mathf.Clamp(playerPosSelected - 1, 1, 3);
        }
        if (Input.GetKeyDown(KeyCode.Return) && !party[playerPosSelected - 1].HasGoneDuringTurn)
        {
           for(int i = 0; i < combatButtons.Length; i++)
            {
                if(i % 2 == 0)
                {
                    combatButtons[i].SetActive(true);
                    Debug.Log(combatButtons[i].name); 
                }
                else
                {
                    combatButtons[i].SetActive(false); 
                }
            }
            playerSelection = false;
            selectionCursor.SetActive(false);
            attackMode = AttackMode.player;
        }
        switch (playerPosSelected)
        {
            case 1:
                selectionCursor.transform.position = playerPartyTransforms[2].position;
                break;
            case 2:
                selectionCursor.transform.position = playerPartyTransforms[1].position;
                break;
            case 3:
                selectionCursor.transform.position = playerPartyTransforms[0].position;
                break;
        }
    }
    enum AttackMode
    {
        player,
        enemy,
        none
    }
}
public class PlayerAttack
{
    public static void attackWithStats(int attack,Player player, Enemy enemy)
    {
        Debug.Log(player.Attacks[attack].Name); 
        int damage = player.Attacks[attack].Damage; 
        enemy.Health -= damage; 
    }
}
public class EnemyAttack
{
    public static int getPhysicalDamageFromAttack(int attack, int level)
    {
        int output = attack + (attack * Random.Range(0, Mathf.Max(1,level)));
        Debug.Log($"Damage: {output}");
        return output; 
    }
}
