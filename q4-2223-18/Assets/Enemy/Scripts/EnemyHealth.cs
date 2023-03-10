using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP;
    private int currentHP;


    private void Start()
    {
        currentHP = maxHP; 
    }
   public void damageEnemy(int amt)
   {
        currentHP -= amt; 
        if(currentHP <= 0)
        {
            Destroy(gameObject); 
        }
   }
}
