using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class SceneEventManager : MonoBehaviour
{
    [Header("Event")]
    public bool firstCutscene = false;

    [Header("Ref")]
    public CameraFollow cameraFollow;
    public DialougeManager dManager; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstCutscene)
        {
            StartCoroutine(FirstCutscene());
            firstCutscene = false; 
        }
    }
    private IEnumerator FirstCutscene()
    {
        cameraFollow.cameraShake = true;
        yield return new WaitForSecondsRealtime(0.3f);
        cameraFollow.cameraShake = false;
        dManager.nameBox.text = "";
        dManager.gameObject.SetActive(true);
        dManager.changeCurrentDialouge(new string[] { "TED TRIANGLE!", "ITS THE FIRST OF THE MONTH!", "PAY YOUR MANDATORY TITHE" }, 0.05f); 
    }
}
