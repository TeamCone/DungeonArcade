using System;
using Game.Input;
using Game.Scripts;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController: MonoBehaviour, IPlayer
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;
        private Animator _animator;
        private float _horizontalMovement;
        
        [SerializeField]
        private float _moveSpeed = 5f;
        [SerializeField]
        private float _jumpHeight = 10f;
        [SerializeField]
        private float _springJumpHeight = 20f;
        [SerializeField]
        private EnumPlayer _enumPlayer;

        private ICharacter _character;
        
        [SerializeField]private LayerMask _springLayerMask;
        [SerializeField]private LayerMask _groundLayerMask;
        [SerializeField] private Transform _groundCheck;
       
        private bool _isGrounded;
        private bool _isSpringJump;
        private bool _isOnConveyer;


        private const string AnimatorIsGrounded = "IsGrounded";
        private const string AnimatorRun = "Run";
        private const string AnimatorJump = "Jump";
        private const string AnimatorThrow = "Throw";
        private const string AnimatorHit = "Hit";
        private const string AnimatorIsDead = "IsDead";
        private const string AnimatorHasItem = "HasItem";
        

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            
            //Create Character Object
            _character = new Character(_enumPlayer);
        }

        private void FixedUpdate()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _groundLayerMask);
            _animator.SetBool(AnimatorIsGrounded, _isGrounded);
            
            _rigidbody2D.velocity = new Vector3(_horizontalMovement, _rigidbody2D.velocity.y);
            
            _animator.SetFloat(AnimatorRun, Mathf.Abs(_horizontalMovement));
          
            _animator.SetBool(AnimatorHasItem, _character.HasItem());
            
            _isSpringJump = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _springLayerMask);

            if (_isSpringJump)
            {
                _rigidbody2D.velocity = new Vector3(_horizontalMovement, _springJumpHeight );
                _animator.SetTrigger(AnimatorJump);
            }
        }
     
        public void Jump()
        {
            if (_isGrounded == false)
            {
                return;
            }
            
            _rigidbody2D.velocity = new Vector3(_rigidbody2D.velocity.x, _jumpHeight);
            _animator.SetTrigger(AnimatorJump);
        }

        public void ThrowItem()
        {
            if (_character.HasItem() == false)
            {
                return;
            }

            if (_character.CurrentItem().IsThrowable())
            {
                _animator.SetTrigger(AnimatorThrow);
                _character.CurrentItem().Throw();
            }
            
           
        }

        public void MoveHorizontal(float value)
        {
            _horizontalMovement = value * _moveSpeed;
            FaceCharacter(value);

        }

        public bool IsWinner()
        {
            var item = _character.CurrentItem();
            if (item == null)
            {
                return false;
            }
            
            return item.IsThrowable();
        }

        public EnumPlayer GetPlayerId()
        {
            return _enumPlayer;
        }


        private void FaceCharacter(float value)
        {

            var xScale = _transform.localScale.x;
         
            if (value > 0)
            {
                xScale = 1;
            }
            
            if (value < 0)
            {
                xScale = -1;
            }
            
            
            _transform.localScale = new Vector3(xScale,_transform.localScale.z,_transform.localScale.z);
        }


        private void Hit()
        {
            _animator.SetBool(AnimatorIsDead, true);
            _animator.SetTrigger(AnimatorHit);
        }
        
        private void Resurrect()
        {
            _animator.SetBool(AnimatorIsDead, false);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                //hit 
                switch (other.gameObject.GetComponent<IItem>().GetState())
                {
                    case EnumItemState.IDLE:
                        if (_character.HasItem())
                        {
                            return;
                        }
                        _character.PickUpItem(other.gameObject.GetComponent<IItem>());
                        _character.CurrentItem().SetState(EnumItemState.PICKED);
                        _character.CurrentItem().SetOrigin(_enumPlayer);
                        break;
                    case EnumItemState.MOVING:
                        if (other.gameObject.GetComponent<IItem>().GetOrigin() == _enumPlayer)
                        {
                            return;
                        }
                        
                        Hit();
                        
                        break;
                    case EnumItemState.PICKED:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            
                
            }
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
           
        }

        private void OnCollisionExit2D(Collision2D other)
        {
         
            
        }
    }
}