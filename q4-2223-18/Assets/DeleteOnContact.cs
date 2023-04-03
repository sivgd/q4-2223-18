using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnContact : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            DontDestroyOnLoad(gameObject); 
        }
    }
}
