using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny_StunState : EnemyStunState
{
    private Bunny enemy;
    public Bunny_StunState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyStunState stateData, Bunny enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.ResetStun();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.stunTime)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
