using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class FinalFight : MonoBehaviour
{
    [Header("External References")]
    public EnterCombatManager ecm;
    public IndividualEntityData bossData;
    public PersistantEntityData pData; 
    public DialougeManager dm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (bossData.markedForDeath)
            {
                StartCoroutine(prologueCutscene()); 
            }
            else
            {
                StartCoroutine(finalCutscene());
            }
        }
    }
    private IEnumerator finalCutscene()
    {
       
        dm.gameObject.SetActive(true);
        dm.nameBox.text = "Prince Parallelogram";
        dm.changeCurrentDialouge(new string[] { "Ahh, so you're the polygon thats been making a fuss in my kingdom", "I have a few choice words for you", "So come! Put up your fists and lets settle this!" },0.03f, true,TalkerPersonality.evil);
        yield return new WaitUntil(()=>dm.gameObject.activeInHierarchy);
        yield return new WaitWhile(()=> dm.gameObject.activeInHierarchy);
        pData.addEntity(bossData.id); 
        ecm.enterCombat(new IndividualEntityData[] { bossData }, false); 
    }
    private IEnumerator prologueCutscene()
    {
        dm.gameObject.SetActive(true);
        dm.nameBox.text = "Prince Parallelogram";
        dm.changeCurrentDialouge(new string[] { "Thus concludes the life and times of Prince Parallelogram", "A fitting end, to reign defined by overbearing tyranny.", "Doomed, from the very start.", "And I don't regret a SECOND of it!" }, 0.03f, true, TalkerPersonality.evil);
        yield return new WaitUntil(() => dm.gameObject.activeInHierarchy);
        yield return new WaitWhile(() => dm.gameObject.activeInHierarchy);
        SceneManager.LoadScene("TitleCard");

    }

}
