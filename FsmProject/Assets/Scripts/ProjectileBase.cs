using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour, IProjectile
{

    [Header("Components")]
    [SerializeField] private float damageAmount;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float checkRadius;
    [SerializeField] private GameObject explosionVfx;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (IsTouchingEnemy())
        {
            //Instantiate(explosionVfx, transform.position, Quaternion.identity);
            PerformAttack();
            Instantiate(explosionVfx, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void PerformAttack()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, checkRadius, whatIsEnemy);

        if(enemies.Length >= 1)
        {
            foreach (Collider enemy in enemies)
            {
                IDamageable enemyHp = enemy.GetComponent<IDamageable>();
                if(enemyHp != null)
                {
                    enemyHp.TakeDamage(damageAmount);
                }
                
            }
        }
    }

    public bool IsTouchingEnemy()
    {
        bool isTouchingEnemy = Physics.CheckSphere(transform.position, checkRadius, whatIsEnemy);

        return isTouchingEnemy;
    }

    public void Launch(Vector3 direction, float speed)
    {
        rb.velocity = direction.normalized * speed;
    }

    public void OnHitTarget(GameObject target)
    {
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
