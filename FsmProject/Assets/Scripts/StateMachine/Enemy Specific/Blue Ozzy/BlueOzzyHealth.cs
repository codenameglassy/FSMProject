using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzyHealth : HealthBase
{
    private BlueOzzy enemy;

    public override void Start()
    {
        base.Start();
        enemy = GetComponent<BlueOzzy>();
        GameControl.instance.AddEnemy(transform);
    }

    public override void Die()
    {
        base.Die();
        GameControl.instance.RemoveEnemy(transform);
    }

    public override void Hurt()
    {
        base.Hurt();

        enemy.stateMachine.ChangeState(enemy.hurtState);
    }

    public override void TakeDamage(float damageAmt)
    {
        base.TakeDamage(damageAmt);
    }
}
