using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : StateMachineBehaviour
{
    Transform player;
    Rigidbody rb;
    Enemy enemy;
    public float speed = 1f;
    public float attackRange = 3f;
    public EnemyAttack att;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponentInParent<Enemy>();
        rb = animator.GetComponentInParent<Rigidbody>();
        att = animator.GetComponent<EnemyAttack>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.lookAtPlayer();

        Vector3 targetPosition = new Vector3(player.position.x, rb.position.y, player.position.z);
        Vector3 newPos = Vector3.MoveTowards(rb.position, targetPosition, Time.fixedDeltaTime * speed);

        rb.MovePosition(newPos);

        if (player.GetComponent<PlayerManager>().player.Health > 0)
        {
            if (Vector3.Distance(player.position, rb.position) <= attackRange)
            {
                if (enemy.Stamina < 5f)
                {
                   
                    return;
                }
                int attackType = Random.Range(0, 2);
                if (attackType == 0)
                {
                    animator.SetTrigger("Punch");
                    enemy.StaminaDecrease(5f);
                    att.Punch();
                }
                else
                {
                    animator.SetTrigger("Kick");
                    enemy.StaminaDecrease(5f);
                    att.Kick();
                }
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Punch");
        animator.ResetTrigger("Kick");
    }
}
