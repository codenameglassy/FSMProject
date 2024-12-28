using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialState : PlayerState
{
    protected D_PlayerSpecialState stateData;
    public PlayerSpecialState(PlayerEntity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerSpecialState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.DisableMovement();
        
       
        //entity.GetClosestEnemy();
        //entity.FaceClose();
    }

  

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
       
        if(Time.time >= startTime + stateData.specialTime)
        {
            entity.stateMachine.ChangeState(entity.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
