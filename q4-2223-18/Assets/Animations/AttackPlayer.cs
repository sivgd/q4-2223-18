using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public Transform recipient;
    public Transform attacker;
    public GameObject attackReticle;
    private SpriteRenderer renderer;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>(); 
    }

   public void attackPlayerAnimation(Transform recipientTransform, Transform attackerTransform,Sprite attackerSprite)
    {
        recipient = recipientTransform;
        attacker = attackerTransform;
        renderer.enabled = true;
        renderer.sprite = attackerSprite;
        attackReticle.SetActive(true);
        attackReticle.transform.position = recipientTransform.position;
        StartCoroutine(attackPlayer()); 
    }
    private IEnumerator attackPlayer()
    {
        Vector2 endingPosition = new Vector2(recipient.position.x + renderer.sprite.border.x, recipient.position.y);
        float t = 0;
        while (!transform.position.Equals(endingPosition))
        {
            transform.position = Vector2.Lerp(attacker.position, endingPosition, t);
            t += Time.deltaTime; 
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(5); 

    }
}
