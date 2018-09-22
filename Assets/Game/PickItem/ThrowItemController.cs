using Assets.Game.Scripts.Interface;
using UnityEngine;

namespace Game.PickItem
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
            throw new System.NotImplementedException();
        }

        public int GetDamage()
        {
            return _item.Damage;
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }
}