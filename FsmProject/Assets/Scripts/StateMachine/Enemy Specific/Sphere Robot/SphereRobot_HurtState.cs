using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRobot_HurtState : EnemyHurtState
{
    private SphereRobot enemy;
    private bool isExploding = false;

    public SphereRobot_HurtState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyHurtState stateData, SphereRobot enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        //enemy.ApplyKnockback(enemy.target.position);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExploding)
        {
            return;
        }

        if(Time.time >= startTime + stateData.hurtTime)
        {
            isExploding = true;
            enemy.GetComponent<SphereRobotHealth>().Explode();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
