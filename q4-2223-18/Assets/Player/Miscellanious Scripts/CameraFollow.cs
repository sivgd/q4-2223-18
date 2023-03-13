using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("External References")]
    public Transform player; 
    private void Update()
    {
        float pX = player.position.x;
        float pY = player.position.y;
        transform.position = new Vector3(pX, pY, -10f); 
    }
}
