using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttackVictimState : PlayerState
{
    protected D_PlayerRecoveringState stateData;
    bool isFinished = false;
    public PlayerSpecialAttackVictimState(PlayerEntity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerRecoveringState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.DisableMovement();
        //entity.FaceClose();
    }

    public override void Exit()
    {
        base.Exit();
        //entity.KnockBack();
      
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.recoveringTime)
        {
            if (isFinished)
            {
                return;
            }
            isFinished = true;
            entity.GetComponent<IDamageable>().TakeDamage(100f);
            // entity.stateMachine.ChangeState(entity.idleState);
          
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
