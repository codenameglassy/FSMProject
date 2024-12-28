using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny_ChargeState : EnemyRunState
{
    private Bunny enemy;

    public Bunny_ChargeState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyRunState stateData, Bunny enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        enemy.SetDestination(enemy.target);

        if (enemy.IsCheckPlayerInMinRange())
        {
            enemy.stateMachine.ChangeState(enemy.attackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
