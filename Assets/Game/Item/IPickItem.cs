namespace Game.Item
{
    public interface IPickItem
    {
        IItem GetItem();
        void DestroyItem();
    }
}