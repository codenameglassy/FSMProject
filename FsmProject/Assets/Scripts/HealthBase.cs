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

    [Header("Model Render")]
    [SerializeField] private Renderer render;
    [SerializeField] private Material flashMat;
    private Material defaultMat;

    public virtual void Start()
    {
        currentHp = maxHp;
        defaultMat = render.material;
    }

    public virtual void TakeDamage(float damageAmt)
    {
        currentHp -= damageAmt;
        Hurt();

        if (currentHp <= 0)
        {
            Die();
            return;
        }

       
    }

    public virtual void Hurt()
    {
        Debug.Log(gameObject.name + " taken damage.");
        AudioManagerCS.instance.PlayWithRandomPitch("hurt", 0.7f, 1.3f);

        if (screenShakeProfile == null || impulseSource == null)
        {
            return;
        }

        CameraShake.instance.ScreenShakeFromProfile(screenShakeProfile, impulseSource);
        GameControl.instance.TriggerHitstop(.05f);
        render.material = flashMat;
        Invoke("ResetMat", .2f);
    }

    void ResetMat()
    {
        render.material = defaultMat;
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        //Destroy(gameObject);
        GameControl.instance.TriggerHitstop(.4f);
        WaveSpawnerManager.instance.EnemyKilled();
        gameObject.SetActive(false);
    }

    public void SpawnVfx(Vector3 spawnPos, GameObject vfxPrefab)
    {
        Instantiate(vfxPrefab, spawnPos, Quaternion.identity);
    }

 
    public float CurrentHp()
    {
        return currentHp;
    }
   
}
