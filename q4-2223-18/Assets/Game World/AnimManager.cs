using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class AnimManager : MonoBehaviour
{
    public Animator[] enemyAttacks = new Animator[3];
    public Animator[] playerAttacks = new Animator[3];
    public AttackPlayer attacker;
    private List<Action> animationQueue;
    private bool playingQueue;
    public bool animationFinished = false; 

    private void Start()
    {
        animationQueue = new List<Action>(); 
    }
    private void Update()
    {
        animationFinished = attacker.animFinished;
        if (animationFinished)
        {
            Debug.Log("Animation finished"); 
        }
        else
        {
            Debug.Log("Animation is not finished"); 
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
                    StartCoroutine(playAnim(true, recipient,"Heal",.52f));
                    break;
                case AttackAnim.AttackBoost:
                    StartCoroutine(playAnim(true, recipient, "Rally", .23f));
                    break;
                case AttackAnim.Slash:
                    StartCoroutine(playAnim(true, recipient, "Slash", 0.35f));
                    break;
                case AttackAnim.Bash:
                    StartCoroutine(playAnim(true, recipient, "Bash", 0.45f));
                    break;
                case AttackAnim.MagicDart:
                    StartCoroutine(playAnim(true, recipient, "MDart", .52f));
                    break;
                case AttackAnim.Miss:
                    StartCoroutine(playAnim(true, recipient, "Miss", 0.26f));
                    break; 
                default:
                    Debug.Log("That animation has not been implimented yet :P");
                    break;

            }
            Debug.Log($"Recipient: {recipient} ");
        }
    }
    public void playAttackAnim(AttackAnim anim, bool player, int recipient, Transform perpetrator,bool successful)
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
                    StartCoroutine(playAnim(true, recipient, "Heal", .52f));
                    Debug.Log($"Recipient: {recipient} "); 
                    break;
                case AttackAnim.AttackBoost:
                    StartCoroutine(playAnim(true, recipient, "Rally", .23f));
                    break; 
                case AttackAnim.EnemyAttack:
                   
                    attacker.attackPlayerAnimation(playerAttacks[recipient].transform, perpetrator, perpetrator.gameObject.GetComponent<SpriteRenderer>().sprite,this,recipient,successful);

                    break;
                case AttackAnim.Slash:
                    StartCoroutine(playAnim(true, recipient, "Slash", 0.35f));
                    break;
                case AttackAnim.Bash:
                    StartCoroutine(playAnim(true, recipient, "Bash", 0.45f));
                    break;
                case AttackAnim.MagicDart:
                    StartCoroutine(playAnim(true, recipient, "MDart", .52f));
                    break;
                default:
                    Debug.Log("That animation has not been implimented yet :P");
                    break;


            }
        }
    }
    public void addToAnimationQueue(Action a)
    {
        animationQueue.Add(a); 
    }
    public void runAnimationQueue()
    {
        StartCoroutine(playAnimationQueue()); 
    }
    private IEnumerator playAnimationQueue()
    {
        for(int i = 0; i < animationQueue.Count; i++)
        {
            animationQueue[i].Invoke();
            yield return new WaitUntil(() => attacker.animFinished); 
        }
        animationQueue.Clear();
        animationQueue.TrimExcess();
        playingQueue = false; 
       
    }
    private IEnumerator playAnim(bool player,int recipient,string booleanName,float waitTime)
    {
        Debug.Log($"playing {booleanName}"); 
        if (!player)
        {
            enemyAttacks[recipient].gameObject.SetActive(true);
            enemyAttacks[recipient].SetBool(booleanName, true);
            yield return new WaitForSecondsRealtime(waitTime);
            enemyAttacks[recipient].SetBool(booleanName, false);
            enemyAttacks[recipient].gameObject.SetActive(false);
        }
        else
        {
            playerAttacks[recipient].gameObject.SetActive(true);
            playerAttacks[recipient].SetBool(booleanName, true); 
            yield return new WaitForSecondsRealtime(waitTime);
            playerAttacks[recipient].SetBool(booleanName, false);
            playerAttacks[recipient].gameObject.SetActive(false);


        }
    }

}
public enum AttackAnim
{
    None,
    Heal,
    Slash,
    Bash,
    MagicDart,
    Miss,
    Block,
    EnemyAttack,
    AttackBoost
}
