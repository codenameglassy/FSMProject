using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    [Header("Health")]
    public float maxHp;
    private float currentHp;

    private void Start()
    {
        currentHp = maxHp;
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
}
