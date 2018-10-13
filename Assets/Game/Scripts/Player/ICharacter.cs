using Game.Player;

namespace Game.Scripts
{
    public interface ICharacter
    {
        EnumPlayerState CurrentState();
        void SetState(EnumPlayerState playerState);
        IItem CurrentItem();
        void PickUpItem(IItem item);
        void ThrowItem(bool isFacingRight);
        bool IsCharacterHit(IItem item);
        bool HasItem();
    }
}