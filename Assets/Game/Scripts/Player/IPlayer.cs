using NUnit.Framework;

namespace Game.Player
{
    public interface IPlayer
    {
        void Jump();
        void ThrowItem();
        void MoveHorizontal(float value);
    }
}