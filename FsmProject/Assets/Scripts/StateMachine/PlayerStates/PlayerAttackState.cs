using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    protected D_PlayerAttackState stateData;
    public PlayerAttackState(PlayerEntity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerAttackState stateData) : base(entity, stateMachine, animBoolName)
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
       

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
