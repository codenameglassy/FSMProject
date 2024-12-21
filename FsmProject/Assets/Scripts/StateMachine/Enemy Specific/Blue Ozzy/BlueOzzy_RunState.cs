using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzy_RunState : EnemyRunState
{
    private BlueOzzy enemy;
    private int runWay;
    private Transform runAwayPoint;
    private float runAwayTime;
    public BlueOzzy_RunState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyRunState stateData, BlueOzzy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();


        runAwayTime = Random.Range(.8f, 2f);
        int randomInt = Random.Range(0, GameControl.instance.runawayPoints.Count);
        runAwayPoint = GameControl.instance.runawayPoints[randomInt];

        runWay = randomInt;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (runWay == 1)
        {
            enemy.SetDestination(runAwayPoint);

            if(Time.time >= startTime + runAwayTime)
            {
                enemy.stateMachine.ChangeState(enemy.idleState);
            }
            return;
        }

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
