using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzy_HurtState : EnemyHurtState
{
    private BlueOzzy enemy;
    public BlueOzzy_HurtState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyHurtState stateData, BlueOzzy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.ApplyKnockback(enemy.target.position);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.hurtTime)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
