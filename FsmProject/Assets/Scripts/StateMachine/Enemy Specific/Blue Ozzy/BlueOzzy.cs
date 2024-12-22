using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzy : EnemyEntity
{
    public BlueOzzy_IdleState idleState { get; private set; }
    public BlueOzzy_MoveState moveState { get; private set; }
    public BlueOzzy_AttackState attackState { get; private set; }
    public BlueOzzy_HurtState hurtState { get; private set; }
    public BlueOzzy_RunState runState { get; private set; }
    public BlueOzzy_StunState stunState { get; private set; }

    [Header("State Data")]
    [SerializeField]
    private D_EnemyIdleState idleStateData;
    [SerializeField]
    private D_EnemyMoveState moveStateData;
    [SerializeField]
    private D_EnemyAttackState attackStateData;
    [SerializeField]
    private D_EnemyHurtState hurtStateData;
    [SerializeField]
    private D_EnemyRunState runStateData;
    [SerializeField]
    private D_EnemyStunState stunStateData;

    int stunCount = 0;

    public override void Start()
    {
        base.Start();

        idleState = new BlueOzzy_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new BlueOzzy_MoveState(this, stateMachine, "move", moveStateData, this);
        attackState = new BlueOzzy_AttackState(this, stateMachine, "attack", attackStateData, this);
        hurtState = new BlueOzzy_HurtState(this, stateMachine, "hurt", hurtStateData, this);
        runState = new BlueOzzy_RunState(this, stateMachine, "run", runStateData, this);
        stunState = new BlueOzzy_StunState(this, stateMachine, "stun", stunStateData, this);

        stateMachine.Initialize(idleState);
    }

    public void StunHit()
    {
        stunCount++;
    }
    public bool Stun()
    {
       
        if(stunCount >= 3)
        {
            stunCount = 0;
            return true;
        }
        return false;
    }

}
