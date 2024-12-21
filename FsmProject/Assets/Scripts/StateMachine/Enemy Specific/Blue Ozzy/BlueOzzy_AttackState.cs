using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzy_AttackState : EnemyAttackState
{
    private BlueOzzy enemy;
    public BlueOzzy_AttackState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyAttackState stateData, BlueOzzy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        enemy.FaceThis(enemy.target.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.attackTime)
        {
            enemy.stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
