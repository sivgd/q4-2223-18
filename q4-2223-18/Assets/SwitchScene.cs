using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SwitchScene : MonoBehaviour
{
    public string nextScene;
    public string playerTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            Debug.Log($"Entering {nextScene}"); 
            SceneManager.LoadScene(nextScene); 
        }
    }
}
