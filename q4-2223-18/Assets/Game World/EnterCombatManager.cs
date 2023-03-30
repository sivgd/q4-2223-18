using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnterCombatManager : MonoBehaviour
{
    [Header("References")]
    public PersistantPartyData pData;
    public PersistantEntityData eData;
    public TrackPartyData tPartyData; 
    public Player[] party = new Player[3];
    public Enemy[] enemies = new Enemy[3]; 
    public void enterCombat(IndividualEntityData[] enemies,bool isTutorial)
    {
        pData.isCombatTutorial = isTutorial;
        tPartyData.savePartyData(); 
        for(int i = 0; i < enemies.Length; i++)
        {
            eData.addEntity(enemies[i].id);
            this.enemies[i].sprite = enemies[i].combatSprite;
            this.enemies[i].Health = enemies[i].health;
            this.enemies[i].BaseAttack = (int)enemies[i].attack;
            this.enemies[i].Level = enemies[i].level;
            this.enemies[i].Name = enemies[i].name; 
        }
        SceneManager.LoadScene("Combat"); 
    }
}
