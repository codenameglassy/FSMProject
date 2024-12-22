using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRobot : EnemyEntity
{
    public SphereRobot_IntroState introState { get; private set; }
    public SphereRobot_IdleState idleState { get; private set; }
    public SphereRobot_MoveState moveState { get; private set; }

    [SerializeField] private D_EnemyMoveState moveStateData;
    [SerializeField] private D_EnemyIdleState idleStateData;
    [SerializeField] private D_EnemyIntroState introStateData;

    public override void Start()
    {
        base.Start();

        introState = new SphereRobot_IntroState(this, stateMachine, "intro", introStateData, this);
        idleState = new SphereRobot_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new SphereRobot_MoveState(this, stateMachine, "move", moveStateData, this);

        stateMachine.Initialize(introState);
    }
}
