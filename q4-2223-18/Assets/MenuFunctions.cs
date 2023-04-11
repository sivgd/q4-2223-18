using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuFunctions : MonoBehaviour
{
    public void LoadL1()
    {
        SceneManager.LoadScene("Level1"); 
    }
    public void LoadCredits()
    {
        Debug.Log("Credits"); 
    }
    public void ExitGame()
    {
        Debug.Log("Goodbye!"); 
        Application.Quit(); 
    }
}
