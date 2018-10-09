namespace Game.Player
{
    public interface IPlayer
    {
        void Jump();
        void ThrowItem();
        void MoveHorizontal(float value);
        bool IsWinner();
        EnumPlayer GetPlayerId();
    }
}