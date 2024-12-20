using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class PlayerEntity : MonoBehaviour
{
    public D_PlayerEntity playerEntityData;
    public FiniteStateMachine stateMachine;
    public Animator anim { get; private set; }
    public ThirdPersonController thirdPersonController { get; private set; }


    public PlayerMoveState moveState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerShootState shootState { get; private set; }

    [SerializeField]
    private D_PlayerIdleState idleStateData;
    [SerializeField]
    private D_PlayerMoveState moveStateData;
    [SerializeField]
    private D_PlayerAttackState attackStateData;
    [SerializeField]
    private D_PlayerShootState shootStateData;


    public virtual void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        anim = GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();

        moveState = new PlayerMoveState(this, stateMachine, "move", moveStateData);
        idleState = new PlayerIdleState(this, stateMachine, "idle", idleStateData);
        attackState = new PlayerAttackState(this, stateMachine, "attack", attackStateData);
        shootState = new PlayerShootState(this, stateMachine, "shoot", shootStateData);

        stateMachine.Initialize(idleState);
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void EnableMovement()
    {
        thirdPersonController.canMove = true;
    }

    public virtual void DisableMovement()
    {
        thirdPersonController.canMove = false;
    }

    public virtual void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            //attack
            stateMachine.ChangeState(attackState);

            //random attack int
            int randomAttackInt = Random.Range(0, 2);
            anim.SetFloat("attackInt", randomAttackInt);
            Debug.Log(randomAttackInt);
        }

        if (Input.GetKey(KeyCode.K))
        {
            stateMachine.ChangeState(shootState);
        }
    }

    public void ResetAttack()
    {
        stateMachine.ChangeState(idleState);
    }
}
