namespace Game.Player
{
    public interface IPlayer
    {
        void Jump();
        void UseItem();
        void MoveHorizontal(float value);
    }
}