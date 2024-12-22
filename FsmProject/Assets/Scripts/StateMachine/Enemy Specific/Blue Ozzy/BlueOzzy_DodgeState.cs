using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzy_DodgeState : EnemyDodgeState
{
    private BlueOzzy enemy;
    public BlueOzzy_DodgeState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyDodgeState stateData, BlueOzzy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.StopMovement();
        enemy.ApplyKnockback(enemy.target.position);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.dodgeTime)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
