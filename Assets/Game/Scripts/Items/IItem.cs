using UnityEngine;

namespace Game.Player
{
    public interface IItem
    {
        bool IsThrowable();
        EnumPlayer GetOrigin();
        void SetOrigin(EnumPlayer player, Transform itemHolder);
        EnumItemState GetState();
        void SetState(EnumItemState state);
        void Throw(bool isFacingRight);
        void RemoveItem();
    }
}