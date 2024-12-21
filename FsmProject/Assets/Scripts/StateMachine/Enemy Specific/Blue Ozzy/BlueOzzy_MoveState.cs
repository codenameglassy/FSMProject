using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzy_MoveState : EnemyMoveState
{
    private BlueOzzy enemy;
    public BlueOzzy_MoveState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, BlueOzzy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
