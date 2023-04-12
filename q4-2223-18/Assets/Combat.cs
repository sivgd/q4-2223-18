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
    public TMP_Text playerName;
    public TMP_Text playerHabit; 
    public GameObject[] combatButtons;
    public Image[] attackCards;
    public Image[] itemCards; 
 


    [Header("DEBUG")]
   // [SerializeField] private int turn = 1;
    [SerializeField] private bool playerSelection = false;
    [SerializeField] private int playerPosSelected = 2;
    [SerializeField] private int playerSelected = 0; 
    [SerializeField] private int combatButtonSelected = 2;
    [SerializeField] private int itemSelected = 0; 
    [SerializeField] private int enemySelected = 0; 
    [SerializeField] private int enemyPosSelected = 0; 
    [SerializeField] private int attackSelected = 0;
    [SerializeField] private Turn turn = Turn.player; 
    [SerializeField] private UIMODE uiMode = UIMODE.none;
    public bool playerHasBeenSelected = false;
    public bool hasAttackBeenSelected = false;
    public bool hasEnemyBeenSelected = false; 

    public UIMODE UiMode { get => uiMode; set => uiMode = value; }
    public int PlayerSelected { get => playerSelected; set => playerSelected = value; }

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
            if(enemyHealthValues[i] <= 0)
            {
                GetComponent<CombatSceneManager>().badGuyRenderers[i].sprite = null; 
            }
        }
    }
    private void progressTurn()
    {
        turn = (turn == Turn.player) ? Turn.enemy : Turn.player;
        for (int i = 0; i < party.Length; i++)
        {
            party[i].HasGoneDuringTurn = false;
            party[i].TempAttackBoost = 0; 
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
    /// <summary>
    /// The update method is where the combat logic is organized and calls are made
    /// </summary>
    private void Update()
    {
        updateHealthValues(); 
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerHealthValues[0] -= EnemyAttack.getPhysicalDamageFromAttack(enemies[0].BaseAttack,enemies[0].Level);
        }
        if(enemyHealthValues[0] > 0 || enemyHealthValues[1] > 0 || enemyHealthValues[2] > 0)
        {
            if (turn == Turn.player)
            {
                /// if all players have gone during the turn then progress the turn
                if ((party[0].HasGoneDuringTurn || party[0].Health <= 0) && (party[1].HasGoneDuringTurn || party[1].Health <= 0) && (party[2].HasGoneDuringTurn || party[2].Health <= 0))
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
                        case UIMODE.playerItemSelection:
                            playerItemSelectionMode(playerSelected);
                            updateHealthValues();
                            break;
                        case UIMODE.playerPartySupport:
                            playerSupportMode(playerSelected, attackSelected);
                            updateHealthValues();
                            break;

                    }
                }
            }
            else if (turn == Turn.enemy)
            {
                Debug.Log("enemy turn");
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.Health > 0)
                    {
                        //EnemyAttack.getRandomPlayerToAttack(); TODO: impliment enemy attack
                        EnemyAttack.attackRandomPlayer(party, enemy.BaseAttack, enemy.Level);
                    }
                }
                turn = Turn.player;
                playerSelection = true;
                //playerHealth.text = $"PlayerHealth: {playerHealthValues[0]}";
                //enemyHealth.text = $"EnemyHealth: {enemyHealthValues[0]}";
            }
            updateHealthValues();
        }
        else
        {
            StartCoroutine( GetComponent<CombatSceneManager>().exitCombat());
            return; 
        }
       
        
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
    private void playerSupportMode(int playerIndex, int attackIndex)
    {
        selectionCursor.SetActive(true);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerPosSelected = Mathf.Clamp(playerPosSelected + 1, 1, 3);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerPosSelected = Mathf.Clamp(playerPosSelected - 1, 1, 3);
        }
        switch (playerPosSelected) /// visual cursor selection stuff
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (party[playerIndex].Attacks[attackIndex].AttackType)
            {
                case AttackType.Healing:
                    party[playerSelected].Health = Mathf.Clamp(party[playerSelected].Health + party[playerIndex].Attacks[attackIndex].Damage, 0, party[playerSelected].maxHealth); /// say goodbye to overheal :(
                    party[playerIndex].Attacks[attackIndex].resetCoolDown();
                    break;
                case AttackType.AttackBoost:
                    party[playerSelected].TempAttackBoost = Mathf.Clamp(party[playerSelected].Health + party[playerIndex].Attacks[attackIndex].Damage, 0, party[playerSelected].maxHealth); /// say goodbye to overheal :(
                    party[playerIndex].Attacks[attackIndex].resetCoolDown();
                    break;
            }
            Debug.Log($"{party[playerSelected].Name} was healed by {party[playerIndex].Name}");
            uiMode = UIMODE.none;
            playerSelection = true;
            party[playerIndex].HasGoneDuringTurn = true;
        }

    }
    /// <summary>
    /// playerItemSelectionMode allows the player to select items 
    /// </summary>
    /// <param name="playerIndex"></param> the index of the selected player 
    private void playerItemSelectionMode(int playerIndex)
    {
       // bool card1Selected = false,card2Selected = false,card3Selected = false; 
        int maxIndex = Mathf.Min(party[playerIndex].Items.Count, itemCards.Length); /// initializes and activates the item cards 
        for(int i = 0; i< maxIndex; i++)
        {
            if (!itemCards[i].gameObject.activeInHierarchy)
            {
                itemCards[i].sprite = party[playerIndex].Items[i].defaultIcon;
                itemCards[i].gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            itemSelected = Mathf.Clamp(itemSelected - 1,0,maxIndex - 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            itemSelected = Mathf.Clamp(itemSelected + 1, 0, maxIndex - 1);
        }
        for(int i = 0; i < maxIndex; i++) /// changes the sprite of the item cards based on selection 
        {
            if(i == itemSelected)
            {
                itemCards[i].sprite = party[playerIndex].Items[itemSelected].selectedIcon; 
            }
            else
            {
                itemCards[i].sprite = party[playerIndex].Items[i].defaultIcon;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return)) /// use the item and delete it from the player's list of items 
        {
            ItemFunction itemFunction = party[playerIndex].Items[itemSelected].function;
            switch (itemFunction)
            {
                case ItemFunction.healing:
                    party[playerIndex].Health += party[playerIndex].Items[itemSelected].boost;
                    party[playerIndex].Items.RemoveAt(itemSelected);
                    party[playerIndex].Items.TrimExcess(); 
                    break;
                case ItemFunction.attackBoost:
                    party[playerIndex].TempAttackBoost += party[playerIndex].Items[itemSelected].boost;
                    party[playerIndex].Items.RemoveAt(itemSelected);
                    party[playerIndex].Items.TrimExcess();
                    break;
            }
            uiMode = UIMODE.none;
            playerSelection = true;
            for (int i = 0; i < maxIndex; i++) itemCards[i].gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiMode = UIMODE.none;
            playerSelection = true;
            for (int i = 0; i < maxIndex; i++) itemCards[i].gameObject.SetActive(false);
        }
    }
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
            else if(combatButtonSelected == 2)
            {
                uiMode = UIMODE.playerItemSelection;
                foreach (GameObject button in combatButtons)
                {
                    button.SetActive(false);
                }
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiMode = UIMODE.none;
            playerSelection = true;
            for (int i = 0; i < combatButtons.Length; i++) combatButtons[i].gameObject.SetActive(false);
        }

    }
    /// <summary>
    /// playerAttackSelectionMode is pretty self explanitory, allows for the player to select an attack before actually performing it 
    /// </summary>
    /// <param name="playerIndex"></param>
    private void playerAttackSelectionMode(int playerIndex)
    {
        hasAttackBeenSelected = false; 
       // Debug.Log($"{party[playerIndex].name}"); 
        Debug.Log($"{Mathf.Min(attackCards.Length, party[playerIndex].Attacks.Length)}");
        int maxCardIndex = Mathf.Min(attackCards.Length, party[playerIndex].Attacks.Length); 
        for (int i = 0; i < maxCardIndex; i++)
        {
            if (!attackCards[i].gameObject.activeInHierarchy) ///Initializes the attack cards, the menu can only show 3 cards at a time
            {
                attackCards[i].sprite = party[playerIndex].Attacks[i].defaultIcon;
                Debug.Log($"{attackCards[i].name}");
                attackCards[i].gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            attackSelected = Mathf.Clamp(attackSelected - 1,0, maxCardIndex - 1); 
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            attackSelected = Mathf.Clamp(attackSelected + 1, 0, maxCardIndex - 1);
        }
        for(int i = 0; i < maxCardIndex; i++) /// changes the sprite of the card based on selection 
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
        if (Input.GetKeyDown(KeyCode.Return)) /// if the player hits enter, check if the attack can be used, and then change modes to either select a enemy to attack or a player to heal
        {
            if (party[playerIndex].Attacks[attackSelected].Cooldown > 0)
            {
                Debug.Log("Attack needs to cooldown!"); 
            }
            else if(party[playerIndex].Attacks[attackSelected].AttackType == AttackType.Healing)
            {
                uiMode = UIMODE.playerPartySupport;
                for (int i = 0; i < maxCardIndex; i++) attackCards[i].gameObject.SetActive(false);
            }
            else 
            {
                uiMode = UIMODE.playerAttackEnemy;
                for (int i = 0; i < maxCardIndex; i++) attackCards[i].gameObject.SetActive(false);
                hasAttackBeenSelected = true;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiMode = UIMODE.none;
            playerSelection = true;
            for (int i = 0; i < maxCardIndex; i++) attackCards[i].gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// playerAttackEnemyMode is what allows the player to apply damage to an enemy
    /// </summary>
    /// <param name="playerIndex"></param> the index of the player that is attacking in the partys
    /// <param name="attackIndex"></param> the index of the attack that the player is performing 
    private void playerAttackEnemyMode(int playerIndex, int attackIndex)
    {
        hasEnemyBeenSelected = false; 
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
        if(enemies[enemySelected].sprite != null)
        {
            playerHealth.text = "Health: " + enemies[enemySelected].Health;
            playerName.text = "Name: " + enemies[enemySelected].Name;
            if (playerHabit.gameObject.activeInHierarchy)
            {
                playerHabit.gameObject.SetActive(false);
            }
        }
        /// if the enter button is pressed, attack the enemy, reset attack cooldowns, and change mode back to player selection 
        if (Input.GetKeyDown(KeyCode.Return)){
            hasEnemyBeenSelected = true; 
            PlayerAttack.attackWithStats(attackSelected, party[playerIndex], enemies[enemySelected]);
            party[playerIndex].Attacks[attackIndex].resetCoolDown();
            Debug.Log($"{party[playerIndex].name} attacked {enemies[enemySelected].name}");
            uiMode = UIMODE.none;
            playerSelection = true;
            party[playerIndex].HasGoneDuringTurn = true; 

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiMode = UIMODE.none;
            playerSelection = true;
        }

    }  
    /// <summary>
    /// Player selection mode moves impliments the logic behind the UI selection cursor and actually selecting a player
    /// </summary>
    private void PlayerSelectionMode()
    {
        playerHasBeenSelected = false; 
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
        if (Input.GetKeyDown(KeyCode.Return) && !party[PlayerSelected].HasGoneDuringTurn)
        {
           for(int i = 0; i < combatButtons.Length; i++) /// Buttons change sprites based on activation 
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
            playerHasBeenSelected = true; 
            playerSelection = false;
            selectionCursor.SetActive(false);
            uiMode = UIMODE.playerActionSelection;
        }
        switch (playerPosSelected) /// visual cursor selection stuff
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
       if(party[playerSelected].combatSprite != null)
        {
            if (!playerHabit.gameObject.activeInHierarchy)
            {
                playerHabit.gameObject.SetActive(true);
            }
            playerHealth.text = "Health: " + party[playerSelected].Health;
            playerName.text = "Name: " + party[playerSelected].Name;
            playerHabit.text = "Habit: " + party[playerSelected].habit.name;
        }
    }
    #endregion playerAttackLogic
    
    
    enum Turn
    {
        player,
        enemy
    }
}
public enum UIMODE
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
    playerItemSelection,
    playerPartySupport,
    none
}
public class PlayerAttack
{
    public static void attackWithStats(int attack,Player player, Enemy enemy)
    {
        AttackType aType = player.Attacks[attack].AttackType;
        HabitType pHabit = player.habit.habitType; 
        Debug.Log(player.Attacks[attack].Name); 
        double damage = (player.Attacks[attack].Damage + player.TempAttackBoost)  * player.BaseAttack;
        if (aType.ToString().Equals(pHabit.ToString()))
        {
            damage = (damage * (1.0 + player.habit.Boost)); 
        }
        enemy.Health -= (int)Mathf.Ceil((float)damage); 
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
    public static void attackRandomPlayer(Player[] players, int baseDamage, int enemyLevel)
    {
        int playerToAttack = getRandomPlayerToAttack(players);
        //TODO: Add defense 
        int damage = getPhysicalDamageFromAttack(baseDamage, enemyLevel); 
        if(damage <= players[playerToAttack].BaseDefense + players[playerToAttack].TempDefenseBoost)
        {
            Debug.Log($"{players[playerToAttack].Name}'s armor deflected the blow!");
            return; 
        }
        else
        {
            double defenseRoll = (double)(players[playerToAttack].BaseDefense + players[playerToAttack].TempDefenseBoost) / (double)damage;
            if (players[playerToAttack].habit.habitType == HabitType.Defender) defenseRoll *= (1 + players[playerToAttack].habit.Boost); 
            if (Random.value <= defenseRoll)
            {
                Debug.Log($"{players[playerToAttack].Name}'s armor deflected the blow! {defenseRoll} {damage}");
                return; 
            }
            else
            {
                players[playerToAttack].Health -= damage;
                Debug.Log($"{players[playerToAttack].Name} took damage! {damage}");
            }
        }
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
