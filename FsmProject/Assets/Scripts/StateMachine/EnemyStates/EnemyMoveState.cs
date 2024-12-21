using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected D_EnemyMoveState stateData;
    public EnemyMoveState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetDestination(entity.target);
    }

    public override void Exit()
    {
        base.Exit();
        entity.StopMovement();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
