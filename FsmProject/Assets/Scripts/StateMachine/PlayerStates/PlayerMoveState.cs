using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    protected D_PlayerMoveState stateData;
    
    public PlayerMoveState(PlayerEntity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerMoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
       
    }

    public override void Enter()
    {
        base.Enter();
        entity.EnableMovement();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        entity.HandleAttackInput();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
