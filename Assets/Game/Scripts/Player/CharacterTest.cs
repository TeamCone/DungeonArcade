using System;
using Game.Player;
using NUnit.Framework;

namespace Game.Scripts
{
    
    [TestFixture]
    public class CharacterTest
    {
        private ICharacter _p;
        
        [SetUp]
        public void SetUp()
        {
            _p = new Character();
        }
        
        [Test]
        public void TestCharacterDefaults()
        {
            Assert.AreEqual(null, _p.CurrentItem());
            Assert.AreEqual(EnumState.DEFAULT, _p.CurrentState());
        }

        [Test]
        public void TestPickAndThrowItem()
        {
            //Throw with no Item
            _p.ThrowItem();
            Assert.AreEqual(null, _p.CurrentItem());
            
            //Pick Up Item
            var dummy = new ThrowableItem();
            _p.PickUpItem(dummy);
            Assert.AreEqual(dummy, _p.CurrentItem());
            
            //Throw with Item
            _p.ThrowItem();
            Assert.AreEqual(null, _p.CurrentItem());
            
        }

        [Test]
        public void TestChangeState()
        {
            Assert.AreEqual(EnumState.DEFAULT, _p.CurrentState());

            var expectedState = EnumState.KNOCKED_DOWN;
            _p.SetState(expectedState);
            Assert.AreEqual(expectedState, _p.CurrentState());
        }

        [Test]
        public void TestCharacterHit()
        {
            _p.PickUpItem(new ThrowableItem());
            _p.CharacterHit();
            Assert.AreEqual(EnumState.KNOCKED_DOWN, _p.CurrentState(), "State should be KNOCKED_DOWN");
            Assert.AreEqual(null, _p.CurrentItem(), "Current Item should be null");
        }

        [Test]
        public void TestCharacterInvulnerable()
        {
            _p.SetState(EnumState.INVULNERABLE);
            var dummy = new ThrowableItem();
            _p.PickUpItem(dummy);
            _p.CharacterHit();
            Assert.AreEqual(EnumState.INVULNERABLE, _p.CurrentState(), "State should still be INVULNERABLE");
            Assert.AreEqual(dummy, _p.CurrentItem(), "Current Item should not be null");
        }

        [Test]
        public void TestThrowItems()
        {
            //Arrange
            var rock = new ThrowableItem();
            var gold = new NonThrowableItem();
            
            _p.PickUpItem(rock);
            Assert.AreEqual(rock, _p.CurrentItem(), "Current item should be rock.");
            _p.ThrowItem();
            Assert.AreEqual(null, _p.CurrentItem(), "Current item should be null.");
            
            _p.PickUpItem(gold);
            Assert.AreEqual(gold, _p.CurrentItem(), "Current item should be gold.");
            _p.ThrowItem();
            Assert.AreEqual(gold, _p.CurrentItem(), "Current item should still be gold.");
        }
     }

    public class ThrowableItem : IItem
    {
        public bool IsThrowable()
        {
            return true;
        }
    }
    
    public class NonThrowableItem : IItem
    {
        public bool IsThrowable()
        {
            return false;
        }
    }
    
}