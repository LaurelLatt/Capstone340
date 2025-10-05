using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")] public PlayerMovementStats MoveStats;

        [SerializeField] private Collider2D _feetColl;
        [SerializeField] private Collider2D _bodyColl;

        private Rigidbody2D _rb;
        private SimpleAnimator _animator;

        //movement vars
        private Vector2 _moveVelocity;
        //private bool _isFacingRight;

        //collision check vars
        private RaycastHit2D _groundHit;
        private RaycastHit2D _headHit;
        private bool _isGrounded;
        private bool _bumpedHead;

        //jump vars
        public float VerticalVelocity { get; private set; }
        private bool _isJumping;
        private bool _isFalling;
        private int _numberOfJumpsUsed;

        //jump buffer vars
        private float _jumpBufferTimer;
        private bool _jumpReleasedDuringBuffer;

        //coyote time vars
        private float _coyoteTimer;
        
        //gravity invert var
        public int _gravityDirection { get; private set; } = 1;

        private void Awake()
        {
            //_isFacingRight = true;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CountTimers();
            JumpChecks();
        }

        private void FixedUpdate()
        {
            CollisionChecks();
            Jump();
            if (_isGrounded)
            {
                Move(MoveStats.GroundAcceleration, MoveStats.GroundDeceleration, InputManager.Movement);
            }
            else
            {
                Move(MoveStats.AirAcceleration, MoveStats.AirDeceleration, InputManager.Movement);
                
            }
        }

        #region Movement

        private void Move(float acceleration, float deceleration, Vector2 moveInput)
        {
            if (moveInput != Vector2.zero)
            {
                //TurnCheck(moveInput);

                Vector2 targetVelocity;
                if (InputManager.RunIsHeld)
                {
                    targetVelocity = new Vector2(moveInput.x, 0f) * MoveStats.MaxRunSpeed;
                }
                else
                {
                    targetVelocity = new Vector2(moveInput.x, 0f) * MoveStats.MaxWalkSpeed;
                }

                _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
                _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);
            }
            else if (moveInput == Vector2.zero)
            {
                _moveVelocity = Vector2.Lerp(_moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
                _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);
            }
        }

        // private void TurnCheck(Vector2 moveInput)
        // {
        //     if (_isFacingRight && moveInput.x > 0)
        //     {
        //         Turn(false);
        //     }
        //     else if (!_isFacingRight && moveInput.x > 0)
        //     {
        //         Turn(true);
        //     }
        // }
        //
        // private void Turn(bool turnRight)
        // {
        //     if (turnRight)
        //     {
        //         _isFacingRight = true;
        //         transform.Rotate(0f, 180f, 0f);
        //     }
        //     else
        //     {
        //         _isFacingRight = false;
        //         transform.Rotate(0f, -180f, 0f);
        //     }
        // }

        #endregion

        #region Jump

        private void JumpChecks()
        {
            //when we press the jump button
            if (InputManager.JumpWasPressed)
            {
                _jumpBufferTimer = MoveStats.JumpBufferTime;
                _jumpReleasedDuringBuffer = false;
            }

            //when we release the jump button
            if (InputManager.JumpWasReleased)
            {
                if (_jumpBufferTimer > 0f)
                {
                    _jumpReleasedDuringBuffer = true;
                }

                //might need to fix with invert
                if (_isJumping && VerticalVelocity > 0f)
                {
                    
                }

            }

            //initiate jump with jump buffering and coyote time
            if (_jumpBufferTimer > 0f && !_isJumping && (_isGrounded || _coyoteTimer > 0f))
            {
                InitiateJump(1);
                
            }

            //double jump
            else if (_jumpBufferTimer > 0f && _isJumping && _numberOfJumpsUsed < MoveStats.NumberOfJumpsAllowed)
            {
                InitiateJump(1);
            }
            
            //air jump after coyote time lapsed
            // prevents two jumps after falling from ledge
            else if (_jumpBufferTimer > 0f && _isFalling && _numberOfJumpsUsed < MoveStats.NumberOfJumpsAllowed - 1)
            {
                InitiateJump(2);
            }

            //landed
            // might need to be fixed with invert gravity?
            if ((_isJumping || _isFalling) && _isGrounded && ((VerticalVelocity <= 0f && _gravityDirection == 1) || (VerticalVelocity > 0f && _gravityDirection == -1)))
            {
                _isJumping = false;
                _isFalling = false;
                
                _numberOfJumpsUsed = 0;
            
                // default gravity value based on physics system
                VerticalVelocity = MoveStats.Gravity * _gravityDirection;
            }
            
            
        }

        private void InitiateJump(int numberOfJumpsUsed)
        {
            if (!_isJumping)
            {
                _isJumping = true;
            }

            _jumpBufferTimer = 0f;
            _numberOfJumpsUsed += numberOfJumpsUsed;
            VerticalVelocity = MoveStats.InitialJumpVelocity * _gravityDirection;
        }

        private void Jump()
        {
            Debug.Log($"VelY: {VerticalVelocity}, Gravity: {MoveStats.Gravity}, Grounded: {_isGrounded}, Jumping: {_isJumping}");
            //apply gravity while jumping
            if (_isJumping)
            {
                // when upward velocity runs out, stop being "jumping"
                if (VerticalVelocity <= 0f)
                {
                    _isFalling = true;
                }
                
                //check for head bump
                if (_bumpedHead)
                {
                    _isFalling = true;
                }
                
                if (VerticalVelocity >= 0f)
                {
                    VerticalVelocity += MoveStats.Gravity * _gravityDirection * Time.fixedDeltaTime;
                }
                
                //gravity on descending
                else if (VerticalVelocity <= 0f)
                {
                    if (!_isFalling)
                    {
                        _isFalling = true;
                    }
                }
            }
            
            //normal gravity while falling
            if (!_isGrounded && _isFalling)
            {
                VerticalVelocity += MoveStats.Gravity * _gravityDirection * Time.fixedDeltaTime;
            }
            
            //clamp fall speed
            VerticalVelocity = Mathf.Clamp(VerticalVelocity, -MoveStats.MaxFallSpeed, 50f);
            
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, VerticalVelocity); 
        }
        
        #endregion
        
        #region Collision Checks

        private void IsGrounded()
        {
            // Pick origin based on gravity direction
            float yOrigin = (_gravityDirection == 1) 
                ? _feetColl.bounds.min.y      // bottom for normal gravity
                : _feetColl.bounds.max.y;     // top for inverted gravity
    
            Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, yOrigin);
            Vector2 boxCastSize = new Vector2(_bodyColl.bounds.size.x, MoveStats.GroundDetectionRayLength);

            // Cast in direction of "gravity down"
            Vector2 castDir = (_gravityDirection == 1) ? Vector2.down : Vector2.up;

            _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, castDir, MoveStats.GroundDetectionRayLength, MoveStats.GroundLayer);
            
            if (_groundHit.collider != null)
            {
                _isGrounded = true;
            }
            else { _isGrounded = false; }
            
            #region Debug Visualization

            if (MoveStats.DebugShowIsGroundedBox)
            {
                Color rayColor;
                if (_isGrounded)
                {
                    rayColor = Color.green;
                }
                else {rayColor = Color.red;}
                
                Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * MoveStats.GroundDetectionRayLength, rayColor);
                Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * MoveStats.GroundDetectionRayLength, rayColor);
                Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - MoveStats.GroundDetectionRayLength), Vector2.right * boxCastSize.x, rayColor);
            }
            
            #endregion
            
        }

        private void BumpedHead()
        {
            // Opposite of grounded: check "against" gravity
            float yOrigin = (_gravityDirection == 1)
                ? _bodyColl.bounds.max.y      // top for normal gravity
                : _feetColl.bounds.min.y;     // bottom for inverted gravity

            Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, yOrigin);
            Vector2 boxCastSize = new Vector2(_bodyColl.bounds.size.x * MoveStats.HeadWidth, MoveStats.HeadDetectionRayLength);

            // Cast opposite gravity
            Vector2 castDir = (_gravityDirection == 1) ? Vector2.up : Vector2.down;

            _headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, castDir, MoveStats.HeadDetectionRayLength, MoveStats.GroundLayer);
            
            if (_headHit.collider != null)
            {
                _bumpedHead = true;
            }
            else { _bumpedHead = false; }
            
            #region Debug Visualization

            if (MoveStats.DebugShowHeadBumpBox)
            {
                float headWidth = MoveStats.HeadWidth;
                
                Color rayColor;
                if (_bumpedHead)
                {
                    rayColor = Color.green;
                }
                else {rayColor = Color.red;}
                
                Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y), Vector2.up * MoveStats.HeadDetectionRayLength, rayColor);
                Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x / 2 * headWidth, boxCastOrigin.y), Vector2.up * MoveStats.HeadDetectionRayLength, rayColor);
                Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y + MoveStats.HeadDetectionRayLength), Vector2.right * boxCastSize.x * headWidth, rayColor);
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
            _jumpBufferTimer -= Time.deltaTime;

            if (!_isGrounded)
            {
                _coyoteTimer -= Time.deltaTime;
            }
            else
            {
                _coyoteTimer = MoveStats.JumpCoyoteTime; 
            }
        }
        
        #endregion

        public void FlipGravity()
        {
            _gravityDirection *= -1;
            transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
        }
    }
}
