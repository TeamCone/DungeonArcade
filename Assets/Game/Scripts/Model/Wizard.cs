using System.Runtime.ExceptionServices;
using Assets.Game.Scripts.Interface;
using UnityEngine;

namespace Game.Scripts.Model
{
    public class Wizard: ICharacter
    {
        private int _baseHp;
        private int _currentHp;
        private EnumState _state;
        private IItem _item;

        public Wizard()
        {
            _baseHp = 10;
            _currentHp = _baseHp;
        }

        public int CurrentHP()
        {
            return _currentHp;
        }

        public void DeductHP(int dmg = 1)
        {
            _currentHp -= dmg;
        }

        public EnumState CurrentState()
        {
            return _state;
        }

        public void SetState(EnumState state)
        {
            _state = state;
        }

        public IItem CurrentItem()
        {
            return _item;
        }

        public void ReceiveItem(IItem item)
        {
            if (_item != null)
            {
                return;
            }
            _item = item;
        }

        public void ThrowItem()
        {
            _item = null;
        }

        public void Skill()
        {
            Debug.Log("Thunderstorm!");
        }
    }
}