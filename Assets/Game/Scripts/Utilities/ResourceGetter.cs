using UnityEngine;

namespace Game.Utilities
{
    public static class ResourceGetter
    {
        public static GameObject GetPrefab(string prefabName)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
            return prefab;
        }

        public static Sprite GetSprite(string spriteName)
        {
            var sprite = Resources.Load<Sprite>("Sprites/" + spriteName);
            return sprite;
        }
    }
    

}