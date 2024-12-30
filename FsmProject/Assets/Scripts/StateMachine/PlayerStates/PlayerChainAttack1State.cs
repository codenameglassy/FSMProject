using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChainAttack1State : PlayerState
{
    protected D_PlayerChainAttack1State stateData;
    public PlayerChainAttack1State(PlayerEntity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerChainAttack1State stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.DisableMovement();
        entity.GetClosestEnemy();

        ScoreManager.instance.SpawnComboPrefab("Combo +50");
        ScoreManager.instance.AddScore(50);
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

       /* if(Time.time >= startTime + stateData.chainAttack2WindowTime && Input.GetKeyDown(KeyCode.J))
        {
            entity.stateMachine.ChangeState(entity.chainAttack2State);
        }*/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
