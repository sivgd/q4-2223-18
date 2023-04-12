using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnDestroy : MonoBehaviour
{
    public GameObject activate;
    public DialougeManager dm; 
    private void OnDestroy()
    {
        activate.SetActive(true);
        dm.FreezePlayer = false; 
    }
}
