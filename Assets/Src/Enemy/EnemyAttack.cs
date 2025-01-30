using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage = 10f;
    public float enragedAttackDamage = 20f;

    public Vector3 attackOffset =new Vector3(-0.6f,0,0);
    public float attackRange = 0.25f;
    public LayerMask attackMask;

    public void Punch()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        
        Collider[] hitEnemies = Physics.OverlapSphere(pos, attackRange, attackMask);
        
        foreach (Collider hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy);
            
            if (hitEnemy != null)
            {
                hitEnemy.GetComponent<PlayerManager>().TakeDamage(attackDamage);

                
            }
            
        }
    }

    public void Kick()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider[] hitEnemies = Physics.OverlapSphere(pos, attackRange, attackMask);

        foreach (Collider hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy);
            
            if (hitEnemy != null)
            {
                hitEnemy.GetComponent<PlayerManager>().TakeDamage(attackDamage);


            }

        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
