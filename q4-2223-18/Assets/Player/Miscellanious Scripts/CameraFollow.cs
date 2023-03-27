using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("External References")]
    public Transform player;
    public Vector3 transformOffset;
    public float shakeDuration = 1f;
    public float intensity = 1f; 
    public bool cameraShake; 
    private void Update()
    {
        float pX = player.position.x;
        float pY = player.position.y;
        transform.position = new Vector3(pX + transformOffset.x, pY + transformOffset.y, -10f + transformOffset.z);
        if (cameraShake)
        {
            ShakeCamera(); 
        }
    }
    public void ShakeCamera()
    {
        StartCoroutine(Shake()); 
    }
    private IEnumerator Shake()
    {
        Vector2 startPosition = transform.position; 
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = startPosition + (Random.insideUnitCircle) * intensity;
            transform.position = transform.position + new Vector3(0f, 0f, -10f); 
            yield return new WaitForEndOfFrame();
        }
        transform.position = startPosition; 
    } 
}
