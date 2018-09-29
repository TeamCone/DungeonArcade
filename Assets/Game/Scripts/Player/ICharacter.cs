using Game.Player;

namespace Game.Scripts
{
    public interface ICharacter
    {
        EnumState CurrentState();
        void SetState(EnumState state);
        IItem CurrentItem();
        void PickUpItem(IItem item);
        void ThrowItem();
        void CharacterHit();
    }
}