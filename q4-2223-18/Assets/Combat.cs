using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 


public class Combat : MonoBehaviour
{
    private int[] playerHealthValues = new int[3];
    private int[] enemyHealthValues = new int[3]; 

    [Header("External References")]
    public CombatSceneData combatData;
    public GameObject selectionCursor;
    public Transform[] playerPartyTransforms = new Transform[3]; 
    [Header("UI References")]
    public TMP_Text playerHealth;
    public TMP_Text enemyHealth;
    
    [Header("DEBUG")]
    [SerializeField] private int turn = 1;
    [SerializeField] private bool playerSelection;
    [SerializeField] private int playerPosSelected = 2; 
    
    private void Start()
    {
        playerHealthValues[0] = combatData.PlayerHealth;
        playerHealthValues[1] = combatData.Player2Health;
        playerHealthValues[2] = combatData.Player3Health;
        enemyHealthValues[0] = combatData.Enemy1Health; 
        enemyHealthValues[1] = combatData.Enemy2Health;
        enemyHealthValues[2] = combatData.Enemy3Health;

        // enemyHealthValues[0] = 
    }
    public void exitCombat()
    {
        combatData.Enemy1Health = 0; 
        combatData.Enemy2Health = 0;
        combatData.Enemy3Health = 0;
        combatData.Enemy1Attack = 0;
        combatData.Enemy2Attack = 0;
        combatData.Enemy3Attack = 0;
    }
    private void Update()
    {
        playerHealth.text = $"PlayerHealth: {playerHealthValues[0]}";
        enemyHealth.text = $"EnemyHealth: {enemyHealthValues[0]}"; 
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerHealthValues[0] -= EnemyAttack.getPhysicalDamageFromAttack((int)combatData.Enemy1Attack,combatData.Enemy1Level);
        }
        if (playerSelection)
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
        }
        switch (playerPosSelected){
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
}
public class PlayerAttack
{

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
