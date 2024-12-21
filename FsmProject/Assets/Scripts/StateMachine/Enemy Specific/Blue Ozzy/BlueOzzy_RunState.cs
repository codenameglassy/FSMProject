using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzy_RunState : EnemyRunState
{
    private BlueOzzy enemy;
    public BlueOzzy_RunState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyRunState stateData, BlueOzzy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (!enemy.IsCheckPlayerInMaxRange())
        {
            enemy.stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
