using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected D_EnemyAttackState stateData;
    public EnemyAttackState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyAttackState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.StopMovement();
        AudioManagerCS.instance.PlayWithRandomPitch("enemyAttack", .2f, 3f);
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
