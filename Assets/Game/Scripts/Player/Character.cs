using Game.Player;
using UnityEngine;

namespace Game.Scripts
{
    public class Character : ICharacter
    {
        private EnumPlayerState _playerState;
        private readonly EnumPlayer _player;
        private IItem _item;

        public Character(EnumPlayer player)
        {
            _player = player;
        }
        
        public EnumPlayerState CurrentState()
        {
            return _playerState;
        }

        public void SetState(EnumPlayerState playerState)
        {
            _playerState = playerState;
        }

        public IItem CurrentItem()
        {
            return _item;
        }

        public void PickUpItem(IItem item)
        {
            _item = item;
        }

        public void ThrowItem(bool isFacingRight)
        {
            if (_item?.IsThrowable() == false)
            {
                return;
            }

            if (_item?.GetState() != EnumItemState.PICKED)
            {
                return;
            }
            _item.Throw(isFacingRight);
            _item = null;
        }

        public bool IsCharacterHit(IItem item)
        {
            if (_playerState == EnumPlayerState.Invulnerable)
            {
                return false;
            }

            if (item.GetOrigin() == _player)
            {
                return false;
            }
            
            _playerState = EnumPlayerState.Hit;

            if (_item != null)
            {
                //when carrying item, remove from player
                if (_item.GetState() == EnumItemState.PICKED)
                {
                    Debug.Log("REMOVING ITEM ");
                    _item.RemoveItem(); 
                }
               
                _item = null;
                
            }
           
            return true;
        }

        public bool HasItem()
        {
            return _item != null;
        }
    }
}