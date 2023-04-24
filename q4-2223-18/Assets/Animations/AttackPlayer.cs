using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public Transform recipient;
    public Transform attacker;
    public GameObject attackReticle;
    public SFXManager sfxManager; 
    private SpriteRenderer spriteRenderer;
    public bool animFinished = true; 
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
    private void Update()
    {
        if (animFinished)
        {
            spriteRenderer.enabled = false;
            attackReticle.SetActive(false);
           
        }
    }
    public void attackPlayerAnimation(Transform recipientTransform, Transform attackerTransform,Sprite attackerSprite, AnimManager aManager,int recipientNum,bool successful)
    {
        recipient = recipientTransform;
        attacker = attackerTransform;
        attackerTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false; 
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = attackerSprite;
        attackReticle.SetActive(true);
        attackReticle.transform.position = recipientTransform.position;
        transform.position = attackerTransform.position;  
        animFinished = false; 
        StartCoroutine(attackPlayer(aManager,recipientNum,successful)); 
        
    }
    private IEnumerator attackPlayer(AnimManager aManager, int recipientNum,bool succesful)
    {
        Debug.Log("playing player attack animation"); 
        Vector2 endingPosition = new Vector2(recipient.position.x + 2, recipient.position.y);
        float t = 0;
        while (!transform.position.Equals(endingPosition))
        {
            transform.position = Vector2.Lerp(attacker.position, endingPosition, t);
            t += Time.deltaTime*1.5f; 
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(0.2f);
        if (succesful)
        {
            aManager.playAttackAnim(AttackAnim.Slash, true, recipientNum);
            sfxManager.playAttackAudio(1);
        }
        else
        {
            aManager.playAttackAnim(AttackAnim.Miss, true, recipientNum); 
        }
        yield return new WaitForSecondsRealtime(0.3f);
        animFinished = true;
        attacker.gameObject.GetComponent<SpriteRenderer>().enabled = true;




    }
}
