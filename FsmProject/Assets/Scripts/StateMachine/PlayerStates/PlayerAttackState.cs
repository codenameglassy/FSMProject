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
        entity.GetClosestEnemy();

        AudioManagerCS.instance.PlayWithRandomPitch("playerAttack", 0.8f, 1.4f);


    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.chainAttackWindowTime && Input.GetKeyDown(KeyCode.J))
        {
            if (!entity.IsEnemyInRange())
            {
                return;
            }
            entity.stateMachine.ChangeState(entity.chainAttack1State);
        }

        if (Time.time >= startTime + stateData.attackTime)
        {
            entity.stateMachine.ChangeState(entity.idleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
