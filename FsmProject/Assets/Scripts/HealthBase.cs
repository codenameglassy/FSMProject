using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.AI;
public class HealthBase : MonoBehaviour, IDamageable
{
    [Header("Health")]
    public float maxHp;
    private float currentHp;

    [Space]
    [Header("Camera Shake")]
    public ScreenShakeProfile screenShakeProfile;
    public CinemachineImpulseSource impulseSource;

    public virtual void Start()
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

        if (screenShakeProfile == null || impulseSource == null)
        {
            return;
        }

        CameraShake.instance.ScreenShakeFromProfile(screenShakeProfile, impulseSource);
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject);
    }

    public void SpawnVfx(Vector3 spawnPos, GameObject vfxPrefab)
    {
        Instantiate(vfxPrefab, spawnPos, Quaternion.identity);
    }

 

   
}
