using UnityEngine;
using System;
using static PlatformerMovement;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.Rendering;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;


/// <summary>
/// Hey!
/// Tarodev here. I built this controller as there was a severe lack of quality & free 2D controllers out there.
/// I have a premium version on Patreon, which has every feature you'd expect from a polished controller. Link: https://www.patreon.com/tarodev
/// You can play and compete for best times here: https://tarodev.itch.io/extended-ultimate-2d-controller
/// If you hve any questions or would like to brag about your score, come to discord: https://discord.gg/tarodev
/// 
/// Original MIT License: https://github.com/Matthew-J-Spencer/Ultimate-2D-Controller/blob/main/LICENSE
/// 
/// Modified by Jan £yczba
/// </summary>



[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]


public class PlatformerMovement : MonoBehaviour, IPlayerController
{
    //Components
    [SerializeField] private ScriptableStats stats;
    private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D oponenntRb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer characterSprite;
    private CircleCollider2D col;
    public Vector2 Inputs => _inputs.Move;
    private Vector2 frameVelocity;
    private bool cachedQueryStartInColliders;

    [SerializeField] private bool FlipAtStart;

    
    
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;

    
    private float time;



    private GameInputs _inputs;
    private IInputSource _inputSource;

    public void SetInputSource(IInputSource src) => _inputSource = src;
    public ScriptableStats GetStats() => stats;



    //Load components at a start
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        cachedQueryStartInColliders = Physics2D.queriesStartInColliders;

        _inputSource = new KeyboardInputSource(
            stats.JumpKey, stats.CrouchKey, stats.MoveLeftKey, stats.MoveRightKey
        );
    }


    private void Start()
    {
        characterSprite.flipX = FlipAtStart;
    }

    //Updates every frame
    private void Update()
    {
        time += Time.deltaTime;
        _inputs = _inputSource.Read(time, stats.SnapInput, stats.HorizontalDeadZoneThreshold, stats.VerticalDeadZoneThreshold);

        if (_inputs.JumpDown)
        {
            jumpToConsume = true;
            timeJumpWasPressed = time;
        }

        if (grounded) animator.SetBool("isAirborn", false);
        else animator.SetBool("isAirborn", true);
    }


    

    private void FixedUpdate()
    {
        CheckCollisions();
        HandleJump();
        HandleDirection();
        HandleGravity();
        ApplyMovement();

    }

    //Collisions
    private float frameLeftGrounded = float.MinValue;
    private bool grounded;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = Physics2D.CircleCast(col.bounds.center, col.radius, Vector2.down, stats.GrounderDistance, ~stats.PlayerLayer);
        bool ceilingHit = Physics2D.CircleCast(col.bounds.center, col.radius, Vector2.up, stats.GrounderDistance, ~stats.PlayerLayer);


        // Hit a Ceiling
        if (ceilingHit) frameVelocity.y = Mathf.Min(0, frameVelocity.y);

        // Landed on the Ground
        if (!grounded && groundHit)
        {
            grounded = true;
            coyoteUsable = true;
            bufferedJumpUsable = true;
            endedJumpEarly = false;
            GroundedChanged?.Invoke(true, Mathf.Abs(frameVelocity.y));
        }
        // Left the Ground
        else if (grounded && !groundHit)
        {
            grounded = false;
            frameLeftGrounded = time;
            GroundedChanged?.Invoke(false, 0);
        }

        Physics2D.queriesStartInColliders = cachedQueryStartInColliders;
    }

    //Jump
    private bool jumpToConsume;
    private bool bufferedJumpUsable;
    private bool endedJumpEarly;
    private bool coyoteUsable;
    private float timeJumpWasPressed;

    private bool HasBufferedJump => bufferedJumpUsable && time < timeJumpWasPressed + stats.JumpBuffer;
    private bool CanUseCoyote => coyoteUsable && !grounded && time < frameLeftGrounded + stats.CoyoteTime;

    private void HandleJump()
    {
        if (!endedJumpEarly && !grounded && !_inputs.JumpHeld && rb.linearVelocity.y > 0)
            endedJumpEarly = true;

        if (!jumpToConsume && !HasBufferedJump) return;

        if (grounded || CanUseCoyote) ExecuteJump();

        jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        endedJumpEarly = false;
        timeJumpWasPressed = 0;
        bufferedJumpUsable = false;
        coyoteUsable = false;
        frameVelocity.y = stats.JumpPower;
        Jumped?.Invoke();
    }


    //Horizontal movemeent
    private void HandleDirection()
    {

        if (_inputs.Move.x == 0)
        {
            animator.SetBool("isWalking", false);
            float deceleration = grounded ? stats.GroundDeceleration : stats.AirDeceleration;
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else 
        {
            animator.SetBool("isWalking", true);
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, _inputs.Move.x * stats.MaxSpeed, stats.Acceleration * Time.fixedDeltaTime);
            Vector3 scale = characterSprite.transform.localScale;
            if (frameVelocity.x > 0)
            {
                characterSprite.flipX = false;
                //scale.x = Mathf.Abs(scale.x);
            }
            else
            {
                characterSprite.flipX = true;
                //scale.x = -Mathf.Abs(scale.x);
            }
            characterSprite.transform.localScale = scale;
        }
        
    }

    //Gravity
    private void HandleGravity()
    {
        if (inKnockback)
        {
            frameVelocity.y = Mathf.Max(frameVelocity.y, -stats.MaxFallSpeed * 0.5f);
        }
        else if (grounded && frameVelocity.y <= 0f)
        {
            frameVelocity.y = stats.GroundingForce;
        }
        else
        {
            float inAirGravity = stats.FallAcceleration;
            if (endedJumpEarly && frameVelocity.y > 0) inAirGravity *= stats.JumpEndEarlyGravityModifier;
            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }


    //knockback
    private float knockbackEndTime;
    private bool inKnockback => time < knockbackEndTime;


    public void HandleKnockback()
    {
        knockbackEndTime = time + stats.KnockbackTime;
        Vector2 direction = (rb.position - oponenntRb.position).normalized;
        float knockbackPower = grounded ? stats.KnockbackPower : stats.KnockbackPower * stats.AirKnockbackMulti;
        frameVelocity = direction * knockbackPower;


    }



    //ApplyMovement
    private void ApplyMovement()
    {
        rb.linearVelocity = frameVelocity;
    }




    private void OnValidate()
    {
        if (stats == null) Debug.LogWarning("No stats assigned to the Player Controller.", this);
    }

} 