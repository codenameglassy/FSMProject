using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny_IdleState : EnemyIdleState
{
    private Bunny enemy;
    public Bunny_IdleState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, Bunny enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.StopMovement();
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
            enemy.stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
