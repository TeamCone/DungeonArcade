using NUnit.Framework;

namespace Game.Player
{
    public interface IPlayer
    {
        void Jump();
        void UseItem();
        void MoveHorizontal(float value);
    }

    public enum EnumState
    {
        DEFAULT,
        KNOCKED_DOWN,
        INVULNERABLE
    }
}