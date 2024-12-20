using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerState
{
    protected D_PlayerShootState stateData;
    public PlayerShootState(PlayerEntity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerShootState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.DisableMovement();
        entity.blazingCannon.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        entity.blazingCannon.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time <= startTime + stateData.shootTime)
        {
            return;
        }

        if (Input.GetKey(KeyCode.K))
        {
            // do nothing
        }
        else
        {
            stateMachine.ChangeState(entity.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
