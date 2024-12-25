using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChainAttack2State : PlayerState
{
    protected D_PlayerChainAttack2State stateData;
    public PlayerChainAttack2State(PlayerEntity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerChainAttack2State stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.DisableMovement();
        entity.GetClosestEnemy();

        ScoreManager.instance.SpawnComboPrefab("Combo x2");
    }

    public override void Exit()
    {
        base.Exit();
      
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.chainAttackTime)
        {
            entity.stateMachine.ChangeState(entity.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
