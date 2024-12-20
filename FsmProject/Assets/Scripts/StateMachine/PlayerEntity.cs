using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using DG.Tweening;

public class PlayerEntity : MonoBehaviour
{
    public D_PlayerEntity playerEntityData;
    public FiniteStateMachine stateMachine;
    public Animator anim { get; private set; }
    public ThirdPersonController thirdPersonController { get; private set; }
    public CharacterController characterController { get; private set; }
    public Rigidbody rb { get; private set; }

    public PlayerMoveState moveState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerShootState shootState { get; private set; }
    public PlayerDodgeState dodgeState { get; private set; }
    public PlayerRecoveringState recoveringState { get; private set; }

    [Space]
    [Header("State Data - Component")]
    [SerializeField]
    private D_PlayerIdleState idleStateData;
    [SerializeField]
    private D_PlayerMoveState moveStateData;
    [SerializeField]
    private D_PlayerAttackState attackStateData;
    [SerializeField]
    private D_PlayerShootState shootStateData;
    [SerializeField]
    private D_PlayerDodgeState dodgeStateData;
    [SerializeField]
    private D_PlayerRecoveringState recoveringStateData;


    [Space]
    [Header("Blazing Cannon - Component")]
    public GameObject blazingCannon;
    public GameObject cannonProjectilePrefab;
    public Transform launchPoint;
    public float launchSpeed;
    public float ylaunchOffset;

    [Space]
    [Header("Target Detection - Component")]
    public Transform target;

    public virtual void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();

        moveState = new PlayerMoveState(this, stateMachine, "move", moveStateData);
        idleState = new PlayerIdleState(this, stateMachine, "idle", idleStateData);
        attackState = new PlayerAttackState(this, stateMachine, "attack", attackStateData);
        shootState = new PlayerShootState(this, stateMachine, "shoot", shootStateData);
        dodgeState = new PlayerDodgeState(this, stateMachine, "dodge", dodgeStateData);
        recoveringState = new PlayerRecoveringState(this, stateMachine, "recovering", recoveringStateData);

        stateMachine.Initialize(idleState);

        EnableCharacterController();
        DisableRigidbody();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void EnableMovement()
    {
        thirdPersonController.canMove = true;
    }

    public virtual void DisableMovement()
    {
        thirdPersonController.canMove = false;
    }

    #region Attack/Shoot/Dodge
    public virtual void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            //attack
            stateMachine.ChangeState(attackState);

            //random attack int
            int randomAttackInt = Random.Range(0, 2);
            anim.SetFloat("attackInt", randomAttackInt);
            Debug.Log(randomAttackInt);
        }

        if (Input.GetKey(KeyCode.K))
        {
            if(target == null)
            {
                Debug.Log("No target to shoot!");
                return;
            }
            stateMachine.ChangeState(shootState);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(dodgeState);
        }
    }

    public void ResetAttack()
    {
        stateMachine.ChangeState(idleState);
    }

    public void ResetDodge()
    {
        stateMachine.ChangeState(idleState);
    }
    #endregion



    public void LaunchProjectile()
    {
        GameObject projectileInstance = Instantiate(cannonProjectilePrefab, launchPoint.position, launchPoint.rotation);
        IProjectile projectile = projectileInstance.GetComponent<IProjectile>();

        if (projectile != null)
        {
            //Vector3 direction = launchPoint.right; // Adjust as per your game setup

            // Offset the target position in the Y-axis
            Vector3 targetPositionWithOffset = target.position + new Vector3(0, ylaunchOffset, 0);

            // Calculate the direction towards the offset target
            Vector3 direction = (targetPositionWithOffset - launchPoint.position).normalized;

            projectile.Launch(direction, launchSpeed);
        }
    }


    public void FaceThis(Vector3 target)
    {
        Vector3 target_ = new Vector3(target.x, target.y, target.z);
        Quaternion lookAtRotation = Quaternion.LookRotation(target_ - transform.position);
        lookAtRotation.x = 0;
        lookAtRotation.z = 0;
        transform.DOLocalRotateQuaternion(lookAtRotation, 0.2f);
    }

    #region CharacterController -- Rigidbody
    public void DisableCharacterController()
    {
        characterController.enabled = false;
    }

    public void EnableCharacterController()
    {
        characterController.enabled = true;
    }

    public void DisableRigidbody()
    {
        // Disable the Rigidbody
        rb.isKinematic = true; // Make sure Rigidbody reacts to physics
        rb.useGravity = false; // Optional: enable gravity if needed
    }

    public void EnableRigidbody()
    {
        // Enable the Rigidbody
        rb.isKinematic = false; // Make sure Rigidbody reacts to physics
        rb.useGravity = false; // Optional: enable gravity if needed
    }
    #endregion

    #region Dash

    private bool isDashing = false; // Whether the player is currently dashing

    public void DashBack()
    {
        StartCoroutine(BackwardsDash());
    }
    
    private IEnumerator BackwardsDash()
    {
        isDashing = true;
        Vector3 dashDirection = -transform.forward; // Get the opposite of the forward direction
        rb.AddForce(dashDirection * dodgeStateData.dashForce, ForceMode.Impulse); // Apply a force in the opposite direction

        yield return new WaitForSeconds(dodgeStateData.dashDuration); // Wait for the dash duration

        isDashing = false;
    }
    #endregion
}
