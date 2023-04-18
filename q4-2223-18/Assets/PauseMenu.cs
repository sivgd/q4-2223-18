using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    private bool disabledElements = true; 
    public GameObject[] uiElements; 
    private void Update()
    {
        if (isPaused)
        {
            if (disabledElements)
            {
                for (int i = 0; i < uiElements.Length; i++)
                {
                    uiElements[i].SetActive(true);
                }
                disabledElements = false;
            }
            Time.timeScale = 0;
            
        }
        else
        {
            Time.timeScale = 1;
            if (!disabledElements)
            {
                
                for (int i = 0; i < uiElements.Length; i++)
                {
                    uiElements[i].SetActive(false);
                }
                disabledElements = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            isPaused = !isPaused; 
        }
    }
    public void unPause()
    {
        isPaused = false;
        disabledElements = false; 
    }
    public void quitGame()
    {
        Application.Quit(); 
    }
}
