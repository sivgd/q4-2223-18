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
    public Image[] attackCards; 
 


    [Header("DEBUG")]
   // [SerializeField] private int turn = 1;
    [SerializeField] private bool playerSelection = false;
    [SerializeField] private int playerPosSelected = 2;
    [SerializeField] private int playerSelected = 0; 
    [SerializeField] private int combatButtonSelected = 2;
    [SerializeField] private int enemySelected = 0; 
    [SerializeField] private int enemyPosSelected = 0; 
    [SerializeField] private int attackSelected = 0;
    [SerializeField] private Turn turn = Turn.player; 
    [SerializeField] private UIMODE uiMode = UIMODE.none;
    
    private void Start()
    {
        updateHealthValues(); 

        // enemyHealthValues[0] = 
    }
    private void updateHealthValues()
    {
        playerHealthValues = new int[party.Length];
        enemyHealthValues = new int[enemies.Length];
        for (int i = 0; i < playerHealthValues.Length; i++)
        {
            playerHealthValues[i] = party[i].Health;
        }
        for (int i = 0; i < enemyHealthValues.Length; i++)
        {
            enemyHealthValues[i] = enemies[i].Health;
        }
    }
    private void progressTurn()
    {
        turn = (turn == Turn.player) ? Turn.enemy : Turn.player;
        for (int i = 0; i < party.Length; i++)
        {
            party[i].HasGoneDuringTurn = false;
            for(int a = 0; a < party[i].Attacks.Length; a++)
            {
                party[i].Attacks[a].Cooldown -= 1; 
            }
        }
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
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerHealthValues[0] -= EnemyAttack.getPhysicalDamageFromAttack((int)enemies[0].BaseAttack,enemies[0].Level);
        }
        if(turn == Turn.player)
        {   
            if((party[0].HasGoneDuringTurn || party[0].Health <= 0) && (party[1].HasGoneDuringTurn || party[1].Health <= 0) && (party[2].HasGoneDuringTurn || party[2].Health <= 0)) /// if all players have gone during the turn then progress the turn
            {
                progressTurn();
                Update(); 
            }
            if (playerSelection)
            {
                PlayerSelectionMode();
            }
            else
            {
                switch (uiMode)
                {
                    case UIMODE.playerActionSelection:
                        playerActionSelectionMode();
                        break;
                    case UIMODE.playerAttackSelection:
                        playerAttackSelectionMode(playerSelected);
                        break;
                    case UIMODE.playerAttackEnemy:
                        playerAttackEnemyMode(playerSelected, attackSelected);
                        updateHealthValues();
                        break;
                }
            }
        }
        else if(turn == Turn.enemy)
        {
            Debug.Log("enemy turn"); 
            foreach(Enemy enemy in enemies){
                if (enemy.Health > 0)
                {
                    //EnemyAttack.getRandomPlayerToAttack(); TODO: impliment enemy attack
                    EnemyAttack.attackRandomPlayer(party, enemy.BaseAttack, enemy.Level); 
                }
            }
            turn = Turn.player;
            playerSelection = true;
            playerHealth.text = $"PlayerHealth: {playerHealthValues[0]}";
            enemyHealth.text = $"EnemyHealth: {enemyHealthValues[0]}";
        }
        playerHealth.text = $"PlayerHealth: {playerHealthValues[0]}";
        enemyHealth.text = $"EnemyHealth: {enemyHealthValues[0]}";
        /*  else if(uiMode == UIMODE.playerActionSelection)
          {
              //Select Enemy; 
              // PlayerAttack.attackWithStats(); 
              //selectionCursor.SetActive(true);
              playerActionSelectionMode(); 
          }
          else if(uiMode == UIMODE.playerAttackSelection)
          {
              playerAttackSelectionMode(playerSelected);
          }
          else if*/
    }
    #region playerAttackLogic
    private void playerActionSelectionMode()
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
            if (combatButtonSelected == 1)
            {
                uiMode = UIMODE.playerAttackSelection; 
                foreach(GameObject button in combatButtons)
                {
                    button.SetActive(false); 
                }
            }
            
        }
    }

    private void playerAttackSelectionMode(int playerIndex)
    {
       // Debug.Log($"{party[playerIndex].name}"); 
        Debug.Log($"{Mathf.Min(attackCards.Length, party[playerIndex].Attacks.Length)}");
        int maxCardIndex = Mathf.Min(attackCards.Length, party[playerIndex].Attacks.Length); 
        for (int i = 0; i < maxCardIndex; i++)
        {
            attackCards[i].sprite = party[playerIndex].Attacks[i].defaultIcon;
            Debug.Log($"{attackCards[i].name}"); 
            attackCards[i].gameObject.SetActive(true); 
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            attackSelected = Mathf.Clamp(attackSelected - 1,0, maxCardIndex - 1); 
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            attackSelected = Mathf.Clamp(attackSelected + 1, 0, maxCardIndex - 1);
        }
        for(int i = 0; i < maxCardIndex; i++)
        {
            if(i == attackSelected)
            {
                attackCards[i].sprite = party[playerIndex].Attacks[i].selectedIcon; 
            }
            else
            {
                attackCards[i].sprite = party[playerIndex].Attacks[i].defaultIcon;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (party[playerIndex].Attacks[attackSelected].Cooldown > 0)
            {
                Debug.Log("Attack needs to cooldown!"); 
            }
            else
            {
                uiMode = UIMODE.playerAttackEnemy;
                for (int i = 0; i < maxCardIndex; i++) attackCards[i].gameObject.SetActive(false);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiMode = UIMODE.none;
            playerSelection = true;
            for (int i = 0; i < maxCardIndex; i++) attackCards[i].gameObject.SetActive(false);
        }
    }
    private void playerAttackEnemyMode(int playerIndex, int attackIndex)
    {
        selectionCursor.SetActive(true);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            enemyPosSelected = Mathf.Clamp(enemyPosSelected - 1, 0, 2); 
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            enemyPosSelected = Mathf.Clamp(enemyPosSelected + 1, 0, 2);
        }
        switch (enemyPosSelected)
        {
            case 0:
                selectionCursor.transform.position = enemyPartyTransforms[0].position;
                enemySelected = 1; 
                break;
            case 1:
                selectionCursor.transform.position = enemyPartyTransforms[1].position;
                enemySelected = 0; 
                break;
            case 2:
                selectionCursor.transform.position = enemyPartyTransforms[2].position;
                enemySelected = 2; 
                break; 
        }
        if (Input.GetKeyDown(KeyCode.Return)){
            PlayerAttack.attackWithStats(attackSelected, party[playerIndex], enemies[enemySelected]);
            party[playerIndex].Attacks[attackIndex].resetCoolDown();
            Debug.Log($"{party[playerIndex].name} attacked {enemies[enemySelected].name}");
            uiMode = UIMODE.none;
            playerSelection = true;
            party[playerIndex].HasGoneDuringTurn = true; 

        }

    }  
    private void PlayerSelectionMode()
    {
        uiMode = UIMODE.none;
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
            uiMode = UIMODE.playerActionSelection;
        }
        switch (playerPosSelected)
        {
            case 1:
                selectionCursor.transform.position = playerPartyTransforms[2].position;
                playerSelected = 1; 
                break;
            case 2:
                selectionCursor.transform.position = playerPartyTransforms[1].position;
                playerSelected = 0; 
                break;
            case 3:
                selectionCursor.transform.position = playerPartyTransforms[0].position;
                playerSelected = 2; 
                break;
        }
    }
    #endregion playerAttackLogic
    
    enum UIMODE
    {
        /// <summary>
        /// player selects what action they want to do in this mode
        /// </summary>
        playerActionSelection,
        /// <summary>
        /// Player selects what attack they want to use in this mode
        /// </summary>
        playerAttackSelection,
        /// <summary>
        /// Player selects what enemy they want to attack in this mode 
        /// </summary>
        playerAttackEnemy, 
        none
    }
    enum Turn
    {
        player,
        enemy
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
    private static int getRandomPlayerToAttack(Player[] players)
    {
        bool playerAttack = false; 
       if(players.Length == 1)
       {
            return 0; 
       }
       else
       {
            int rand = 0; 
            while (!playerAttack)
            {
                rand = Random.Range(0, players.Length - 1);
                if (players[rand].Health > 0) playerAttack = true;
                else playerAttack = false; 
            }
            return rand; 
           
       }
        
    }
    public static void attackRandomPlayer(Player[] players, int damage, int enemyLevel)
    {
        int playerToAttack = getRandomPlayerToAttack(players);
        players[playerToAttack].Health -= getPhysicalDamageFromAttack(damage, enemyLevel); 
        Debug.Log($"Attacked player: {players[playerToAttack].Name}");
    }
    /// <summary>
    /// [Debug]
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static int getPhysicalDamageFromAttack(int attack, int level)
    {
        int output = attack + (attack * Random.Range(0, Mathf.Max(1,level)));
        Debug.Log($"Damage: {output}");
        return output; 
    }

}
