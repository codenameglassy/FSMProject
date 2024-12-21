using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damageAmt);
    void SpawnVfx(Vector3 spawnPos, GameObject vfxPrefab);

    void Hurt();
    void Die();
}
