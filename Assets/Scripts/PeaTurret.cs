using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaTurret : MonoBehaviour
{
    [SerializeField] private Transform barrelPosition;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private GameObject projectile;
    
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float attackRange;

    private bool playerInAttackRange;
    private bool alreadyAttacked;

    void OnCollisionEnter2D(Collision2D collision)
    {
        BlowUp();
    }

    void Update()
    {
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);
        if (playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            Instantiate(projectile, barrelPosition.position, Quaternion.identity);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void BlowUp()
    {
        Destroy(gameObject, .2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
