using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class IndividualEntityData : MonoBehaviour
{
    public Sprite combatSprite;
    public PersistantEntityData eData; 
    public string name;
    public int id = -1;
    public int health;
    public float attack;
    public int level;

    public IndividualEntityData(Sprite combatSprite, PersistantEntityData eData, string name, int id, int health, float attack, int level)
    {
        this.combatSprite = combatSprite;
        this.eData = eData;
        this.name = name;
        this.id = id;
        this.health = health;
        this.attack = attack;
        this.level = level;
    }

    private void Start()
    {
        if (eData.hasEntity(id))
        {
            if (!name.Contains("Prince"))
            {
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator prologueCutscene()
    {
        DialougeManager dm = FindObjectOfType<DialougeManager>(); 
        dm.gameObject.SetActive(true);
        dm.nameBox.text = "Prince Parallelogram";
        dm.changeCurrentDialouge(new string[] { "Thus concludes the life and times of Prince Parallelogram", "A fitting end, to reign defined by overbearing tyranny.", "Doomed, from the very start.", "And I don't regret a SECOND of it!" }, 0.03f, true);
        yield return new WaitUntil(() => dm.gameObject.activeInHierarchy);
        yield return new WaitWhile(() => dm.gameObject.activeInHierarchy);
        SceneManager.LoadScene("TitleCard"); 

    }


}
