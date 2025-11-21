using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")] 
        public PlayerMovementStats moveStats;
        [SerializeField] private Collider2D feetColl;
        [SerializeField] private Collider2D bodyColl;
        
        private PlayerController playerController;
        private Rigidbody2D rb;
        private SimpleAnimator animator;

        //movement vars
        private Vector2 moveVelocity;

        //collision check vars
        private RaycastHit2D groundHit;
        private RaycastHit2D headHit;
        private bool isGrounded;
        private bool bumpedHead;

        //jump vars
        public float verticalVelocity { get; private set; }
        private bool isJumping;
        private bool isFalling;
        private int numberOfJumpsUsed;
        
        //bounce vars
        private bool isBounced;
        private float bounceForce;

        //jump buffer vars
        private float jumpBufferTimer;
        private bool jumpReleasedDuringBuffer;

        //coyote time vars
        private float coyoteTimer;
        
        //gravity invert var
        public int gravityDirection { get; private set; } = 1;

        private void Awake()
        {
            //_isFacingRight = true;
            rb = GetComponent<Rigidbody2D>();
            playerController = GetComponent<PlayerController>();
        }
        

        private void Update()
        {
            // Debug.Log("Y vel: " + rb.linearVelocity.y + " | Gravity: " + rb.gravityScale + " | Simulated: " + rb.simulated);
            // DebugLogger.Log(LogChannel.Gameplay, 
            //     $"Direction={gravityDirection}, VerticalVelocity={verticalVelocity}, Grounded={isGrounded}, Falling={isFalling}, Jumping={isJumping}", 
            //     LogLevel.Info);
            CountTimers();
            JumpChecks();
        }

        private void FixedUpdate()
        {
            CollisionChecks();
            Jump();
            if (isGrounded)
            {
                Move(moveStats.GroundAcceleration, moveStats.GroundDeceleration, InputManager.Movement);
            }
            else
            {
                Move(moveStats.AirAcceleration, moveStats.AirDeceleration, InputManager.Movement);
                
            }
        }

        #region Movement
        
        private void Move(float acceleration, float deceleration, Vector2 moveInput)
        {
            if (moveInput != Vector2.zero)
            {
                Vector2 targetVelocity;

                // Determine base speed
                float baseSpeed = InputManager.RunIsHeld ? moveStats.MaxRunSpeed : moveStats.MaxWalkSpeed;

                // 🔹 Reduce control while in the air
                if (!isGrounded)
                {
                    baseSpeed *= 0.6f;               // 60% of normal speed while airborne
                    acceleration *= 0.5f;            // slower acceleration for smoother control
                }

                targetVelocity = new Vector2(moveInput.x * baseSpeed, rb.linearVelocity.y);

                moveVelocity = Vector2.Lerp(moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

                // 🔹 Clamp max horizontal velocity while in air
                if (!isGrounded)
                {
                    float maxAirSpeed = moveStats.MaxAirSpeed; // add to your MoveStats (e.g., 6f)
                    moveVelocity.x = Mathf.Clamp(moveVelocity.x, -maxAirSpeed, maxAirSpeed);
                }

                rb.linearVelocity = new Vector2(moveVelocity.x, rb.linearVelocity.y);
            }
            else
            {
                moveVelocity = Vector2.Lerp(moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
                rb.linearVelocity = new Vector2(moveVelocity.x, rb.linearVelocity.y);
            }
        }


        #endregion

        #region Jump

        private void JumpChecks()
        {
            // don't jump if player is frozen, can still fall
            if (playerController.isFrozen)
            {
                isJumping = false;
            }
            //when we press the jump button
            if (InputManager.JumpWasPressed)
            {
                jumpBufferTimer = moveStats.JumpBufferTime;
            }
            // don't jump if player is frozen, can still fall
            if (playerController.isFrozen)
            {
                isJumping = false;
                return;
            }

            //initiate jump with jump buffering and coyote time
            if (jumpBufferTimer > 0f && !isJumping && (isGrounded || coyoteTimer > 0f))
            {
                InitiateJump(1);
                
            }

            //double jump
            else if (jumpBufferTimer > 0f && isJumping && numberOfJumpsUsed < moveStats.NumberOfJumpsAllowed)
            {
                InitiateJump(1);
            }
            
            //air jump after coyote time lapsed
            // prevents two jumps after falling from ledge
            else if (jumpBufferTimer > 0f && isFalling && numberOfJumpsUsed < moveStats.NumberOfJumpsAllowed - 1)
            {
                InitiateJump(2);
            }

            //landed
            // might need to be fixed with invert gravity?
            if ((isJumping || isFalling) && isGrounded && ((verticalVelocity <= 0f && gravityDirection == 1) || (verticalVelocity > 0f && gravityDirection == -1)))
            {
                isJumping = false;
                isFalling = false;
                
                numberOfJumpsUsed = 0;
            
                // default gravity value based on physics system
                verticalVelocity = moveStats.Gravity * gravityDirection;
            }
            
            
        }

        private void InitiateJump(int numberOfJumpsUsed)
        {
            if (!isJumping)
            {
                isJumping = true;
            }

            jumpBufferTimer = 0f;
            this.numberOfJumpsUsed += numberOfJumpsUsed;
            verticalVelocity = moveStats.InitialJumpVelocity * gravityDirection;
        }

        private void Jump()
        {
            // Debug.Log($"VelY: {VerticalVelocity}, Gravity: {MoveStats.Gravity}, Grounded: {_isGrounded}, Jumping: {_isJumping}");
            // Apply bounce logic first
            if (isBounced)
            {
                // Let gravity start affecting bounce immediately
                verticalVelocity += moveStats.Gravity * gravityDirection * Time.fixedDeltaTime * bounceForce;

                // When bounce slows down, stop bouncing
                if (verticalVelocity <= 0f)
                {
                    if (!isFalling)
                    {
                        isFalling = true;
                    }
                    isBounced = false;
                }

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalVelocity);
                return; // skip rest of Jump() this frame
            }
            //apply gravity while jumping
            if (isJumping)
            {
                // when upward velocity runs out, stop being "jumping"
                if (verticalVelocity <= 0f)
                {
                    isFalling = true;
                }
                
                //check for head bump
                if (bumpedHead)
                {
                    isFalling = true;
                }
                
                if (verticalVelocity >= 0f)
                {
                        verticalVelocity += moveStats.Gravity * gravityDirection * Time.fixedDeltaTime;
                }
                
                //gravity on descending
                else if (verticalVelocity <= 0f)
                {
                    if (!isFalling)
                    {
                        isFalling = true;
                    }
                }
            }
            
            //normal gravity while falling
            if (!isGrounded && isFalling)
            {
                verticalVelocity += moveStats.Gravity * gravityDirection * Time.fixedDeltaTime;
            }
            
            // Clamp fall speed depending on gravity direction
            if (gravityDirection == 1)
            {
                // Normal gravity: fall downward (negative Y)
                verticalVelocity = Mathf.Clamp(verticalVelocity, -moveStats.MaxFallSpeed, 50f);
            }
            else
            {
                // Inverted gravity: fall upward (positive Y)
                verticalVelocity = Mathf.Clamp(verticalVelocity, -50f, moveStats.MaxFallSpeed);
            }
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalVelocity); 
        }
        
        #endregion
        
        #region Collision Checks

        private void IsGrounded()
        {
            // Pick origin based on gravity direction
            float yOrigin = (gravityDirection == 1) 
                ? feetColl.bounds.min.y      // bottom for normal gravity
                : feetColl.bounds.max.y;     // top for inverted gravity
    
            Vector2 boxCastOrigin = new Vector2(feetColl.bounds.center.x, yOrigin);
            Vector2 boxCastSize = new Vector2(bodyColl.bounds.size.x, moveStats.GroundDetectionRayLength);

            // Cast in direction of "gravity down"
            Vector2 castDir = (gravityDirection == 1) ? Vector2.down : Vector2.up;

            groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, castDir, moveStats.GroundDetectionRayLength, moveStats.GroundLayer);
            
            if (groundHit.collider != null)
            {
                isGrounded = true;
            }
            else { isGrounded = false; }
            
            #region Debug Visualization

            if (moveStats.DebugShowIsGroundedBox)
            {
                Color rayColor;
                if (isGrounded)
                {
                    rayColor = Color.green;
                }
                else {rayColor = Color.red;}
                
                Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * moveStats.GroundDetectionRayLength, rayColor);
                Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * moveStats.GroundDetectionRayLength, rayColor);
                Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - moveStats.GroundDetectionRayLength), Vector2.right * boxCastSize.x, rayColor);
            }
            
            #endregion
            
        }

        private void BumpedHead()
        {
            // Opposite of grounded: check "against" gravity
            float yOrigin = (gravityDirection == 1)
                ? bodyColl.bounds.max.y      // top for normal gravity
                : feetColl.bounds.min.y;     // bottom for inverted gravity

            Vector2 boxCastOrigin = new Vector2(feetColl.bounds.center.x, yOrigin);
            Vector2 boxCastSize = new Vector2(bodyColl.bounds.size.x * moveStats.HeadWidth, moveStats.HeadDetectionRayLength);

            // Cast opposite gravity
            Vector2 castDir = (gravityDirection == 1) ? Vector2.up : Vector2.down;

            headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, castDir, moveStats.HeadDetectionRayLength, moveStats.GroundLayer);
            
            if (headHit.collider != null)
            {
                bumpedHead = true;
            }
            else { bumpedHead = false; }
            
            #region Debug Visualization

            if (moveStats.DebugShowHeadBumpBox)
            {
                float headWidth = moveStats.HeadWidth;
                
                Color rayColor;
                if (bumpedHead)
                {
                    rayColor = Color.green;
                }
                else {rayColor = Color.red;}
                
                Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y), Vector2.up * moveStats.HeadDetectionRayLength, rayColor);
                Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x / 2 * headWidth, boxCastOrigin.y), Vector2.up * moveStats.HeadDetectionRayLength, rayColor);
                Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y + moveStats.HeadDetectionRayLength), Vector2.right * boxCastSize.x * headWidth, rayColor);
            }
            
            #endregion
        }
        private void CollisionChecks()
        {
            IsGrounded();
            BumpedHead();
        }
        
        #endregion
        
        #region Timers

        private void CountTimers()
        {
            jumpBufferTimer -= Time.deltaTime;

            if (!isGrounded)
            {
                coyoteTimer -= Time.deltaTime;
            }
            else
            {
                coyoteTimer = moveStats.JumpCoyoteTime; 
            }
        }
        
        #endregion

        public void FlipGravity()
        {
            gravityDirection *= -1;
            transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
            
            // always unground and zero vertical velocity
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            isFalling = true;
        }

        public void ResetMovement()
        {
            gravityDirection = 1;
            transform.rotation = Quaternion.identity;

            // Core physics reset 
            rb.linearVelocity = Vector2.zero;
            verticalVelocity = moveStats.Gravity * gravityDirection;

            // Movement state reset
            isJumping = false;
            isFalling = false;
            isBounced = false;
            numberOfJumpsUsed = 0;
            jumpBufferTimer = 0f;
            coyoteTimer = 0f;

            // Force grounded reset if you spawn on a platform
            isGrounded = true;

            DebugLogger.Log(LogChannel.Gameplay, 
                $"Gravity reset. Direction={gravityDirection}, VerticalVelocity={verticalVelocity}, " +
                $"Grounded={isGrounded}, Jumping={isJumping}", 
                LogLevel.Info);
        }
        
        public void Bounce(float bounceStrength)
        {
            // Start bounce with upward velocity
            verticalVelocity = bounceStrength * -gravityDirection;
            bounceForce = bounceStrength * -gravityDirection;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalVelocity);
            isBounced = true;
            isJumping = false; // Not a normal jump
            isFalling = false;
            isGrounded = false;
            
            InitiateJump(1); // trigger jump state immediately
            StartCoroutine(BounceCooldown());
        }

        private IEnumerator BounceCooldown()
        {
            yield return new WaitForSeconds(.05f);
            isBounced = false;
        }
    }
}
