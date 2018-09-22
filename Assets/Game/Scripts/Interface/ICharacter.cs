using Game.Scripts.Model;

namespace Assets.Game.Scripts.Interface
{
    public interface ICharacter
    {
        int CurrentHP();
        void DeductHP(int dmg = 1);
        EnumState CurrentState();
        void SetState(EnumState state);
        IItem CurrentItem();
        void ReceiveItem(IItem item);
        void ThrowItem();
        void Skill();
    }

}