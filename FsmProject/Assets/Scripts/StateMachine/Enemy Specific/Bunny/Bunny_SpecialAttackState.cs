using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bunny_SpecialAttackState : EnemyAttackState
{
    private Bunny enemy;
    public Bunny_SpecialAttackState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyAttackState stateData, Bunny enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.AddStunCount();
        enemy.PerformSpecialAttack();
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
            enemy.stateMachine.ChangeState(enemy.moveState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
