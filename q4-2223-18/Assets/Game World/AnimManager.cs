using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public Animator[] enemyAttacks = new Animator[3];
    public Animator[] playerAttacks = new Animator[3];

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            playAttackAnim(AttackAnim.Bash, false, 0); 
        }
    }
    public void playAttackAnim(AttackAnim anim,bool player ,int recipient)
    {
        if (!player)
        {
            switch (anim)
            {
                case AttackAnim.Slash:
                    StartCoroutine(playAnim(false, recipient, "Slash", 0.35f)); 
                    break;
                case AttackAnim.Bash:
                    StartCoroutine(playAnim(false, recipient, "Bash", 0.45f));
                    break;
                case AttackAnim.MagicDart:
                    StartCoroutine(playAnim(false, recipient, "MDart", .52f));
                    break; 
                default:
                    Debug.Log("That animation has not been implimented yet :P");
                    break; 
            }
        }
        else
        {
            switch (anim)
            {
                case AttackAnim.Heal:
                    //StartCoroutine(playAnim(true, recipient,,);
                    break; 
            }
        }
    }
    private IEnumerator playAnim(bool player,int recipient,string booleanName,float waitTime)
    {
        if (!player)
        {
            enemyAttacks[recipient].gameObject.SetActive(true);
            enemyAttacks[recipient].SetBool(booleanName, true);
            yield return new WaitForSecondsRealtime(waitTime);
            enemyAttacks[recipient].SetBool(booleanName, false);
            enemyAttacks[recipient].gameObject.SetActive(false);
        }
    }
}
public enum AttackAnim
{
    None,
    Heal,
    Slash,
    Bash,
    MagicDart
}
