using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class EnemyEntity : MonoBehaviour
{
    public EnemyFiniteStateMachine stateMachine;
    public NavMeshAgent navmeshAgent { get; private set; }
    public Animator anim { get; private set; }
    public GameObject modelGO { get; private set; }
    public Transform target { get; private set; }

    [Space]
    [Header("Check Environment - Components")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float checkPlayerInMinRange;
    [SerializeField] private float checkPlayerInMaxRange;
    [SerializeField] private Transform checkPlayerPos;

    [Space]
    [Header("My Components")]
    [SerializeField] private D_EnemyEntity entityData;

    [Space]
    [Header("Knockback")]
    public float knockbackDistance = 2f; // Distance to knock the enemy back
    public float knockbackDuration = 0.5f; // Duration of the knockback effect


    public virtual void Start()
    {
        modelGO = transform.Find("Model").gameObject;
        navmeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        target = FindObjectOfType<PlayerEntity>().transform; //set target

        stateMachine = new EnemyFiniteStateMachine();

    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetDestination(Transform target_)
    {
        navmeshAgent.SetDestination(target_.position);
    }

    public virtual void SetTarget(Transform target_)
    {
        target = target_;
    }

    public virtual  void StopMovement()
    {
        navmeshAgent.ResetPath();
    }
    public virtual bool IsCheckPlayerInMinRange()
    {
        bool isPlayerInMinRange = Physics.CheckSphere(checkPlayerPos.position, checkPlayerInMinRange, whatIsPlayer);
        return isPlayerInMinRange;
    }

    public virtual bool IsCheckPlayerInMaxRange()
    {
        bool isPlayerInMaxRange = Physics.CheckSphere(checkPlayerPos.position, checkPlayerInMaxRange, whatIsPlayer);
        return isPlayerInMaxRange;
    }

    public virtual void SetSpeed(float speed_)
    {
        navmeshAgent.speed = speed_;
    }

    public virtual void FaceThis(Vector3 target)
    {
        Vector3 target_ = new Vector3(target.x, target.y, target.z);
        Quaternion lookAtRotation = Quaternion.LookRotation(target_ - transform.position);
        lookAtRotation.x = 0;
        lookAtRotation.z = 0;
        transform.DOLocalRotateQuaternion(lookAtRotation, 0.2f);
    }

   

    public virtual void ApplyKnockback(Vector3 attackerPosition)
    {

        // Face player
        FaceThis(target.position);

        // Stop the NavMeshAgent
        navmeshAgent.isStopped = true;

        // Calculate the knockback direction (away from the attacker)
        Vector3 knockbackDirection = (transform.position - attackerPosition).normalized;

        // Calculate the knockback target position
        Vector3 knockbackTarget = transform.position + knockbackDirection * knockbackDistance;
        knockbackTarget.y = transform.position.y;

        // Apply DOTween animation
        transform.DOMove(knockbackTarget, knockbackDuration)
            .SetEase(Ease.OutQuad) // Smooth easing
            .OnComplete(() =>
            {
                // Re-enable the NavMeshAgent after knockback
                navmeshAgent.isStopped = false;
            });
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(checkPlayerPos.position, checkPlayerInMaxRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPlayerPos.position, checkPlayerInMinRange);
    }
}
