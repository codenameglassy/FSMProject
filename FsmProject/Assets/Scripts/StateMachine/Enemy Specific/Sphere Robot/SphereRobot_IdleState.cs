using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRobot_IdleState : EnemyIdleState
{
    private SphereRobot enemy;
    public SphereRobot_IdleState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, SphereRobot enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if(Time.time >= startTime + stateData.idleTime)
        {
            enemy.stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
