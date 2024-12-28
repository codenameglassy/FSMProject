using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerState
{
   
    public D_PlayerDodgeState stateData;

    
    public PlayerDodgeState(PlayerEntity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.DisableMovement();
        entity.GetClosestEnemy();
        entity.DashBack();
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
