using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRobot_IntroState : EnemyIntroState
{
    private SphereRobot enemy;
    public SphereRobot_IntroState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyIntroState stateData
        , SphereRobot enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (Time.time >= startTime + stateData.introTime)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
