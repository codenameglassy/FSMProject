using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzy_StunState : EnemyStunState
{
    private BlueOzzy enemy;
    public BlueOzzy_StunState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyStunState stateData, BlueOzzy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        GameControl.instance.TriggerHitstop(.4f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.stunTime)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
