using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SphereRobotHealth : HealthBase
{
    [Space]
    [Header("Explosion Vfx")]
    [SerializeField] private GameObject explosionVfx;
    [SerializeField] private Transform explosionVfxSpawnPos;

    private bool isExploding = false;
    private SphereRobot enemy;
    public override void Start()
    {
        base.Start();
        enemy = GetComponent<SphereRobot>();
        GameControl.instance.AddEnemy(transform);
    }
    public override void TakeDamage(float damageAmt)
    {
        enemy.ApplyKnockback(enemy.target.position);
        base.TakeDamage(damageAmt);
    }

    public override void Hurt()
    {
        base.Hurt();
        
        
    }

    public override void Die()
    {
        enemy.stateMachine.ChangeState(enemy.hurtState);
    }

    public void Explode()
    {
        StartCoroutine(ExplodeRoutine());
    }

    IEnumerator ExplodeRoutine()
    {
        if (isExploding)
        {
            yield break;
        }

        isExploding = true;

        GameControl.instance.RemoveEnemy(transform);
        transform.DOPunchScale(new Vector3(.5f, .5f, .5f), .5f, 5, .5f);
        yield return new WaitForSeconds(.5f);

        CameraShake.instance.ScreenShakeFromProfile(screenShakeProfile, impulseSource);
        Instantiate(explosionVfx, explosionVfxSpawnPos.position, Quaternion.identity);
        GameControl.instance.TriggerHitstop(.2f);
        WaveSpawnerManager.instance.EnemyKilled();

        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

}
