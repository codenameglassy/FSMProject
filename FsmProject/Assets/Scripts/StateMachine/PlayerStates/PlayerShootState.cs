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
        entity.GetClosestEnemy();

        if (entity.IsEnemyClose())
        {
            
            entity.stateMachine.ChangeState(entity.dodgeState);
            return;
        }
        entity.blazingCannon.SetActive(true);
        entity.FaceThis(entity.target.position);
    }

    public override void Exit()
    {
        base.Exit();
      
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
