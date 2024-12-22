using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzy_RangeAttackState : EnemyRangeAttackState
{
    private BlueOzzy enemy;
    public BlueOzzy_RangeAttackState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyRangeAttackState stateData, BlueOzzy enemy ) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
