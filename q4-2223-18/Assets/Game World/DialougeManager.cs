using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class DialougeManager : MonoBehaviour
{
    public TMP_Text nameBox;
    public TMP_Text dialougeBox;
    [Header("Preferences")]
    public float typeWriterDelay = 0.09f;
    [Header("Debug")]
    [SerializeField] private bool runDialouge;
    [SerializeField] private bool updateDialouge; 
    [SerializeField] private string[] dialouge;
    [SerializeField] private int currentDialougeString = 0;
    public bool currentDialougeFinished;
    [SerializeField] private IEnumerator currentDialougeRoutine; 
    public void changeCurrentDialouge(int id)
    {
        if(id >= DialougeManifest.dialougeDatabase.Length)
        {
            id = DialougeManifest.dialougeDatabase.Length - 1;
        }
        dialouge = DialougeManifest.dialougeDatabase[id];
        currentDialougeString = 0; 
        runDialouge = true;
        updateDialouge = true; 
        currentDialougeRoutine = scrollingDialouge(dialouge[currentDialougeString],typeWriterDelay);
    }
    public void changeCurrentDialouge(string[] dialougeList,float charDelay)
    {
        Debug.Log(string.Join(", ",dialougeList)); 
        currentDialougeString = 0;
        Debug.Log("Reset the dialouge string"); 
        typeWriterDelay = charDelay;
        Debug.Log("Set the charDelay"); 
        dialouge = dialougeList;
        Debug.Log("Changed the dialouge array");
        runDialouge = true;
        updateDialouge = true;
        Debug.Log("attempting to run the dialouge routine");
       // currentDialougeRoutine = scrollingDialouge(dialouge[currentDialougeString], typeWriterDelay);
        Debug.Log("Running the dialouge routine");
    }
    private IEnumerator scrollingDialouge(string dialougeString, float charDelaySeconds)
    {
        Debug.Log("Coroutine Start"); 
        updateDialouge = false;
        dialougeBox.text = "";
      
        while (!dialougeString.Equals(dialougeBox.text))
        {
            
            for(int i = 0; i < dialougeString.Length; i++)
            {
                dialougeBox.text += dialougeString[i];
                yield return new WaitForSecondsRealtime(charDelaySeconds); 
            }
        }
        currentDialougeFinished = true;
         
    }
    private void Update()
    {
        if (currentDialougeString >= dialouge.Length)
        {
            currentDialougeString = 0;
            updateDialouge = false;
            gameObject.SetActive(false); 
        }
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return)) && currentDialougeString < dialouge.Length)
        {
            updateDialouge = true;
            currentDialougeString++;
            currentDialougeFinished = false;
            StopCoroutine(currentDialougeRoutine);
            currentDialougeRoutine = scrollingDialouge(dialouge[currentDialougeString], typeWriterDelay);
        }
        if (runDialouge)
        {
            if (updateDialouge && !currentDialougeFinished)
            {
                currentDialougeRoutine = scrollingDialouge(dialouge[currentDialougeString], typeWriterDelay);
                StartCoroutine(currentDialougeRoutine);
            }
        }
        

    }



}
class DialougeManifest
{
    public static readonly string[] sampleDialouge = { "hello.", "this is a test.", "of the new dialouge system." };

    public static readonly string[][] dialougeDatabase = { sampleDialouge };
}
