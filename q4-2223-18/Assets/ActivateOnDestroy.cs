using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnDestroy : MonoBehaviour
{
    public GameObject activate;
    private void OnDestroy()
    {
        activate.SetActive(true); 
    }
}
