using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bunny : EnemyEntity
{
   
    public Bunny_IntroState introState { get; private set; }
    public Bunny_IdleState idleState { get; private set; } 
    public Bunny_MoveState moveState { get; private set; }
    public Bunny_AttackState attackState { get; private set; }
    public Bunny_StunState stunState { get; private set; }
    public Bunny_SpecialAttackState specialAttackState { get; private set; }
    public Bunny_RageState rageState { get; private set; }
    public Bunny_ChargeState chargeState { get; private set; }

    [Header("State Data")]
    [SerializeField] private D_EnemyIntroState introStateData;
    [SerializeField] private D_EnemyIdleState idleStateData;
    [SerializeField] private D_EnemyMoveState moveStateData;
    [SerializeField] private D_EnemyAttackState attackstateData;
    [SerializeField] private D_EnemyStunState stunStateData;
    [SerializeField] private D_EnemyAttackState specialAttackStateData;
    [SerializeField] private D_EnemyRunState chargeStatData;
    [SerializeField] private D_EnemyIdleState rageStateData;

    private int stunCount = 0;

    [Header("Special Attack")]
    [SerializeField] private Transform bunnyHandPos;
    
    public override void Start()
    {
        base.Start();
        introState = new Bunny_IntroState(this, stateMachine, "intro", introStateData, this);
        idleState = new Bunny_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new Bunny_MoveState(this, stateMachine, "move", moveStateData, this);
        attackState = new Bunny_AttackState(this, stateMachine, "attack", attackstateData, this);
        stunState = new Bunny_StunState(this, stateMachine, "stun", stunStateData, this);
        specialAttackState = new Bunny_SpecialAttackState(this, stateMachine, "specialAttack", specialAttackStateData, this);
        rageState = new Bunny_RageState(this, stateMachine, "rage", rageStateData, this);
        chargeState = new Bunny_ChargeState(this, stateMachine, "charge", chargeStatData, this);

        stateMachine.Initialize(idleState);
    }

    public int StunCount()
    {
        return stunCount;
    }

    public void AddStunCount()
    {
        stunCount++;
    }

    public void ResetStun()
    {
        stunCount = 0;
    }

    bool isDancing = false;
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isDancing)
        {
            return;
        }
        if (GameControl.instance.isGameOver)
        {
            
            anim.SetBool("dance", true);
            isDancing = true;
        }
    }

    public override void SpecialAttackVictim()
    {
        PlayerEntity player = target.GetComponent<PlayerEntity>();
        player.stateMachine.ChangeState(player.specialAttackVictimState);
        player.FaceThis(transform.position);
        MovePlayerToHandPos();
    }

    public void MovePlayerToHandPos()
    {
        target.DOLocalMove(bunnyHandPos.position, .2f);
        PlayerEntity player = target.GetComponent<PlayerEntity>();
        player.stateMachine.ChangeState(player.specialAttackVictimState);
    }
}
