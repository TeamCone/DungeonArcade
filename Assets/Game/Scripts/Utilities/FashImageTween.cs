using UnityEngine;
using UnityEngine.UI;

public class FashImageTween : MonoBehaviour {

	
	// Use this for initialization
	void Start () 
	{
		TweenFacade.FlashImage(GetComponent<Image>(), 1);
	}

}
