using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthBase
{
    [Header("Health Canvas")]
    [SerializeField] private Image healthFillImage;

    [Header("Death Vfx")]
    [SerializeField] private GameObject deathVfx;
    [SerializeField] private Transform deathVfxPos;
    [SerializeField] private Quaternion deathVfxRot = Quaternion.Euler(0, 0, 0);
    public override void Die()
    {
        Instantiate(deathVfx, deathVfxPos.position, deathVfxRot);
        GameControl.instance.GameOver();
        GameControl.instance.TriggerHitstop(.4f);
        gameObject.SetActive(false);
        // base.Die();
    }

    public override void Hurt()
    {
        base.Hurt();
        
    }

    public override void Start()
    {
        base.Start();
        UpdateHealthFill();
    }

    public override void TakeDamage(float damageAmt)
    {
        base.TakeDamage(damageAmt);
        UpdateHealthFill();
    }

    void UpdateHealthFill()
    {
        healthFillImage.fillAmount = (float)CurrentHp() / maxHp;
    }
}
