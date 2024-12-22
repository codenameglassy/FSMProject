using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using DG.Tweening;
using Cinemachine;

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
    public PlayerSpecialState specialState { get; private set; }

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
    [SerializeField] 
    private D_PlayerSpecialState specialStateData;


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
    public LayerMask whatIsEnemy;
    public float checkCloseRadius;

    [Space]
    [Header("Combat - Component")]
    public float deltaDistance;
    public float damageAmount;
    public GameObject hitVfx;
    public Transform hitSpawnPos;
    public Transform attackPoint;
    public float attackRange;
    public GameObject groundSlamVfx;

    [Space]
    [Header("Camera Shake")]
    public ScreenShakeProfile screenShakeProfile_SpecialAttack;
    public CinemachineImpulseSource impulseSource;

    public virtual void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        characterController = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();

        moveState = new PlayerMoveState(this, stateMachine, "move", moveStateData);
        idleState = new PlayerIdleState(this, stateMachine, "idle", idleStateData);
        attackState = new PlayerAttackState(this, stateMachine, "attack", attackStateData);
        shootState = new PlayerShootState(this, stateMachine, "shoot", shootStateData);
        dodgeState = new PlayerDodgeState(this, stateMachine, "dodge", dodgeStateData);
        recoveringState = new PlayerRecoveringState(this, stateMachine, "recovering", recoveringStateData);
        specialState = new PlayerSpecialState(this, stateMachine, "special", specialStateData);

        stateMachine.Initialize(idleState);

       // EnableCharacterController();
       // DisableRigidbody();
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
        thirdPersonController.SetAnimSpeedFloat(0);
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

    public void PerformAttack()
    {
        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, attackRange, whatIsEnemy);

        if (enemies.Length >= 1)
        {
            foreach (Collider enemy in enemies)
            {
                IDamageable enemyHp = enemy.GetComponent<IDamageable>();
                if (enemyHp != null)
                {
                    enemyHp.TakeDamage(damageAmount);
                    Vector3 collisionPoint = enemy.ClosestPoint(attackPoint.position);
                    enemyHp.SpawnVfx(collisionPoint, hitVfx);
                    //enemy.GetComponent<HealthBase>().ApplyKnockback(transform.position);
                   
                }

            }
        }
    }

    public void PerformSpecialAttack()
    {
        float attackRange_ = attackRange * 12f;

        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, attackRange_, whatIsEnemy);

        if (enemies.Length >= 1)
        {
            foreach (Collider enemy in enemies)
            {
                IDamageable enemyHp = enemy.GetComponent<IDamageable>();
                if (enemyHp != null)
                {
                    enemyHp.TakeDamage(damageAmount);
                    Vector3 collisionPoint = enemy.ClosestPoint(attackPoint.position);
                    enemyHp.SpawnVfx(collisionPoint, hitVfx);
                    //enemy.GetComponent<HealthBase>().ApplyKnockback(transform.position);

                }

            }
        }

        GameControl.instance.TriggerHitstop(0.2f);
    }
    public void ResetAttack()
    {
        stateMachine.ChangeState(idleState);
    }

    public void ResetDodge()
    {
        characterController.enabled = true;
        isDashing = false;
        stateMachine.ChangeState(idleState);
    }


    public void ShakeCamera_SpecialAttack()
    {
        CameraShake.instance.ScreenShakeFromProfile(screenShakeProfile_SpecialAttack, impulseSource);
        //Vector3 groundSlamVfxPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Instantiate(groundSlamVfx, transform.position, rotation);
    }
    public void SpecialAttackYFix()
    {
        characterController.enabled = false;
        Vector3 currentPos = transform.position;
        Vector3 yFixPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        transform.DOMove(yFixPos, .2f).OnComplete(() =>
        {
            transform.DOMove(currentPos, .2f).OnComplete(() => {
                characterController.enabled = true;
            });

        });
        ;
    }

    #endregion

    public bool IsEnemyClose()
    {
        bool isEnemyClose_ = Physics.CheckSphere(transform.position, checkCloseRadius, whatIsEnemy);
        return isEnemyClose_;
    }

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

    public Vector3 TargetOffset(Vector3 target, float deltaDistance)
    {
        Vector3 position;
        position = target;
        return Vector3.MoveTowards(position, transform.position, deltaDistance);
    }

    public void GetClose() // Animation Event ---- for Moving Close to Target
    {
        Vector3 getCloseTarget;
        /* if (target == null)
         {
             getCloseTarget = oldTarget.transform.position;
         }
         else
         {
             getCloseTarget = target.position;
         }*/
        getCloseTarget = target.position; 
        FaceThis(getCloseTarget);
        Vector3 finalPos = TargetOffset(getCloseTarget, deltaDistance);
        finalPos.y = 0;
        transform.DOMove(finalPos, 0.2f);
    }


    public void FaceClose()
    {
        if(target== null)
        {
            Debug.Log("No target close to face to!");
            return;
        }
        FaceThis(target.position);
    }

    public void GetClosestEnemy()
    {
        target = FindClosestTarget();
    }

    Transform FindClosestTarget()
    {
        if (GameControl.instance.enemyEntityList == null || GameControl.instance.enemyEntityList.Count == 0)
            return null;

        Transform closest = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (Transform target in GameControl.instance.enemyEntityList)
        {
            if (target == null) continue;

            Vector3 directionToTarget = target.position - playerPosition;
            float distanceSqr = directionToTarget.sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closest = target;
            }
        }

        return closest;
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

        /*isDashing = true;
        Vector3 dashDirection = -transform.forward; // Get the opposite of the forward direction
        rb.AddForce(dashDirection * dodgeStateData.dashForce, ForceMode.Impulse); // Apply a force in the opposite direction

        yield return new WaitForSeconds(dodgeStateData.dashDuration); // Wait for the dash duration

        isDashing = false;*/

        // Calculate the dash back position


        //--------------

        /* Vector3 dashDirection = -transform.forward; // Dash backwards relative to facing direction
         Vector3 targetPosition = transform.position + dashDirection * dodgeStateData.dashForce;

         // Use DOTween to move the character
         transform.DOMove(targetPosition, dodgeStateData.dashDuration).SetEase(Ease.OutQuad);

         // Wait for the dash duration
         yield return new WaitForSeconds(dodgeStateData.dashDuration);

         isDashing = false;*/

        //----------------
        FaceClose();
        yield return new WaitForSeconds(0.1f);

        isDashing = true;

        // Calculate dash direction (opposite of player's forward direction)
        Vector3 dashDirection = -transform.forward * dodgeStateData.dashForce;

        // Use DOTween to smoothly move the CharacterController
        Vector3 targetPosition = transform.position + dashDirection;

        // Temporarily disable CharacterController to use DOTween
        characterController.enabled = false;

        transform.DOMove(targetPosition, dodgeStateData.dashDuration)
            .SetEase(Ease.OutQuad) // Optional easing
            .OnComplete(() =>
            {
                // Re-enable CharacterController and stop dashing
               // characterController.enabled = true;
               // isDashing = false;
            });

        yield return new WaitForSeconds(0.2f);
        ResetDodge();
    }

   
    #endregion



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkCloseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
