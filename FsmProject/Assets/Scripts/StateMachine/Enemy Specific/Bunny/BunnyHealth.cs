using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyHealth : EnemyHealth
{
    [Space]
    [Header("Explosion Vfx")]
    [SerializeField] private GameObject explosionVfx;
    [SerializeField] private Transform explosionVfxSpawnPos;

    private bool isExploding = false;
    private Bunny enemy;
    bool isDetonating = false;

    public override void Die()
    {
        GameControl.instance.RemoveEnemy(transform);
        ScoreManager.instance.AddScore(200);
        Instantiate(explosionVfx, explosionVfxSpawnPos.position, Quaternion.identity);
        base.Die();
        
    }

    public override void Hurt()
    {
        base.Hurt();
        ScoreManager.instance.AddScore(20);
    }

    public override void Start()
    {
        base.Start();
        enemy = GetComponent<Bunny>();
        GameControl.instance.AddEnemy(transform);
    }

    public override void TakeDamage(float damageAmt)
    {
        base.TakeDamage(damageAmt);
    }
}
