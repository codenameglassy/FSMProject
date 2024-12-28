using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny_RageState : EnemyIdleState
{
    private Bunny enemy;
    public Bunny_RageState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, Bunny enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.idleTime)
        {
            enemy.stateMachine.ChangeState(enemy.chargeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
