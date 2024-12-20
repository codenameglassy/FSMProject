using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerIdleState : PlayerState
{
    protected D_PlayerIdleState stateData;
    public PlayerIdleState(PlayerEntity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerIdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.EnableMovement();
        entity.thirdPersonController.SetAnimSpeedFloat(0);
        entity.blazingCannon.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.attackTime)
        {
            entity.HandleAttackInput();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
