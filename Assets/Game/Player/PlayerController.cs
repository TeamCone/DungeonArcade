using System.Collections;
using Assets.Game.Scripts.Interface;
using Game.PickItem;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController: MonoBehaviour, IPlayer
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;
        private float _horizontalMovement;
        private float _verticalMovement;
        
        [SerializeField]
        private float _moveSpeed = 5;
        [SerializeField]
        private float _jumpHeight = 10;

        private ICharacter _character;


        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
        }
        
        private void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector3(_horizontalMovement, _verticalMovement);
        }

        public void SetCharacter(ICharacter character)
        {
            _character = character;
        }

        public void Jump()
        {
            _rigidbody2D.velocity = new Vector3(_horizontalMovement, _jumpHeight);
        }

        public void ThrowItem()
        {
            
        }

        public void MoveHorizontal(float value)
        {
            _horizontalMovement = value * _moveSpeed;
        }

        public void MoveVertical(float value)
        {
            _verticalMovement = value * _moveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.CompareTag("PickItem"))
            {
                var item = other.GetComponent<IPickItem>();
                _character.ReceiveItem(item.GetItem());
                item.DestroyItem();
            }    
            
            if (other.CompareTag("ThrowItem"))
            {
                var item = other.GetComponent<IThrowItem>();
                _character.DeductHP(item.GetDamage());
                item.DestroyItem();
            }   
            
        }
    }
}