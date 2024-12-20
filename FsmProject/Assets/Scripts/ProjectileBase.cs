using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour, IProjectile
{

    [Header("Components")]
    [SerializeField] private Rigidbody rb;

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

    public void Launch(Vector3 direction, float speed)
    {
        rb.velocity = direction.normalized * speed;
    }

    public void OnHitTarget(GameObject target)
    {

    }
}
