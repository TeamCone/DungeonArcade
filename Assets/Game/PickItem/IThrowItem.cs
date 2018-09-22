using Assets.Game.Scripts.Interface;

namespace Game.PickItem
{
    public interface IThrowItem
    {
        void SetItem(IItem item);
        void Throw();
        int GetDamage();
        void DestroyItem();
    }
}