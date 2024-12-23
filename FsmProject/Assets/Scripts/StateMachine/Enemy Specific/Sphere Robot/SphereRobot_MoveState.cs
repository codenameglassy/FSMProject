using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRobot_MoveState : EnemyMoveState
{
    private SphereRobot enemy;
    public SphereRobot_MoveState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, SphereRobot enemy) : base(entity, stateMachine, animBoolName, stateData)
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


        //explode if near player.

        if (enemy.IsCheckPlayerInMinRange())
        {
            enemy.stateMachine.ChangeState(enemy.hurtState);
            //enemy.GetComponent<IDamageable>().Die();
            //enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
