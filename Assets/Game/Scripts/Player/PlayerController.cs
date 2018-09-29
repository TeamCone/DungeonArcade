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
        private float _moveSpeed = 5f;
        [SerializeField]
        private float _jumpHeight = 10f;
        [SerializeField]
        private float _springJumpHeight = 20f;
        
        private EnumPlayer _enumPlayer;
        
        [SerializeField]private LayerMask _springLayerMask;
        [SerializeField]private LayerMask _groundLayerMask;
        [SerializeField] private Transform _groundCheck;
        private Animator _animator;
        private bool _isGrounded;
        private bool _isSpringJump;



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
            
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _groundLayerMask);
            _isSpringJump = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _springLayerMask);

            if (_isSpringJump)
            {
                _rigidbody2D.velocity = new Vector3(_horizontalMovement, _springJumpHeight );
            }
        }
     
        public void Jump()
        {
            if (_isGrounded == false)
            {
                return;
            }
            
            _rigidbody2D.velocity = new Vector3(_horizontalMovement, _jumpHeight);
        }

        public void UseItem()
        {
         
        }

        public void MoveHorizontal(float value)
        {
            _horizontalMovement = value * _moveSpeed;
            FaceCharacter(value);

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
        

        private void OnTriggerEnter2D(Collider2D other)
        {
        
            
        }
    }
}