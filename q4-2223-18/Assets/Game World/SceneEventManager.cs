using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro; 

public class SceneEventManager : MonoBehaviour
{
    [Header("Event")]
    public bool firstCutscene = false;

    [Header("Ref")]
    public CameraFollow cameraFollow;
    public DialougeManager dManager;
    public GameObject dObj;

    private void Start()
    {
        firstCutscene = true; 
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
        yield return new WaitForSecondsRealtime(1f);
        cameraFollow.cameraShake = false; 
       // dManager.nameBox.text = "???";
        //dManager.dialougeBox.text = ""; 
        dObj.SetActive(true);
        yield return new WaitUntil(()=>dObj.activeInHierarchy);
        dManager.nameBox.text = "???";
        dManager.changeCurrentDialouge(new string[] { "TED TRIANGLE!", "ITS THE FIRST OF THE MONTH!", "PAY YOUR MANDATORY TITHE" }, 0.03f);

    }
}
