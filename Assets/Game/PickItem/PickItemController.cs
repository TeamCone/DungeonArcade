using System;
using UnityEngine;

namespace Game.PickItem
{
    public class PickItemController: MonoBehaviour
    {
        private EnumItem _item;
        private SpriteRenderer _spriteRenderer;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void Start()
        {  
            //get random items
            var itemCount = Enum.GetNames(typeof(EnumItem)).Length;
            var randomItem = UnityEngine.Random.Range(0, itemCount);
            _item = (EnumItem) randomItem;

            //show item image
            var itemSprite = Resources.Load<Sprite>("Item" + _item);
            _spriteRenderer.sprite = itemSprite;
        }
        
        //return item and destroy
        public EnumItem GetItem()
        {
            Destroy(gameObject);
            return _item;
        }
        
    }
}