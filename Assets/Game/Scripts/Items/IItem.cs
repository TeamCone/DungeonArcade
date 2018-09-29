namespace Game.Player
{
    public interface IItem
    {
        bool IsThrowable();
        EnumPlayer GetOrigin();
        void SetOrigin(EnumPlayer player);
        EnumItemState GetState();
        void SetState(EnumItemState state);
    }
}