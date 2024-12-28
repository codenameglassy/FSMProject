using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny_MoveState : EnemyMoveState
{
    private Bunny enemy;

    public Bunny_MoveState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, Bunny enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.SetDestination(enemy.target);

        if (enemy.IsCheckPlayerInMinRange())
        {
            /*if (enemy.StunCount() == 3)
            {
                enemy.stateMachine.ChangeState(enemy.specialAttackState);
                return;
            }*/

            enemy.stateMachine.ChangeState(enemy.attackState);
            //enemy.stateMachine.ChangeState(enemy.specialAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
