using Game.Player;

namespace Game.Scripts
{
    public class NonThrowableItem : IItem
    {
        private EnumPlayer _enumPlayer;
        private EnumItemState _enumItemState = EnumItemState.IDLE;
        
        public bool IsThrowable()
        {
            return false;
        }

        public EnumPlayer GetOrigin()
        {
            return _enumPlayer;
        }

        public void SetOrigin(EnumPlayer player)
        {
            _enumPlayer = player;
        }

        public EnumItemState GetState()
        {
            return _enumItemState;
        }

        public void SetState(EnumItemState state)
        {
            _enumItemState = state;
        }
    }
}