using System;
using Assets.Game.Scripts.Interface;
using UnityEngine;

namespace Game.PickItem
{
    public class PickItemController: MonoBehaviour, IPickItem
    {
        private IItem _item;
        private SpriteRenderer _spriteRenderer;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void Start()
        {  
            //get random items
            _item = new Sword();

            //show item image
            var itemSprite = Resources.Load<Sprite>(_item.Name);
            _spriteRenderer.sprite = itemSprite;
        }
        
        //return item and destroy
        public IItem GetItem()
        {
            return _item;
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }
        
    }
}