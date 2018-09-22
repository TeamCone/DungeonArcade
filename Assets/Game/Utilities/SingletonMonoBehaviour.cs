namespace Game.Utilities
{
    using UnityEngine;

    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        Debug.LogError(typeof(T) + " is not in the heirarchy");
                    }
                }

                return instance;
            }
        }
    }


//Attach this to a gameobject in the heirarchy
    public class MyMonobehaviorClass : SingletonMonoBehaviour<MyMonobehaviorClass>
    {
        public void Beer()
        {
            Debug.Log("Hello Beers!");
        }
    }

//Lets call that singleton class
    public class BeerCaller
    {
        public void CallBeer()
        {
            MyMonobehaviorClass.Instance.Beer();
        }
    }


}