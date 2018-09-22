using Game.Player;
using UnityEngine;

namespace Game.Item
{
    public class ThrowItemController: MonoBehaviour, IThrowItem
    {
        private IItem _item;

        public void SetItem(IItem item)
        {
            _item = item;
        }

        public void Throw()
        {
            Debug.Log("throwing " +_item);
        }

        public int GetDamage()
        {
            return _item.Damage;
        }
        

        public void DestroyItem()
        {
            Destroy(gameObject);
        }

        public EnumPlayer EnumPlayer { get; set; }
    }
}