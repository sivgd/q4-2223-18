using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro; 

public class SceneEventManager : MonoBehaviour
{
    [Header("Event")]
    public bool firstCutscene = false;
    public bool secondCutscene = false; 

    [Header("Ref")]
    public CameraFollow cameraFollow;
    public DialougeManager dManager;
    public GameObject dObj;

    // Update is called once per frame
    void Update()
    {
        if (firstCutscene)
        {
            StartCoroutine(FirstCutscene());
            firstCutscene = false; 
        }
        if (secondCutscene)
        {
            StartCoroutine(SecondCutscene());
            secondCutscene = false; 
        }
    }
    private IEnumerator FirstCutscene()
    {
        cameraFollow.cameraShake = true;
        yield return new WaitForSecondsRealtime(1f);
        cameraFollow.cameraShake = false; 
       // dManager.nameBox.text = "???";
        //dManager.dialougeBox.text = ""; 
        dObj.SetActive(true);
        yield return new WaitUntil(()=>dObj.activeInHierarchy);
        dManager.nameBox.text = "???";
        dManager.changeCurrentDialouge(new string[] { "TED TRIANGLE!", "ITS THE FIRST OF THE MONTH!", "PAY YOUR MANDATORY TITHE" }, 0.03f,true);

    }
    private IEnumerator SecondCutscene()
    {
        dObj.SetActive(true);
        //yield return new WaitUntil(() => dObj.activeInHierarchy);
        dManager.nameBox.text = "Right Angle Rookie";
        dManager.changeCurrentDialouge(new string[] { "Ahhh, so you finally decided to be reasonable!", "As you know, the monthly tithe compunds daily.", "So, with an interest rate of 1.5, according to my paper here...", "You owe around ten thousand gold pieces", "Now, pay up!" }, 0.03f,true);
        yield return new WaitUntil(() => !dManager.isActiveAndEnabled);
        
    }
}
