using Game.Player;

namespace Game.Scripts
{
    public class Character : ICharacter
    {
        private EnumState _state;
        private IItem _item;
        
        public EnumState CurrentState()
        {
            return _state;
        }

        public void SetState(EnumState state)
        {
            _state = state;
        }

        public IItem CurrentItem()
        {
            return _item;
        }

        public void PickUpItem(IItem item)
        {
            _item = item;
        }

        public void ThrowItem()
        {
            if (_item.IsThrowable() == false)
            {
                return;
            }
            _item = null;
        }

        public void CharacterHit()
        {
            if (_state == EnumState.INVULNERABLE)
            {
                return;
            }
            _state = EnumState.KNOCKED_DOWN;
            _item = null;
        }
    }
}