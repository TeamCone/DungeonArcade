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
            _item.SetState(EnumItemState.PICKED);
        }

        public void ThrowItem(bool isFacingRight)
        {
            _item.Throw(isFacingRight);
            _item = null;
        }

        public bool IsCharacterHit(IItem item)
        {
            if (_playerState == EnumPlayerState.Invulnerable)
            {
                return false;
            }

            if (item != null)
            {
                if (item.GetOrigin() == _player)
                {
                    return false;
                }
                
                if (item.GetOrigin() == EnumPlayer.None)
                {
                    return false;
                }
            }
            
            
            _playerState = EnumPlayerState.Hit;

            if (_item != null)
            {
                //when carrying item, remove from player
                if (_item.GetState() == EnumItemState.PICKED)
                {
                    _item.RemoveItem(); 
                }
               
                _item = null;
                
            }
           
            return true;
        }

        public bool HasItem()
        {
            if (_item != null)
            {
                if(_item.HasHolder())
                {
                    return true;
                }
               
                _item = null;
                return false;
            }

            return false;

            //sometimes not accurate so i removed
            //return _item != null;
        }
    }
}