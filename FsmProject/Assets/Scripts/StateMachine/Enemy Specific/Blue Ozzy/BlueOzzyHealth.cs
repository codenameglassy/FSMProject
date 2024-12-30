using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlueOzzyHealth : HealthBase
{
    private BlueOzzy enemy;

    [Header("Dead Vfx")]
    [SerializeField] private Transform deadVfxPos;
    [SerializeField] private GameObject deadVfx;
    [SerializeField] private Quaternion deadVfxRot = Quaternion.Euler(0, 0, 0);

   

    public override void Start()
    {
        base.Start();
        enemy = GetComponent<BlueOzzy>();
        GameControl.instance.AddEnemy(transform);

    }

    public override void Die()
    {
        enemy.knockBackSequence.Kill();
        GameControl.instance.RemoveEnemy(transform);
        Instantiate(deadVfx, deadVfxPos.position, deadVfxRot);
        ScoreManager.instance.AddScore(100);
        base.Die();
    }

    public override void Hurt()
    {
        base.Hurt();

        enemy.stateMachine.ChangeState(enemy.hurtState);
        ScoreManager.instance.AddScore(10);
      
    }

    public override void TakeDamage(float damageAmt)
    {
        base.TakeDamage(damageAmt);
    }
}
