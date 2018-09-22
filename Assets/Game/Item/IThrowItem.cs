using Game.Player;

namespace Game.Item
{
    public interface IThrowItem
    {
        void SetItem(IItem item);
        void Throw();
        int GetDamage();
        void DestroyItem();
        EnumPlayer EnumPlayer { get; set; }
    }
}