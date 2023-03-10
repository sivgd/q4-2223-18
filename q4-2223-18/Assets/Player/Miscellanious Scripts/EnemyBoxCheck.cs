using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoxCheck : MonoBehaviour
{
    private List<EnemyHealth> enemiesHit;
    private bool checkForCollison = false; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyHealth>() != null && checkForCollison)
        {
            enemiesHit.Add(collision.GetComponent<EnemyHealth>()); 
        }
    }
    public void ClearCollisonList()
    {
        enemiesHit.Clear();
        enemiesHit.TrimExcess(); 
    }
    public void CheckForCollision(bool check)
    {
        checkForCollison = check; 
    }
    public EnemyHealth[] GetEnemiesHit()
    {
        return enemiesHit.ToArray(); 
    }
}
