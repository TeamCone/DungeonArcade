using Game.Input;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController: MonoBehaviour, IPlayer
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;
        private float _horizontalMovement;
        
        [SerializeField]
        private float _moveSpeed = 5;
        [SerializeField]
        private float _jumpHeight = 10;
        private EnumPlayer _enumPlayer;


        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            TEST();
        }

        private void TEST()
        {
            SetPlayer(EnumPlayer.Player1);
            GameInputController.Instance.SetPlayer(_enumPlayer, this);
        }
        
        public void SetPlayer(EnumPlayer enumPlayer)
        {
            _enumPlayer = enumPlayer;
        }

        
        private void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector3(_horizontalMovement, _rigidbody2D.velocity.y);
        }

     
        public void Jump()
        {
            _rigidbody2D.velocity = new Vector3(_horizontalMovement, _jumpHeight);
        }

        public void UseItem()
        {
         
        }

        public void MoveHorizontal(float value)
        {
            _horizontalMovement = value * _moveSpeed;
           
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
        
            
        }
    }
}