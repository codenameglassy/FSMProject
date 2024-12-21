using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
public class HealthBase : MonoBehaviour, IDamageable
{
    [Header("Health")]
    public float maxHp;
    private float currentHp;
   

    private void Start()
    {
        currentHp = maxHp;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public virtual void TakeDamage(float damageAmt)
    {
        currentHp -= damageAmt;

        if (currentHp <= 0)
        {
            Die();
            return;
        }

        Hurt();
    }

    public virtual void Hurt()
    {
        Debug.Log(gameObject.name + " taken damage.");
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject);
    }

   

    public void SpawnVfx(Vector3 spawnPos, GameObject vfxPrefab)
    {
        // throw new System.NotImplementedException();
        Instantiate(vfxPrefab, spawnPos, Quaternion.identity);
    }

    public float knockbackDistance = 2f; // Distance to knock the enemy back
    public float knockbackDuration = 0.5f; // Duration of the knockback effect
    private NavMeshAgent navMeshAgent;


    public void ApplyKnockback(Vector3 attackerPosition)
    {
        // Stop the NavMeshAgent
        navMeshAgent.isStopped = true;

        // Calculate the knockback direction (away from the attacker)
        Vector3 knockbackDirection = (transform.position - attackerPosition).normalized;

        // Calculate the knockback target position
        Vector3 knockbackTarget = transform.position + knockbackDirection * knockbackDistance;
        knockbackTarget.y = transform.position.y;

        // Apply DOTween animation
        transform.DOMove(knockbackTarget, knockbackDuration)
            .SetEase(Ease.OutQuad) // Smooth easing
            .OnComplete(() =>
            {
                // Re-enable the NavMeshAgent after knockback
                if(navMeshAgent != null)
                    navMeshAgent.isStopped = false;
            });
    }
}
