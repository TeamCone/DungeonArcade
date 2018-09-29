using UnityEngine;

namespace Game.Utilities
{
    public static class PrefabCreator
    {
        public static GameObject CreatePrefab(string prefabName, Transform parent)
        {
            var prefab = GameObject.Instantiate(ResourceGetter.GetPrefab(prefabName), parent) ;
            return prefab;
        }
    }
}