using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecoveringState : PlayerState
{
    protected D_PlayerRecoveringState stateData;
    public PlayerRecoveringState(PlayerEntity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerRecoveringState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.DisableMovement();
    }

    public override void Exit()
    {
        base.Exit();
      
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.recoveringTime)
        {
            entity.stateMachine.ChangeState(entity.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
