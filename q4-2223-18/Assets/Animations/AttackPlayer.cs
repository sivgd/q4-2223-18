using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public Transform recipient;
    public Transform attacker;
    public GameObject attackReticle;
    public SFXManager sfxManager; 
    private SpriteRenderer renderer;
    private bool animFinished = false; 
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>(); 
    }
    private void Update()
    {
        if (animFinished)
        {
            renderer.enabled = false;
            attackReticle.SetActive(false);
            animFinished = false; 
        }
    }
    public void attackPlayerAnimation(Transform recipientTransform, Transform attackerTransform,Sprite attackerSprite, AnimManager aManager,int recipientNum)
    {
        recipient = recipientTransform;
        attacker = attackerTransform;
        renderer.enabled = true;
        renderer.sprite = attackerSprite;
        attackReticle.SetActive(true);
        attackReticle.transform.position = recipientTransform.position;
        transform.position = attackerTransform.position;
        animFinished = false; 
        StartCoroutine(attackPlayer(aManager,recipientNum)); 
        
    }
    private IEnumerator attackPlayer(AnimManager aManager, int recipientNum)
    {
        Vector2 endingPosition = new Vector2(recipient.position.x + 2, recipient.position.y);
        float t = 0;
        while (!transform.position.Equals(endingPosition))
        {
            transform.position = Vector2.Lerp(attacker.position, endingPosition, t);
            t += Time.deltaTime*1.5f; 
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(0.2f); 
        aManager.playAttackAnim(AttackAnim.Slash, true, recipientNum);
        sfxManager.playAttackAudio(1); 
        yield return new WaitForSecondsRealtime(0.3f);
        animFinished = true; 
        

        

    }
}
