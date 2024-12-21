using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyState
{
    protected D_EnemyHurtState stateData;
    public EnemyHurtState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyHurtState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.StopMovement();
    }

    public override void Exit()
    {
        base.Exit();
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
