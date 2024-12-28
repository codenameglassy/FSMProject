using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny_AttackState : EnemyAttackState
{
    private Bunny enemy;
    public Bunny_AttackState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyAttackState stateData, Bunny enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.SetDestination(enemy.target);
        enemy.AddStunCount();
        enemy.StopMovement();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.attackTime)
        {
            if(enemy.StunCount() == 3)
            {
                enemy.stateMachine.ChangeState(enemy.rageState);
                return;
            }
            if (enemy.StunCount() >= 4)
            {
                enemy.stateMachine.ChangeState(enemy.stunState);
                return;
            }
            enemy.stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
