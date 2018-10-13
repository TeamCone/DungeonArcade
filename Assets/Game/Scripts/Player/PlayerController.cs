﻿using System;
using System.Collections;
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
        private SpriteRenderer _spriteRenderer;
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
        
        [SerializeField] private LayerMask _springLayerMask;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Transform _itemHolder;
       
        private bool _isGrounded;
        private bool _isSpringJump;
        private bool _isOnConveyer;
        private bool _isHit;
        private bool _isFacingRight = true;


        private const string AnimatorIsGrounded = "IsGrounded";
        private const string AnimatorRun = "Run";
        private const string AnimatorJump = "Jump";
        private const string AnimatorThrow = "Throw";
        private const string AnimatorHit = "Hit";
        private const string AnimatorIsDead = "IsDead";
        private const string AnimatorHasItem = "HasItem";
        
        private const float HitTime = 3;
        private const float InvulnerableTime = 2;
        

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            //Create Character Object
            _character = new Character(_enumPlayer);
        }

        private void FixedUpdate()
        {
            if (_isHit)
            {
                return;
            }
            
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
            if (_isHit)
            {
                return;
            }
            
            if (_isGrounded == false)
            {
                return;
            }
            
            _rigidbody2D.velocity = new Vector3(_rigidbody2D.velocity.x, _jumpHeight);
            _animator.SetTrigger(AnimatorJump);
        }

        public void ThrowItem()
        {
            if (_isHit)
            {
                return;
            }
            
            if (_character.HasItem() == false)
            {
                return;
            }

            if (_character.CurrentItem().IsThrowable() == false)
            {
                return;
            }
            
            _animator.SetTrigger(AnimatorThrow);
            _character.ThrowItem(_isFacingRight);

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
            if (_isHit)
            {
                return;
            }
            
            var xScale = _transform.localScale.x;
         
            if (value > 0)
            {
                xScale = 1;
                _isFacingRight = true;
            }
            
            if (value < 0)
            {
                xScale = -1;
                _isFacingRight = false;
            }
            
            
            _transform.localScale = new Vector3(xScale,_transform.localScale.z,_transform.localScale.z);
        }


        private async void Hit()
        {
            _animator.SetBool(AnimatorIsDead, true);
            _animator.SetTrigger(AnimatorHit);
            _rigidbody2D.velocity = new Vector3(0, _rigidbody2D.velocity.y);
            _isHit = true;

            await Invulnerable();
            await BackToNormal();
        }
        
        private IEnumerator BackToNormal()
        {
            TweenFacade.CharacterInvulnerable(_spriteRenderer, InvulnerableTime);
            yield return new WaitForSeconds(InvulnerableTime);
            _character.SetState(EnumPlayerState.Default);
        }
        
        private IEnumerator Invulnerable()
        {
            yield return new WaitForSeconds(HitTime);
            _character.SetState(EnumPlayerState.Invulnerable);
            _isHit = false;
            _animator.SetBool(AnimatorIsDead, false);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                var item = other.gameObject.GetComponent<IItem>();
                switch (item.GetState())
                {
                    case EnumItemState.IDLE:
                        if (_isHit)
                        {
                            return;
                        }
                        
                        if (_character.HasItem())
                        {
                            return;
                        }
                        _character.PickUpItem(item);
                        _character.CurrentItem().SetState(EnumItemState.PICKED);
                        _character.CurrentItem().SetOrigin(_enumPlayer, _itemHolder);
                        break;
                    case EnumItemState.MOVING:
                        if (_isHit)
                        {
                            return;
                        }
                        
                        if (_character.IsCharacterHit(item) == false)
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