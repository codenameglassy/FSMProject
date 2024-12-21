using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEntity : MonoBehaviour
{
    public EnemyFiniteStateMachine stateMachine;
    public NavMeshAgent navmeshAgent { get; private set; }
    public Animator anim { get; private set; }
    public GameObject modelGO { get; private set; }
    public Transform target { get; private set; }

  /*  public EnemyIdleState idleState { get; private set; }
    public EnemyMoveState moveState { get; private set; }

    [Header("State Data - Components")]
    [SerializeField] private D_EnemyIdleState idleStateData;
    [SerializeField] private D_EnemyMoveState moveStateData;*/

    [Header("Check Environment - Components")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float checkPlayerInMinRange;
    [SerializeField] private float checkPlayerInMaxRange;
    [SerializeField] private Transform checkPlayerPos;


    public virtual void Start()
    {
        modelGO = transform.Find("Model").gameObject;
        navmeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        target = FindObjectOfType<PlayerEntity>().transform; //set target

        stateMachine = new EnemyFiniteStateMachine();

        /*idleState = new EnemyIdleState(this, stateMachine, "idle", idleStateData);
        moveState = new EnemyMoveState(this, stateMachine, "move", moveStateData);

        stateMachine.Initialize(idleState);*/
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(checkPlayerPos.position, checkPlayerInMaxRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPlayerPos.position, checkPlayerInMinRange);
    }
}
