using Game.Player;

namespace Game.Scripts
{
    public interface ICharacter
    {
        EnumPlayerState CurrentState();
        void SetState(EnumPlayerState playerState);
        IItem CurrentItem();
        void PickUpItem(IItem item);
        void ThrowItem();
        void CharacterHit(IItem item);
        bool HasItem();
    }
}