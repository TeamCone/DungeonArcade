using System.Collections;
using Assets.Game.Scripts.Interface;
using Game.Input;
using Game.Item;
using Game.Scripts.Model;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController: MonoBehaviour, IPlayer
    {
        private Rigidbody _rigidbody;
        private Transform _transform;
        private float _horizontalMovement;
        private float _verticalMovement;
        
        [SerializeField]
        private float _moveSpeed = 5;
        [SerializeField]
        private float _jumpHeight = 10;

        private ICharacter _character;
        private EnumPlayer _enumPlayer;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();

            TEST();
        }

        private void TEST()
        {
            SetPlayer(EnumPlayer.Player1);
            SetCharacter(new Wizard());
            GameInputController.Instance.SetPlayer(_enumPlayer, this);
            
        }
        
        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector3(_horizontalMovement, _rigidbody.velocity.y , _verticalMovement);
        }

        public void SetPlayer(EnumPlayer enumPlayer)
        {
            _enumPlayer = enumPlayer;
        }

        public void SetCharacter(ICharacter character)
        {
            _character = character;
        }

        public void Jump()
        {
            _rigidbody.velocity = new Vector3(_horizontalMovement, _jumpHeight , _verticalMovement);
        }

        public void ThrowItem()
        {
            var item = _character.CurrentItem();
            var throwItem = Instantiate(Resources.Load<GameObject>("ThrowItemSword"), transform.parent).GetComponent<ThrowItemController>();
            throwItem.SetItem(item);
            throwItem.EnumPlayer = _enumPlayer;
            throwItem.Throw();
            _character.ThrowItem();
        }

        public void MoveHorizontal(float value)
        {
            _horizontalMovement = value * _moveSpeed;
           
        }

        public void MoveVertical(float value)
        {
            _verticalMovement = value * _moveSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PickItem"))
            {
                IPickItem item = other.GetComponent<PickItemController>();
                var i = item.GetItem();
                _character.ReceiveItem(i);
                Debug.LogFormat("item: {0}, character: {1}", i, _character);
                item.DestroyItem();
            }    
            
            if (other.CompareTag("ThrowItem"))
            {
                IThrowItem item = other.GetComponent<ThrowItemController>();
                
                //avoid damage self
                if (item.EnumPlayer == _enumPlayer)
                {
                    return;
                }
                
                _character.DeductHP(item.GetDamage());
                item.DestroyItem();
            }   
            
        }
    }
}