using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOzzy : EnemyEntity
{
    public BlueOzzy_IdleState idleState { get; private set; }
    public BlueOzzy_MoveState moveState { get; private set; }

    [Header("State Data")]
    [SerializeField]
    private D_EnemyIdleState idleStateData;
    [SerializeField]
    private D_EnemyMoveState moveStateData;

    public override void Start()
    {
        base.Start();

        idleState = new BlueOzzy_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new BlueOzzy_MoveState(this, stateMachine, "move", moveStateData, this);

        stateMachine.Initialize(idleState);
    }

}
