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
        entity.blazingCannon.SetActive(false);
        entity.anim.SetBool("dance", true);
        //entity.thirdPersonController.SetAnimSpeedFloat(0);
    }

    public override void Exit()
    {
        base.Exit();
        entity.anim.SetBool("dance", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //entity.FocusAtMidPoint();

        if(Time.time >= startTime + stateData.idleTime)
        {
            entity.HandleAttackInput();
            if (Input.GetKeyDown(KeyCode.L))
            {
                //special
                entity.stateMachine.ChangeState(entity.specialState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
