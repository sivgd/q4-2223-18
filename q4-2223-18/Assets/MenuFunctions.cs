using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 

public class MenuFunctions : MonoBehaviour
{
   
    [SerializeField] private GameObject nonCreditButtons;
    [SerializeField] private GameObject creditStuff;
    
   
    public void LoadL1()
    {
        SceneManager.LoadScene("Level1"); 
    }
    public void ExitGame()
    {
        Debug.Log("Goodbye!"); 
        Application.Quit(); 
    }
    public void ActivateCredits()
    {
        nonCreditButtons.SetActive(false); 
        creditStuff.SetActive(true); 
    }
    public void HideCredits()
    {
        creditStuff.SetActive(false);
        nonCreditButtons.SetActive(true); 
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleCard");
    }
}
