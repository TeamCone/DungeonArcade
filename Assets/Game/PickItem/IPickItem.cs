using Assets.Game.Scripts.Interface;

namespace Game.PickItem
{
    public interface IPickItem
    {
        IItem GetItem();
        void DestroyItem();
    }
}